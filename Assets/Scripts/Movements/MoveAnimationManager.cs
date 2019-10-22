using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimationManager : MonoBehaviour
{
    private MoveManager moveManager;
    private bool stepParticles = false;
    private bool isGrounded = false;

    private ParticleSystem stepParticlesSys;
    private BoxCollider2D playerCollider;

    private Animator anim;

    [SerializeField]
    private GameObject jumpParticlePrefab;

    // Start is called before the first frame update
    void Start()
    {
        moveManager = GetComponent<MoveManager>();
        Debug.Assert(moveManager, "You must add a MoveManager !");

        GameObject stepParticlesPreFab = GameObject.Find("StepParticles");
        Debug.Assert(stepParticlesPreFab, "You must add a StepParticles prefab");
        stepParticlesSys = stepParticlesPreFab.GetComponent<ParticleSystem>();
        Debug.Assert(stepParticlesSys, "The StepParticles prefab does not have a ParticleSystem to be used.");

        Debug.Assert(jumpParticlePrefab, "You must add a jumParticlePrefab");


        playerCollider = GetComponent<BoxCollider2D>();
        Debug.Assert(playerCollider, "You must add a BoxCollider2D to the gameObject.");

        anim = GetComponentInChildren<Animator>();
        Debug.Assert(anim, "You must add an Animator in the player");

        stepParticlesSys.Stop();
    }

    private void OnEnable()
    {
        GroundDetection.OnLand += OnLandGround;
        GroundDetection.OnLeaveGround += OnLeaveGround;
        MoveManager.OnJump += JumpEffect;
    }

    private void OnDisable()
    {
        GroundDetection.OnLand -= OnLandGround;
        GroundDetection.OnLeaveGround -= OnLeaveGround;
        MoveManager.OnJump -= JumpEffect;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStepParticles();
    }

    private void OnLandGround()
    {
        // anim.SetTrigger("land");
        isGrounded = true;
    }

    private void OnLeaveGround()
    {
        isGrounded = false;
    }

    private void UpdateStepParticles()
    {
        if (stepParticles && (moveManager.Speed.x == 0 || !isGrounded))
        {
            stepParticlesSys.Stop();
            stepParticles = false;
        }
        else if (!stepParticles && (moveManager.Speed.x != 0 && isGrounded))
        {
            stepParticlesSys.Play();
            stepParticles = true;
        }

        if (stepParticles)
        {
            var shape = stepParticlesSys.shape;
            var main = stepParticlesSys.main;
            if (moveManager.Speed.x > 0)
            {
                shape.rotation = new Vector3(0, -65, 0);
                shape.position = new Vector3(-0.3f, 0, 0);
            }
            else
            {
                shape.rotation = new Vector3(0, 65, 0);
                shape.position = new Vector3(0.3f, 0, 0);
            }

        }
    }

    private void JumpEffect()
    {
        //  anim.SetTrigger("jump");
        StartCoroutine(Squish());
        var jumpParticles = Instantiate(jumpParticlePrefab, transform.position - new Vector3(0,-playerCollider.size.y/2), transform.rotation);
        jumpParticles.transform.localScale = new Vector2(0.8f, 0.8f);
    }

    IEnumerator Squish()
    {
        anim.speed = 2;
        anim.Play("Squish");
        yield return new WaitForSeconds(0.2f);
        anim.Play("Idle");

    }
}
