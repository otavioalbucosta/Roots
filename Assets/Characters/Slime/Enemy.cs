using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public Animator animator;
    public float damage = 1f;
    public float moveSpeed = 50f;
    public float knockbackForce = 100f;
    Rigidbody2D rb;
    public DetectionZone detectionZone;
    private bool moveEnabled = true;

    public float Health {
        set {
            health = value;
            if (health <= 0){
                Defeated();
            }
        }
        get {
            return health;
        }

    }
    private float health = 4f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   
        if (detectionZone.detectedObjs.Count > 0 && moveEnabled == true){
            Vector2 direction = (detectionZone.detectedObjs[0].transform.position - transform.position).normalized;

            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (direction * Time.fixedDeltaTime * moveSpeed), moveSpeed);
        }
    }

    public void OnHit(float damage, Vector2 knockback){
        Health -= damage;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(knockback);


        animator.SetTrigger("tookDamage");
        print(Health);
    }

    public void OnHit(float damage){
        Health -= damage;
    }

    public void disableMove(){
        moveEnabled = false;
    }
    public void enableMove(){
        moveEnabled = true;
    }

    void Defeated() {
        rb.simulated = false;
        animator.SetTrigger("Defeated");
    }

    void RemoveEnemy() {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        IDamageable collider = other.collider.GetComponent<IDamageable>();

        Vector3 pos = GetComponent<Transform>().position;
        
        Vector2 direction = (Vector2) (other.collider.gameObject.transform.position - pos).normalized;

        Vector2 knockbackDirection = direction * knockbackForce;
        if (other.collider.gameObject.tag == "Player"){
            collider.OnHit(damage,knockbackDirection);
        }
    }
}
