using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D attackCollider;
    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
    }

    void attackLeft(){
        attackCollider.enabled = true;
    }
    void attackRight() {
            attackCollider.enabled = true;
    }
    void attackTop() {
            attackCollider.enabled = true;
    }
    void attackBottom() {
        attackCollider.enabled = true;
    }
    void stopAttacking() {
        attackCollider.enabled = false;
    }
}
