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
    private Vector3 lastPosition;
    private Vector3 velocity;
    [SerializeField]
    private ParticleSystem deathEffect;
    private EnemyAnimator anim;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        movement = GetComponent<EnemyMovement>();
        Alive = true;
        anim = GetComponentInChildren<EnemyAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive && velocity.y < 0)
        {
            var targetDir = rigidBody.velocity;
            var diff = Vector2.SignedAngle(-transform.right, targetDir);
            var rotateSpeed = 720.0f;
            var rotation = Mathf.Min(rotateSpeed * Time.deltaTime, Mathf.Abs(diff));
            transform.Rotate(Vector3.forward, Mathf.Sign(diff) * rotation);
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
            Kill();
        }
    }

    public void Kill()
    {
        if (deathSprite != null)
        {
            renderer.sprite = deathSprite;
        }
        Alive = false;
        rigidBody.gravityScale = 1.0f;
        if (movement)
        {
            movement.enabled = false;
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
    }

    public void RegisterGroup(EnemyGroup group)
    {
        enemyGroup = group;
    }

    private void OnDestroy()
    {
        PickupDrop drop = GetComponent<PickupDrop>();
        if (drop != null)
        {
            drop.Drop(transform.position);
        }
        if (enemyGroup != null)
        {
            enemyGroup.EnemyKilled(this);
        }
    }
}
