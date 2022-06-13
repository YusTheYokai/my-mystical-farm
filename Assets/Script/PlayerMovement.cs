using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public static PlayerMovement Instance;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    private Vector2 movement;
    private float moveSpeed = 10;
    private float maxStamina = 250f;
    private float currentStamina = 250f;
    private const float sprintStat = 20;
    private const float walking = 10;
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() { 
        Physics2D.IgnoreLayerCollision(3,6);
    }

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

    public Vector2 getPosition() {
        return rb.position;
    }
}
