using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    [SerializeField]
    private Vector2 startPosition;
    [SerializeField]
    private Vector2 endPosition;
    [SerializeField]
    private Sprite flag;
    [SerializeField]
    private GameObject victoryParticlesPrefab;

    private Transform _player;
    
    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _player.position = startPosition;
        GameObject flagObject = new GameObject();
        flagObject.name = "Flag";
        flagObject.transform.SetParent(transform);
        flagObject.transform.position = endPosition;
        SpriteRenderer spriteRenderer = flagObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = flag;
        BoxCollider2D boxCollider2D = flagObject.AddComponent<BoxCollider2D>();
        boxCollider2D.size = spriteRenderer.size;
        boxCollider2D.isTrigger = true;
        flagObject.AddComponent<Flag>();
        flagObject.layer = gameObject.layer;


    }

    private void OnEnable()
    {
        
        Flag.OnVictory += LoadNextScene;
    }

    private void OnDisable()
    {
        Flag.OnVictory -= LoadNextScene;        
    }

    private void Update()
    {
        if (!_player.gameObject.activeSelf)
        {
            _player.position = startPosition;
            _player.gameObject.SetActive(true);
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LaunchVictory());
    }

    IEnumerator LaunchVictory()
    {
        Debug.Assert(victoryParticlesPrefab, "You must add a Victory prefab");
        var victoryParticlesInstance = Instantiate(victoryParticlesPrefab, transform);
        victoryParticlesInstance.transform.position = endPosition;
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
    }

        private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(endPosition, new Vector3(0.25f,0.25f,0.25f));
    }
}
