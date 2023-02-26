using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp;

    private EnemyGroup enemyGroup;

    private SpriteRenderer renderer;
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Sprite deathSprite;

    public bool Alive = true;
    private EnemyMovement movement;
    private FollowEnemyMovement movement2;
    private EnemyShooting shooting;
    private Vector3 lastPosition;
    private Vector3 velocity;
    [SerializeField]
    private ParticleSystem deathEffect;
    private SpriteFlasher spriteFlasher;
    private EnemyAnimator anim;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovement>();
        movement2 = GetComponent<FollowEnemyMovement>();
        shooting = GetComponent<EnemyShooting>();
        spriteFlasher = GetComponent<SpriteFlasher>();
        anim = GetComponentInChildren<EnemyAnimator>();
        collider = GetComponent<Collider2D>();
        Alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            renderer.color = Color.white;
        }
        if (!Alive && velocity.y < 0)
        {
            var targetDir = rigidBody.velocity;
            var diff = Vector2.SignedAngle(-transform.right, targetDir);
            if (Mathf.Abs(diff) > 10.0f)
            {
                var rotateSpeed = 720.0f;
                var rotation = Mathf.Min(rotateSpeed * Time.deltaTime, Mathf.Abs(diff));
                transform.Rotate(Vector3.forward, Mathf.Sign(diff) * rotation);
            }
        }
    }

    void LateUpdate()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    public void TakeDamage(float damage)
    {
        if (!Alive)
        {
            return;
        }

        hp -= damage;
        if (hp <= 0)
        {
            if (spriteFlasher != null)
            {
                spriteFlasher.StopFlashing();
            }
            Kill();
        }
        else
        {
            if (spriteFlasher != null)
            {
                spriteFlasher.StartFlashing();
            }
        }
    }

    public void Kill()
    {
        renderer.sprite = deathSprite;
        renderer.color = Color.white;
        Alive = false;
        rigidBody.gravityScale = 0.4f;
        if (movement != null)
        {
            movement.enabled = false;
        }
        if (movement2 != null)
        {
            movement2.enabled = false;
        }
        rigidBody.velocity = velocity;
        if (deathEffect != null)
        {
            var fx = Instantiate(deathEffect);
            fx.transform.position = transform.position;
        }
        if (anim != null)
        {
            anim.Disable();
        }
        if (shooting != null)
        {
            shooting.enabled = false;
        }

        PickupDrop drop = GetComponent<PickupDrop>();
        if (drop != null)
        {
            drop.Drop(transform.position);
        }
        if (enemyGroup != null)
        {
            enemyGroup.EnemyKilled(this);
        }
        gameObject.layer = LayerMask.NameToLayer("Dead Enemy");
        if (collider != null)
        {
            collider.isTrigger = false;
        }
    }

    public void RegisterGroup(EnemyGroup group)
    {
        enemyGroup = group;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y < 0.0f)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.gravityScale = 0.0f;
            rigidBody.freezeRotation = true;
            rigidBody.simulated = false;
        }
    }
}
