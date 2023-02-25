using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private bool upgradeSideGunner;
    [SerializeField]
    private bool upgradeMainGunner;

    [SerializeField]
    private List<GameObject> mainGunBulletPrefabs;
    private GameObject currentMainGunPrefab;

    [SerializeField]
    private List<GameObject> sideGunBulletPrefabs;
    private GameObject currentSideGunPrefab;

    [SerializeField]
    private float shootingBaseRate; // bullets per minute
    private float shootingDelay; // time between bullets
    private float lastShot;
    private bool isShooting = false;

    private bool allowedToShoot = true;

    private int mainGunUpgrades = 0;
    private int sideGunUpgrades = 0;
    private int shootingRateUpgrades = 0;

    private float shootingRateStep = 10f;

    List<Vector3> sideGunnerDirs = new()
    {
        new (1, -1, 0),
        new (1, 1, 0),
        new (-1, 1, 0),
        new (-1, -1, 0)
    };

    private float shootingRate { get { return shootingBaseRate + shootingRateStep * shootingRateUpgrades; } }

    public void IncreaseFireRate(int increase)
    {
        // Debug.Log($"Firerate: {shootingRate} -> {shootingRate + increase}");
        // shootingRate += increase;
        shootingRateUpgrades += increase;
    }

    public void UpgradeMainGun()
    {
        mainGunUpgrades++;
    }

    public void UpgradeSideGun()
    {
        sideGunUpgrades++;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
    }

    public void DisableControls()
    {
        allowedToShoot = false;
    }
    public void EnableControls()
    {
        allowedToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (upgradeSideGunner)
        {
            UpgradeSideGun();
            upgradeSideGunner = false;
        }
        if (upgradeMainGunner)
        {
            UpgradeMainGun();
            upgradeMainGunner = false;
        }
        currentMainGunPrefab = mainGunBulletPrefabs[Mathf.Clamp(mainGunUpgrades, 0, mainGunBulletPrefabs.Count - 1)];
        currentSideGunPrefab = sideGunBulletPrefabs[Mathf.Clamp(sideGunUpgrades, 0, sideGunBulletPrefabs.Count - 1)];

        if (!allowedToShoot)
        {
            return;
        }
        shootingDelay = 60f / shootingRate;
        isShooting = Input.GetKey(KeyCode.Space);

        if (!isShooting || (Time.time - lastShot) < shootingDelay)
        {
            return;
        }

        lastShot = Time.time;
        GameObject bullet = Instantiate(currentMainGunPrefab);
        bullet.transform.position = transform.position + Vector3.right;
        bullet.GetComponent<Bullet>().Initialize(Vector3.right);

        if (sideGunUpgrades > 0)
        {
            int sideGunners = (new[] { 1, 2, 4 })[Mathf.Clamp(sideGunUpgrades - 1, 0, 2)];
            for (int i = 0; i < sideGunners; i++)
            {
                GameObject sideBullet = Instantiate(currentSideGunPrefab);
                Vector3 dir = sideGunnerDirs[i].normalized;
                sideBullet.transform.position = transform.position + dir;
                sideBullet.GetComponent<Bullet>().Initialize(dir);
            }
        }
    }

}
