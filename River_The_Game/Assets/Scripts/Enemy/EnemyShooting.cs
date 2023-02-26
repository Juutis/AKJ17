using Mono.Cecil;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private ShootingType type;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shootingRate;

    [SerializeField]
    private float damage;

    [Header("Constant direction type")]
    [SerializeField]
    private Vector2 direction;

    [Header("Omnidirectional type")]
    [SerializeField]
    private float directionCount;

    [Header("Spread type")]
    [SerializeField]
    private float bulletCount;
    [SerializeField]
    private float spreadAngle;
    [SerializeField]
    private Vector2 midDirection;

    [Header("Generic animation")]
    public float FireDelay = 0.0f;

    private float lastShot;
    private float shootDelay;
    private Transform player;
    private bool shootingEnabled;
    private float initialShootDelay = 2f; // wait a second before starting to shoot
    private List<Vector2> bulletDirections = new();


    private EnemyAnimator anim;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        shootDelay = 60 / shootingRate;
        player = FindObjectOfType<PlayerMovement>().transform;
        shootingEnabled = false;

        if (type == ShootingType.Omnidirectional)
        {
            float angleBetweenDirs = 360f / directionCount;
            for (int i = 0; i < directionCount; i++)
            {
                bulletDirections.Add(Quaternion.Euler(0, 0, angleBetweenDirs * i) * Vector2.left);
            }
        }
        else if (type == ShootingType.Spread)
        {
            float angleFromMid = spreadAngle / 2f;
            float angleBetweenDirs = spreadAngle / (bulletCount-1);
            Vector2 startDir = Quaternion.Euler(0, 0, -angleFromMid) * midDirection;
            Debug.Log($"Start: {startDir}, mid: {midDirection}");
            for (int i = 0; i < bulletCount; i++)
            {
                bulletDirections.Add(Quaternion.Euler(0, 0, angleBetweenDirs * i) * startDir);
                Debug.Log($"Dir: {Quaternion.Euler(0, 0, angleBetweenDirs * i) * startDir}");
            }
        }
        else if (type == ShootingType.ConstantDirection)
        {
            direction = direction.normalized;
        }

        anim = GetComponentInChildren<EnemyAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!shootingEnabled)
        {
            return;
        }
        if (!enabled)
        {
            return;
        }

        if (Time.time - lastShot < shootDelay)
        {
            return;
        }

        if (type == ShootingType.ConstantDirection)
        {
            lastShot = Time.time;
            Invoke("Fire", FireDelay);
            anim.Shoot();
        }
        else if (type == ShootingType.OnPlayerY)
        {
            bool isCloseToPlayerY = transform.position.y < player.position.y + 0.1f && transform.position.y > player.position.y - 0.1f;
            if (isCloseToPlayerY)
            {
                lastShot = Time.time;
                Invoke("Fire", FireDelay);
                anim.Shoot();
            }
        }
        else if (type == ShootingType.Omnidirectional || type == ShootingType.Spread)
        {
            foreach(Vector2 dir2 in bulletDirections)
            {
                lastShot = Time.time;
                Invoke("Fire", FireDelay);
                anim.Shoot();
            }
        }
    }

    public void Fire()
    {
        if (type == ShootingType.ConstantDirection)
        {
            Vector3 dir = new Vector3(direction.x, direction.y, 0);
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + dir;
            bullet.GetComponent<Bullet>().Initialize(dir);
        }
        else if (type == ShootingType.OnPlayerY)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + Vector3.left;
            bullet.GetComponent<Bullet>().Initialize(Vector3.left);
        }
        else if (type == ShootingType.Omnidirectional || type == ShootingType.Spread)
        {
            foreach(Vector2 dir2 in bulletDirections)
            {
                Vector3 dir = new Vector3(dir2.x, dir2.y, 0);
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = transform.position + dir;
                bullet.GetComponent<Bullet>().Initialize(dir);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyActivator")
        {
            shootingEnabled = true;
            lastShot = Time.time + initialShootDelay;
        }
    }
}

enum ShootingType
{
    OnPlayerY,
    ConstantDirection,
    Omnidirectional,
    Spread
}
