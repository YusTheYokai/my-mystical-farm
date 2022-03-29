using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator animator;
    public Vector2 movement;
    private float moveSpeed = 1;//0.5f
    private float maxStamina = 250f;
    private float currentStamina = 250f;
    private const float sprintStat = 2;
    private const float walking = 1;
    
    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude); //Optimized to length of a movement vector squared
    }

    void FixedUpdate() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            if (currentStamina > 0) {
                moveSpeed = sprintStat;
                currentStamina--;
            } else {
               moveSpeed = walking; 
            } 
        } else {
            moveSpeed = walking;
            if (currentStamina < maxStamina) {
                currentStamina++;
            }  
        }
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
