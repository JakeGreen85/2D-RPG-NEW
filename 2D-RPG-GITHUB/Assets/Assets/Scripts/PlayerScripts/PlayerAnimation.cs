using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    GameObject player;
    Animator pAnimator;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        pAnimator = player.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void MovePlayer(float horizontalInput, float verticalInput, Vector2 lookDir)
    {
        // Player Animation
        pAnimator.SetFloat("Speed", Mathf.Max(Mathf.Abs(horizontalInput), Mathf.Abs(verticalInput)));
        pAnimator.SetFloat("Horizontal", lookDir.x);
        pAnimator.SetFloat("Vertical", lookDir.y);
    }

    public void PlayerAttack(){
        pAnimator.SetTrigger("Attack");
    }

    public void PlayerDeath(){
        pAnimator.SetTrigger("Death");
    }
}
