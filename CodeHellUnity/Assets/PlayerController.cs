using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput;

    Rigidbody2D rb;

    public Animator animator;

    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    // void Update()
    // {

    // }

    void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool succ = tryMove(movementInput);

            if (!succ)
            {
                succ = tryMove(new Vector2(movementInput.x, 0));

                if (!succ)
                {
                    succ = tryMove(new Vector2(0, movementInput.y));
                }
            }
            animator.SetBool("isMovingHor", succ);
        } else {
            animator.SetBool("isMovingHor", false);
            animator.SetBool("isMovingDown", false);
            animator.SetBool("isMovingUp", false);
            animator.SetBool("LeftRight", false);
        }
        if (movementInput.y > 0)
        {
            animator.SetBool("isMovingUp", true);
            animator.SetBool("isMovingDown", false);

        } else if (movementInput.y < 0) {
            animator.SetBool("isMovingUp", false);
            animator.SetBool("isMovingDown", true);
        }
        if (movementInput.y != 0 && movementInput.x == 0)
        {
            animator.SetBool("LeftRight", false);
        }
        if (movementInput.x < 0) {
            spriteRenderer.flipX = true;
            animator.SetBool("LeftRight", true);
        } else {
            spriteRenderer.flipX = false;
            animator.SetBool("LeftRight", true);
        }
    }

    bool tryMove(Vector2 direction)
    {
        int count = rb.Cast(direction, movementFilter, castCollisions, moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0) {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        // Vector3 movement = new Vector3(inputVector.x, 0, inputVector.y);
        // transform.Translate(movement * Time.deltaTime);
    }
}
