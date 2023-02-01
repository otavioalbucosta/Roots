using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 movement;
    Rigidbody2D rb;
    public float moveSpeed = 1;
    public float collisonOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    private bool allowedToMove = true;

    public SwordAttack sword;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame


    private void FixedUpdate() {
        if (movement != Vector2.zero && allowedToMove == true) {
            animator.SetBool("isMoving",true);
            if(movement.x < 0 ){
                spriteRenderer.flipX = true;
            }else{
                spriteRenderer.flipX = false;
            }
            setAttackDirection(movement);
            if (canMove(movement)){
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);


            }else if(canMove(new Vector2(movement.x,0))){
                rb.MovePosition(rb.position + new Vector2(movement.x,0) * moveSpeed * Time.fixedDeltaTime);
                animator.SetFloat("Horizontal", movement.x);

            }else if(canMove(new Vector2(0,movement.y))){
                rb.MovePosition(rb.position + new Vector2(0,movement.y) * moveSpeed * Time.fixedDeltaTime);
                animator.SetFloat("Vertical", movement.y);


            }else{
                animator.SetBool("isMoving",false);
            }
    }else{
            animator.SetBool("isMoving",false);
        }

    }
    bool canMove(Vector2 direction){
        if (direction != Vector2.zero){
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisonOffset);
            if(count == 0){
                return true;
            }else{
                return false;
            }
        }else {
            return false;
        }
    }
    void OnMove(InputValue movementInput) {
        movement = movementInput.Get<Vector2>();
    }

    void OnFire() {
        animator.SetTrigger("PressAtack");
        sword.Attack();
    }

    void setAttackDirection(Vector2 lastMovement) {
        if(lastMovement.x > 0 || (lastMovement.x>0 && lastMovement.y != 0)){
            sword.attackDirection = SwordAttack.AttackDirection.right;
        }else if(lastMovement.x < 0 || (lastMovement.x<0 &&lastMovement.y != 0)){
            sword.attackDirection = SwordAttack.AttackDirection.left;
        }
        else if(lastMovement.y > 0){
            sword.attackDirection = SwordAttack.AttackDirection.top;
        }else if(lastMovement.y < 0){
            sword.attackDirection = SwordAttack.AttackDirection.bottom;

        }
    }

    public void enableMove(){
        allowedToMove = true;
        sword.stopAttacking();
    }
    public void disableMove(){
        allowedToMove = false;
    }
}
