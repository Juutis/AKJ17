using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy.Alive)
            {
                playerHealth.TakeDamage();
            }
        }
    }
}
