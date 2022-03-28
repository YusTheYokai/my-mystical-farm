using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed = 1;//0.5f

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    float maxStamina = 250f;
    float currentStamina = 250f;
    public const float sprintStat = 2;
    public const float walking = 1;//0.5f
    // Update is called once per frame
    void Update() {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude); //Optimized to length of a movement vector squared
        
    }

    void FixedUpdate() {
        //Movement
        Debug.Log(currentStamina);
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (currentStamina > 0) {
                moveSpeed = sprintStat;
                currentStamina--;
            }
            else {
               moveSpeed = walking; 
            } 
        }
        else {
            moveSpeed = walking;
            if (currentStamina < maxStamina) {
                currentStamina++;
            }  
        }


        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); //fixedDeltaTime
    }
    
}
