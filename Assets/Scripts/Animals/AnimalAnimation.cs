using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimation : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private FeedableAnimal feedableAnimal;

    public float moveSpeed;
    private Rigidbody2D rb;
    public bool isWalking;

    public Vector2 facing;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int walkDirection;

    public Animator animator;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        feedableAnimal = GetComponent<FeedableAnimal>();
        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();
    }
 
 
    void Update () {
        if (animator.enabled && spriteRenderer.sprite == feedableAnimal.getFed()) {
            animator.enabled = false;
        }

        if (isWalking == false) {
            facing.x = 0;
            facing.y = 0;
        }

        if (isWalking == true) {
            walkCounter -= Time.deltaTime;
            
            switch (walkDirection) {

                case 0:
                    rb.velocity = new Vector2(0, moveSpeed);
                    facing.y = 1;
                    facing.x = 0;
                    break;

                case 1:
                    rb.velocity = new Vector2(moveSpeed, 0);
                    facing.x = 1;
                    facing.y = 0;
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    break;

                case 2:
                    rb.velocity = new Vector2(0, -moveSpeed);
                    facing.y = -1;
                    facing.x = 0;
                    break;

                case 3:
                    rb.velocity = new Vector2(-moveSpeed, 0);
                    facing.y = 0;
                    facing.x = -1;
                    transform.localScale = new Vector3(-1f, 1f, 1f);

                    break;
            }

            if (walkCounter < 0) {
                isWalking = false;
                waitCounter = waitTime;
            }
        } else {
            rb.velocity = Vector2.zero;
            waitCounter -= Time.deltaTime;

            if (waitCounter < 0) {
                ChooseDirection();

            }
        }

        animator.SetFloat("Horizontal", facing.x);
        animator.SetFloat("Vertical", facing.y);
        animator.SetBool("Walking", isWalking);
    }

    public void ChooseDirection() {
        walkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}