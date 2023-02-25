using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishy : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private Transform sprite;

    [SerializeField]
    private Transform fin;
    private Vector2 heading = Vector2.right;
    private float speed = 0.0f;

    private float boostReserve = 0.0f;
    private float lastPress = -10.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0)
        {
            var up = Input.GetAxis("Vertical");
            var angle = Time.deltaTime * 100.0f * speed / 5.0f;
            if (up > 0.01f)
            {
                if (Vector2.SignedAngle(transform.right, sprite.right) < 45)
                {
                    sprite.Rotate(Vector3.forward, angle);
                    heading = sprite.right;
                }
            }
            else if (up < -0.01f)
            {
                if (Vector2.SignedAngle(transform.right, sprite.right) > -45)
                {
                    sprite.Rotate(Vector3.forward, -angle);
                    heading = sprite.right;
                }
            }
        }
        else
        {
            heading = heading + Vector2.down * 2.0f * Time.deltaTime;
            sprite.right = heading;
        }


        if (Input.GetKey(KeyCode.Space))
        {
            boostReserve -= Time.deltaTime * 2.0f;
            var x = 1.0f - boostReserve;
            var boost = -1.0f * Mathf.Pow(2.0f * x - 0.95f, 2) + 1.0f;
            speed = speed + 4.0f * boost * Time.deltaTime;
            Debug.Log(boost);
        }
        else
        {
            if (boostReserve < 0.0f) boostReserve = 0.0f;
            boostReserve += Time.deltaTime * 3.5f;
            if (boostReserve > 1.0f) boostReserve = 1.0f;

            if (boostReserve > 0.99f)
            {
                speed -= Time.deltaTime * 5.0f;
            }
        }


        if (speed > 10.0f) speed = 10.0f;
        if (speed < 1.0f) speed = 1.0f;

        if (transform.position.y < -4.9f && heading.y < 0.0f)
        {
            var t = (transform.position.y + 5.0f) / 0.1f;
            heading = new Vector2(heading.x, heading.y * t);
            sprite.right = heading;
        }

        var r = boostReserve * 2.0f - 1.0f;
        if (r < -1.0f) r = -1.0f;
        fin.right = sprite.right + sprite.TransformDirection(new Vector2(2.0f, r * 1.0f));
    }

    void FixedUpdate()
    {
        rb.velocity = heading * speed;
    }
}
