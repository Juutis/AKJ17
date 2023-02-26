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

    private PlayerMovement playerMovement;

    [SerializeField]
    private float shootingBaseRate; // bullets per minute
    private float shootingDelay; // time between bullets
    private float lastShot;
    private bool isShooting = false;

    private bool allowedToShoot = true;

    private float shootingRateStep = 40f;
    private PlayerAnimation anim;

    [SerializeField]
    private GameObject[] SideGunsGraphics;

    List<Vector3> sideGunnerDirs = new()
    {
        new (1, -1, 0),
        new (1, 1, 0),
        new (-1, 1, 0),
        new (-1, -1, 0)
    };

    private float shootingRate { get { return shootingBaseRate + shootingRateStep * GameManager.main.ShootingRateUpgrades; } }

    // Start is called before the first frame update
    void Start()
    {
        lastShot = Time.time;
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponentInChildren<PlayerAnimation>();
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
        var sideGuns = Mathf.Clamp(GameManager.main.SideGunUpgrades, 0, SideGunsGraphics.Length - 1);
        for(int i = 0; i < SideGunsGraphics.Length; i++)
        {
            if (SideGunsGraphics[i] == null)
            {
                continue;
            }

            if (i == sideGuns)
            {
                SideGunsGraphics[i].SetActive(true);
            }
            else
            {
                SideGunsGraphics[i].SetActive(false);
            }
        }

        currentMainGunPrefab = mainGunBulletPrefabs[Mathf.Clamp(GameManager.main.MainGunUpgrades, 0, mainGunBulletPrefabs.Count - 1)];
        currentSideGunPrefab = sideGunBulletPrefabs[Mathf.Clamp(GameManager.main.SideGunUpgrades, 0, sideGunBulletPrefabs.Count - 1)];

        if (!allowedToShoot || playerMovement.IsAboveWater)
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
        anim.Shoot();
        SoundManager.main.PlaySound(GameSoundType.BubbleShoot);

        if (GameManager.main.SideGunUpgrades > 0)
        {
            int sideGunners = (new[] { 1, 2, 4 })[Mathf.Clamp(GameManager.main.SideGunUpgrades - 1, 0, 2)];
            for (int i = 0; i < sideGunners; i++)
            {
                GameObject sideBullet = Instantiate(currentSideGunPrefab);
                Vector3 dir = sideGunnerDirs[i].normalized;
                sideBullet.transform.position = transform.position + dir * 0.5f;
                sideBullet.GetComponent<Bullet>().Initialize(dir);
            }
        }

    }

}
