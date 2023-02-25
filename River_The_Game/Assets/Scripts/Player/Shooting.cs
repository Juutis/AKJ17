using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float shootingRate; // bullets per minute
    private float shootingDelay; // time between bullets
    private float lastShot;
    private bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        shootingDelay = 60f / shootingRate;
    }

    // Update is called once per frame
    void Update()
    {
        isShooting = Input.GetKey(KeyCode.Space);

        if (isShooting && (Time.time - lastShot) >= shootingDelay)
        {
            lastShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + Vector3.right;
        }
    }
}
