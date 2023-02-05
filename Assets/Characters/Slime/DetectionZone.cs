using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public string tagTarget = "Player";

    Collider2D col;


    void Start()
    {
        col = GetComponent<Collider2D>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == tagTarget){
            detectedObjs.Add(other);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == tagTarget){
            detectedObjs.Remove(other);
        }
    }
}
