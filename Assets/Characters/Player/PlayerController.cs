using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;
    public float moveSpeed = 1;
    public float collisonOffset = 0.05f;
    public ContactFilter2D movementFilter;
    public List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if (movement != Vector2.zero) {
            int count = rb.Cast(
                movement,
                movementFilter,
                castCollisions,
                (moveSpeed * Time.fixedDeltaTime) + collisonOffset);
            Debug.Log(count);
            if(count == 0){
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            }

        }
    }
    void OnMove(InputValue movementInput) {
        movement = movementInput.Get<Vector2>();
    }
}
