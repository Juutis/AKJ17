using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float fallSpeed = 400f;
    [SerializeField]
    private float fallSpeedDuration = 1f;
    private float fallSpeedTimer = 0f;
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private TMPro.TextMeshProUGUI txtDebug;

    [SerializeField]
    private Transform waterSurface;

    [SerializeField]
    private Transform backToWaterPoint;

    private Vector2 input;

    private bool isAboveWater = false;

    public bool IsAboveWater { get { return isAboveWater; } }
    private bool isFalling = false;
    [SerializeField]
    private float fallBuffer = 1f;

    private bool isAllowedToMove = true;

    [SerializeField]
    private GameObject waterSplash;

    private float lastY = 0;
    private bool spawning = false;
    private Collider2D coll;

    private void Update()
    {
        CollectInput();
        CheckAboveWaterStatus();
        if (isFalling)
        {
            fallSpeedTimer += Time.deltaTime;
        }
        FaceDirection();
        if (txtDebug != null)
        {
            txtDebug.text = $"[isFalling: {isFalling}]\n[isAboveWater: {isAboveWater}] [velY: {rb.velocity.y}]\n[fallSpeedTimer: {fallSpeedTimer}]";
        }

        if (spawning)
        {
            var t = Time.deltaTime * 5.0f;
            transform.position = Vector2.MoveTowards(transform.position, WorldManager.main.PlayerStart.position, t);
            if (Vector2.Distance(transform.position, WorldManager.main.PlayerStart.position) < 0.5f)
            {
                spawning = false;
                coll.enabled = true;
            }
        }
    }

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        Spawn();
    }

    public void DisableControls()
    {
        rb.velocity = Vector2.zero;
        isAllowedToMove = false;
    }
    public void EnableControls()
    {
        isAllowedToMove = true;
    }

    public void Spawn()
    {
        transform.position = WorldManager.main.PlayerStart.position + Vector3.left * 10.0f;
        spawning = true;
        coll.enabled = false;
    }

    private void CheckAboveWaterStatus()
    {
        if (!isAboveWater)
        {
            isAboveWater = transform.position.y > waterSurface.position.y;
        }
        if (isAboveWater)
        {
            if (transform.position.y < (waterSurface.position.y - fallBuffer))
            {
                isAboveWater = false;
            }
        }
        if (!isFalling && isAboveWater)
        {
            isFalling = true;
            fallSpeedTimer = 0f;
        }
        if (isFalling)
        {
            if (fallSpeedTimer >= fallSpeedDuration)
            {
                isFalling = false;
            }
        }
        bool goingUp = lastY < waterSurface.position.y && transform.position.y >= waterSurface.position.y;
        bool goingDown = lastY > waterSurface.position.y && transform.position.y <= waterSurface.position.y;
        if (goingUp || goingDown)
        {
            var splash = Instantiate(waterSplash);
            splash.transform.position = transform.position;
            if (goingUp)
            {
                SoundManager.main.PlaySound(GameSoundType.Jump);
            }
            else if (goingDown)
            {
                SoundManager.main.PlaySound(GameSoundType.Splash);
            }
        }

        lastY = transform.position.y;
    }

    private void FaceDirection()
    {
        if (isAboveWater)
        {
            Vector2 dir = rb.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            if (dir.x < 0.0f)
            {
                transform.localScale = new Vector3(transform.localScale.x, -1.0f, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
            }
        }
        else
        {
            transform.rotation = Quaternion.identity;
            transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
        }

    }


    private void CollectInput()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (!isAllowedToMove)
        {
            return;
        }
        if (!isAboveWater && !spawning)
        {
            ApplyMovement();
        }
        if (isFalling)
        {
            Fall();
        }
    }

    private void ApplyMovement()
    {
        rb.velocity = input * speed * Time.deltaTime;
    }

    public void Fall()
    {
        Vector2 velocity = rb.velocity;
        velocity.y -= Mathf.Lerp(0f, fallSpeed, fallSpeedTimer / fallSpeedDuration) * Time.deltaTime;
        //velocity.y -= fallSpeed * Time.deltaTime;
        rb.velocity = velocity;
    }
}
