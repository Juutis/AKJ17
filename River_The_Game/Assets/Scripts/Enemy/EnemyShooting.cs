using System.Collections.Generic;
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

    [Header("Omnidirectional type")]
    [SerializeField]
    private float directionCount;

    private float lastShot;
    private float shootDelay;
    private Transform player;
    private bool shootingEnabled;
    private float initialShootDelay = 2f; // wait a second before starting to shoot
    private List<Vector2> omniDirections = new();

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
                omniDirections.Add(Quaternion.Euler(0, 0, angleBetweenDirs * i) * Vector2.left);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!shootingEnabled)
        {
            return;
        }

        if (Time.time - lastShot < shootDelay)
        {
            return;
        }

        if (type == ShootingType.Forward)
        {
            lastShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + Vector3.left;
            bullet.GetComponent<Bullet>().Initialize(Vector3.left);
        }
        else if (type == ShootingType.OnPlayerY)
        {
            bool isCloseToPlayerY = transform.position.y < player.position.y + 0.1f && transform.position.y > player.position.y - 0.1f;
            if (isCloseToPlayerY)
            {
                lastShot = Time.time;
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = transform.position + Vector3.left;
                bullet.GetComponent<Bullet>().Initialize(Vector3.left);
            }
        }
        else if (type == ShootingType.Omnidirectional)
        {
            foreach(Vector2 dir2 in omniDirections)
            {
                Vector3 dir = new Vector3(dir2.x, dir2.y, 0);
                lastShot = Time.time;
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
    Forward,
    Omnidirectional
}
