using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 3;
    private int currentHealth = 3;

    private PlayerMovement playerMovement;
    private Collider2D playerCollider;

    private bool isInvulnerable = false;

    [SerializeField]
    private float invulnerabilityLength = 0.5f;
    private float invulnerabilityTimer = 0f;


    private SpriteFlasher spriteFlasher;

    private void Start()
    {
        spriteFlasher = GetComponent<SpriteFlasher>();
        currentHealth = startingHealth;
        playerMovement = GetComponent<PlayerMovement>();
        playerCollider = GetComponent<Collider2D>();
    }
    public void TakeDamage(int damage = 1)
    {
        Debug.Log($"[PlayerHealth]: Hp {currentHealth} -> {currentHealth - damage}");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.main.GameOver();
            return;
        }

        playerMovement.Spawn();
        playerMovement.DisableControls();
        isInvulnerable = true;
        invulnerabilityTimer = 0f;
        spriteFlasher.StartFlashing();
    }

    private void Update()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > invulnerabilityLength)
            {
                spriteFlasher.StopFlashing();
                playerMovement.EnableControls();
            }
        }
    }


}