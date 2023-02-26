using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFish : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var world = GameObject.FindGameObjectWithTag("MovingWorld");
        transform.parent = world.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity.y < 0)
        {
            var targetDir = rb.velocity;
            var diff = Vector2.SignedAngle(transform.right, targetDir);
            if (Mathf.Abs(diff) > 10.0f)
            {
                var rotateSpeed = 720.0f;
                var rotation = Mathf.Min(rotateSpeed * Time.deltaTime, Mathf.Abs(diff));
                transform.Rotate(Vector3.forward, Mathf.Sign(diff) * rotation);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y < 0.0f)
        {
            rb.velocity = Vector2.zero;
            rb.gravityScale = 0.0f;
            rb.freezeRotation = true;
            rb.simulated = false;
        }
    }
}
