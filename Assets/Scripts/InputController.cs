using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject panel;
    private MoveManager moveManager;
    
    // Start is called before the first frame update
    void Start()
    {
        moveManager = player.GetComponent<MoveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float posX = Input.GetAxis("Horizontal");
        moveManager.UpdateHorizontalSpeed(posX);

        if (Input.GetButtonDown("Jump"))
        {
            moveManager.Jump();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            moveManager.CancelJump();
        }
        else if (Input.GetButtonDown("Parameters"))
        {
            if (panel.activeInHierarchy)
            {
                panel.SetActive(false);
            }
            else
            {
                panel.SetActive(true);
            }
        }
    }
}
