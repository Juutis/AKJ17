using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 3;
    private int currentHealth => GameManager.main.PlayerHP;

    private Shooting shooting;
    private PlayerMovement playerMovement;
    private Collider2D playerCollider;
    private Rigidbody2D rigidBody;

    private bool isInvulnerable = false;

    [SerializeField]
    private float invulnerabilityLength = 0.5f;
    private float invulnerabilityTimer = 0f;
    [SerializeField]
    private float controlLossDuration = 0.5f;
    private bool controlsLost = false;
    private float controlLossTimer = 0f;

    private SpriteFlasher spriteFlasher;

    [SerializeField]
    private GameObject deadFish;

    private void Start()
    {
        spriteFlasher = GetComponent<SpriteFlasher>();
        //currentHealth = startingHealth;
        playerMovement = GetComponent<PlayerMovement>();
        shooting = GetComponent<Shooting>();
        playerCollider = GetComponent<Collider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Initialize()
    {
        Debug.Log(GameManager.main.PlayerHP);
        UIManager.main.SetLives(GameManager.main.PlayerHP);
    }

    public void GainLife()
    {
        //currentHealth += 1;
        UIManager.main.GainLife();
    }

    public void TakeDamage(int damage = 1)
    {
        if (isInvulnerable)
        {
            return;
        }
        Debug.Log($"[PlayerHealth]: Hp {currentHealth} -> {currentHealth - damage}");
        GameManager.main.TakeDamage();
        SoundManager.main.PlaySound(GameSoundType.PlayerHurt);
        if (currentHealth <= 0)
        {
            GameManager.main.GameOver();
            return;
        }

        
        var corpse = Instantiate(deadFish);
        corpse.transform.position = transform.position;
        corpse.transform.rotation = transform.rotation;
        var rb = corpse.GetComponent<Rigidbody2D>();
        rb.velocity = rigidBody.velocity / 2.0f;

        UIManager.main.LoseLife();
        playerMovement.Spawn();
        LoseControls();

        isInvulnerable = true;
        invulnerabilityTimer = 0f;
        spriteFlasher.StartFlashing();
    }

    private void LoseControls()
    {
        playerMovement.DisableControls();
        shooting.DisableControls();
        controlLossTimer = 0f;
        controlsLost = true;
    }

    private void GainControls()
    {
        playerMovement.EnableControls();
        shooting.EnableControls();
        controlLossTimer = 0f;
        controlsLost = false;
    }

    private void Update()
    {
        if (controlsLost)
        {
            controlLossTimer += Time.deltaTime;
            if (controlLossTimer >= controlLossDuration)
            {
                GainControls();
            }
        }
        if (isInvulnerable)
        {
            invulnerabilityTimer += Time.deltaTime;
            if (invulnerabilityTimer > invulnerabilityLength)
            {
                spriteFlasher.StopFlashing();
                isInvulnerable = false;
            }
        }
    }


}