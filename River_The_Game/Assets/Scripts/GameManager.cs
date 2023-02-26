using Assets.Scripts.Player;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager main;
    private void Awake()
    {
        main = this;
    }

    private LevelManager levelManager;
    [SerializeField]
    private LevelManager levelManagerPrefab;
    [SerializeField]
    private PlayerHealth playerHealth;

    private PlayerUpgrades playerUpgrades;
    public int MainGunUpgrades => playerUpgrades.MainGunUpgrades;
    public int SideGunUpgrades => playerUpgrades.SideGunUpgrades;
    public int ShootingRateUpgrades => playerUpgrades.ShootingRateUpgrades;
    public int PlayerHP => playerUpgrades.HP;

    private void Start()
    {
        GameObject levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
        if (levelManagerObject == null)
        {
            levelManager = Instantiate(levelManagerPrefab).GetComponent<LevelManager>();
        }
        else
        {
            levelManager = levelManagerObject.GetComponent<LevelManager>();
        }
        playerHealth.Initialize();
    }

    [SerializeField]
    private Shooting shooting;

    public void IncreaseBubbleFireRate(int increase)
    {
        // shooting.IncreaseFireRate(increase);
        playerUpgrades.ShootingRateUpgrades += increase;
    }

    public void UpgradeMainGun()
    {
        // shooting.UpgradeMainGun();
        playerUpgrades.MainGunUpgrades++;
    }

    public void UpgradeSideGun()
    {
        // shooting.UpgradeSideGun();
        playerUpgrades.SideGunUpgrades++;
    }

    public void GainLife()
    {
        playerHealth.GainLife();
        playerUpgrades.HP++;
    }

    public void TakeDamage()
    {
        playerUpgrades.HP--;
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        playerUpgrades.MainGunUpgrades = 0;
        playerUpgrades.SideGunUpgrades = 0;
        playerUpgrades.ShootingRateUpgrades = 0;
        playerUpgrades.HP = 3;
    }

    public void NextLevel()
    {
        levelManager.NextLevel(playerUpgrades);
    }

    public void TheEnd()
    {
        playerUpgrades.MainGunUpgrades = 0;
        playerUpgrades.SideGunUpgrades = 0;
        playerUpgrades.ShootingRateUpgrades = 0;
        playerUpgrades.HP = 3;
    }

    public void SetPlayerUpgrades(PlayerUpgrades upgrades)
    {
        Debug.Log($"Setting player upgrades {playerUpgrades.SideGunUpgrades}");
        playerUpgrades = upgrades;
        Debug.Log($"Player upgrades updated {playerUpgrades.SideGunUpgrades}");
        // playerHealth.Initialize();
    }
}
