using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Animator animator;

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
    private float health = 1f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void takeDamage(float damage){
        Health -= damage;
    }

    void Defeated() {
        animator.SetTrigger("Defeated");
    }

    void RemoveEnemy() {
        Destroy(gameObject);
    }
}
