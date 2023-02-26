using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool floatUp;
    [SerializeField]
    private float lifeTime;
    [SerializeField]
    private float damage;

    private float started;
    private bool isAlive = true;
    private bool floatWobble = false;
    private float startWobble;
    private Vector3 dir;

    private Rigidbody2D rbody;

    private ParticleSystem effect;

    // Start is called before the first frame update
    void Start()
    {
        started = Time.time;
        rbody = GetComponent<Rigidbody2D>();
        effect = GetComponentInChildren<ParticleSystem>();
    }

    public void Initialize(Vector3 direction)
    {
        dir = direction;
    }

    void Update()
    {
        if (rbody.velocity.magnitude > 0.01f)
        {
            transform.right = rbody.velocity;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            // transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
            rbody.velocity = dir * speed;
        }
        else if (!isAlive && floatUp)
        {
            // GetComponent<SpriteRenderer>().color = Color.red;
            float velX = rbody.velocity.x;
            float velY = rbody.velocity.y;

            if (rbody.velocity.x > 0 && !floatWobble)
            {
                velX *= 0.93f;
            }

            if (velX < 0.35f && !floatWobble)
            {
                floatWobble = true;
                startWobble = Time.time;
            }

            velY += 0.10f;
            rbody.velocity = new Vector2(velX, Mathf.Clamp(velY, -3f, 3f));

            if (transform.position.y >= 5)
            {
                Kill();
            }
        }

        if (Time.time - started >= lifeTime) {
            isAlive = false;
            if (!floatUp)
            {
                Kill();
            }
            else
            {

            }
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            Kill();
        }
    }

    private void Kill()
    {
        if (effect != null)
        {
            effect.Stop();
            effect.transform.parent = null;
        }
        Destroy(gameObject);
    }
}
