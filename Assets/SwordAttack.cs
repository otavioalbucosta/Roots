using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 HorizontalOffset;
    Vector2 HorizontalSize;
    Vector2 VerticalBottomOffset = new Vector2(0.03f,-0.17f);
    Vector2 VerticalTopOffset = new Vector2(0, 0.05f);
    Vector2 VerticalSize = new Vector2(0.1973216f,0.144767f);

    public float damage = 1f;
    public float knockbackForce = 10f;
    
    BoxCollider2D attackCollider;


    public enum AttackDirection {
        left,right,top,bottom
    }

    public AttackDirection attackDirection;

    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        HorizontalOffset = attackCollider.offset;
        HorizontalSize = attackCollider.size;
    }

    public void Attack() {
        switch(attackDirection){
            case AttackDirection.left: 
                attackLeft();
                break;
            case AttackDirection.right:
                attackRight();
                break;
            case AttackDirection.top:
                attackTop();
                break;
            case AttackDirection.bottom:
                attackBottom();
                break;
        }
    }


    void attackLeft(){
        print("left");
        attackCollider.enabled = true;
        attackCollider.offset = new Vector2(-HorizontalOffset.x, HorizontalOffset.y);
        attackCollider.size = HorizontalSize;
    }
    void attackRight() {
        print("right");
        attackCollider.enabled = true;
        attackCollider.offset = HorizontalOffset;
        attackCollider.size = HorizontalSize;
    }
    void attackTop() {
        print("top");
        attackCollider.enabled = true;
        attackCollider.offset = VerticalTopOffset;
        attackCollider.size = VerticalSize;
    }
    void attackBottom() {
        print("bottom");
        attackCollider.enabled = true;
        attackCollider.offset = VerticalBottomOffset;
        attackCollider.size = VerticalSize;
    }
    public void stopAttacking() {
        attackCollider.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {

        IDamageable collider = other.collider.GetComponent<IDamageable>();
        print("Hit");
        // other.SendMessage("OnHit", damage);
        if(collider != null ){
            Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

            Vector2 direction = (Vector2) (other.collider.gameObject.transform.position - parentPosition ).normalized;

            Vector2 knockbackDirection = direction * knockbackForce;
            collider.OnHit(damage, knockbackDirection);
        }

    }

}
