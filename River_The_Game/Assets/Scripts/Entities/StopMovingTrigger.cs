using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMovingTrigger : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr)
        {
            sr.enabled = false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Stop moving");
        WorldManager.main.StopWorldMovement();
        Destroy(gameObject);
    }
}
