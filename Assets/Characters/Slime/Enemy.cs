using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{

    public Animator animator;
    Rigidbody2D rb;

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
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void Defeated() {
        animator.SetTrigger("Defeated");
    }

    void RemoveEnemy() {
        Destroy(gameObject);
    }
}
