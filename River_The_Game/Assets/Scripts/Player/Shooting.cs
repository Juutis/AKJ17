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

    public void IncreaseFireRate(int increase)
    {
        Debug.Log($"Firerate: {shootingRate} -> {shootingRate + increase}");
        shootingRate += increase;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        shootingDelay = 60f / shootingRate;
        isShooting = Input.GetKey(KeyCode.Space);

        if (isShooting && (Time.time - lastShot) >= shootingDelay)
        {
            lastShot = Time.time;
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position + Vector3.right;
            bullet.GetComponent<Bullet>().Initialize(Vector3.right);
        }
    }

}
