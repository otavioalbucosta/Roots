using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 HorizontalOffset;
    Vector2 HorizontalSize;
    Vector2 VerticalBottomOffset = new Vector2(0.03f,-0.17f);
    Vector2 VerticalTopOffset = new Vector2(0, -0.05f);
    Vector2 VerticalSize = new Vector2(0.1973216f,0.144767f);


    BoxCollider2D attackCollider;
    void Start()
    {
        attackCollider = GetComponent<BoxCollider2D>();
        HorizontalOffset = attackCollider.offset;
        HorizontalSize = attackCollider.size;
    }

    void attackLeft(){
        attackCollider.enabled = true;
        attackCollider.offset = new Vector2(-HorizontalOffset.x, HorizontalOffset.y);
        attackCollider.size = HorizontalSize;
    }
    void attackRight() {
        attackCollider.enabled = true;
        attackCollider.offset = HorizontalOffset;
        attackCollider.size = HorizontalSize;
    }
    void attackTop() {
        attackCollider.enabled = true;
        attackCollider.offset = VerticalTopOffset;
        attackCollider.size = VerticalSize;
    }
    void attackBottom() {
        attackCollider.enabled = true;
        attackCollider.offset = VerticalBottomOffset;
    }
    void stopAttacking() {
        attackCollider.enabled = false;
    }
}
