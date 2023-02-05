using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDamageable
{
    public Vector2 movement;
    Rigidbody2D rb;
    public float moveSpeed = 10f;
    public float maxSpeed = 2f;
    public float idleFriction = 0.9f;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool allowedToMove = true;
    private float _health = 3f;
    private float iFrameTime = 1f;
    private bool iFrameActivated = false;
    private float elapsedTime = 0f;

    public GameOverScreen gameOver;

    public SwordAttack sword;

    public float Health { 
        get {
            return _health;
        } set {
            _health = value;
            if (_health <=0){
                HasDefeated();
            }
        } 
        }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update() {
        if(iFrameActivated){
            elapsedTime += Time.deltaTime;
            if (elapsedTime > iFrameTime){
                iFrameActivated = false;
                elapsedTime = 0f;
            }
        }
    }

    private void FixedUpdate() {
        if (movement != Vector2.zero && allowedToMove == true) {
            animator.SetBool("isMoving",true);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (movement * Time.fixedDeltaTime * moveSpeed), maxSpeed);
            if(movement.x < 0 ){
                spriteRenderer.flipX = true;
            }else{
                spriteRenderer.flipX = false;
            }
            setAttackDirection(movement);

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);

        }else if(movement == Vector2.zero && allowedToMove == true){

            rb.velocity = Vector2.Lerp( rb.velocity ,Vector2.zero, idleFriction);
            animator.SetBool("isMoving",false);

        }else{
            animator.SetBool("isMoving",false);
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
        print("foi");
        allowedToMove = true;
        sword.stopAttacking();
    }
    public void disableMove(){
        allowedToMove = false;
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        if(!iFrameActivated){
            
            Health -= damage;
            print(Health);
            rb.velocity = Vector2.zero;

            rb.AddForce(knockback);
            while(rb.velocity!= Vector2.zero){
                allowedToMove = false;
            }
            allowedToMove = true;
            iFrameActivated = true;
        }

    }

    public void OnHit(float damage)
    {
        if(!iFrameActivated){

            Health -= damage;
            print(Health);
            iFrameActivated = true;
        }
    }

    private void HasDefeated(){
        print("morri");
        Destroy(gameObject);
        gameOver.Setup();
    }
}
