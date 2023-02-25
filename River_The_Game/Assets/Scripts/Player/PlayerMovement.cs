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
    private float startFallingBuffer = 1f;

    [SerializeField]
    private Transform backToWaterPoint;

    private Vector2 input;

    private bool isAboveWater = false;
    private bool isFalling = false;

    private bool isAllowedToMove = true;

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
    }

    private void Start()
    {
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
        transform.position = WorldManager.main.PlayerStart.position;
    }

    private void CheckAboveWaterStatus()
    {
        isAboveWater = transform.position.y > waterSurface.position.y;
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
    }

    private void FaceDirection()
    {
        if (isAboveWater)
        {
            Vector2 dir = rb.velocity;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.rotation = Quaternion.identity;
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
        if (!isAboveWater)
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
