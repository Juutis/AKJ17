using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Transform waterSurface;

    [SerializeField]
    private Transform backToWaterPoint;

    private Vector2 input;

    private bool isAboveWater = false;
    private bool isFalling = false;

    private void Update()
    {
        CollectInput();
        CheckAboveWaterStatus();
        if (isFalling)
        {
            fallSpeedTimer += Time.deltaTime;
        }
        FaceDirection();
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
            if (fallSpeedTimer >= fallSpeedDuration && transform.position.y < backToWaterPoint.position.y)
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
        if (!isAboveWater && !isFalling)
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
        velocity.y -= Mathf.Lerp(fallSpeed, 0f, fallSpeedTimer / fallSpeedDuration) * Time.deltaTime;
        rb.velocity = velocity;
    }
}
