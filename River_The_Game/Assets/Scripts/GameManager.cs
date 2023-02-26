using Assets.Scripts.Player;
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
    private PlayerMovement playerMovement;

    private PlayerUpgrades playerUpgrades;
    public int MainGunUpgrades => playerUpgrades.MainGunUpgrades;
    public int SideGunUpgrades => playerUpgrades.SideGunUpgrades;
    public int ShootingRateUpgrades => playerUpgrades.ShootingRateUpgrades;
    public int PlayerHP => playerUpgrades.HP;

    private void Start()
    {
        playerMovement = playerHealth.GetComponent<PlayerMovement>();
        GameObject levelManagerObject = GameObject.FindGameObjectWithTag("LevelManager");
        if (levelManagerObject == null)
        {
            levelManager = Instantiate(levelManagerPrefab).GetComponent<LevelManager>();
            playerUpgrades = new PlayerUpgrades()
            {
                HP = 3,
                MainGunUpgrades = 0,
                SideGunUpgrades = 0,
                ShootingRateUpgrades = 0
            };
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

    public void OpenMainMenu()
    {
        levelManager.MainMenu();
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        playerMovement.DisableControls();
        shooting.DisableControls();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        shooting.EnableControls();
        playerMovement.EnableControls();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        levelManager.RestartGame();
        Time.timeScale = 1f;
    }

    public void GainLife()
    {
        playerHealth.GainLife();
        playerUpgrades.HP++;
    }

    public void TakeDamage()
    {
        playerUpgrades.HP--;
        playerUpgrades.MainGunUpgrades = Mathf.Max(0, --playerUpgrades.MainGunUpgrades);
        playerUpgrades.SideGunUpgrades = Mathf.Max(0, --playerUpgrades.SideGunUpgrades);
        playerUpgrades.ShootingRateUpgrades = Mathf.Max(0, --playerUpgrades.ShootingRateUpgrades);
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        playerUpgrades.MainGunUpgrades = 0;
        playerUpgrades.SideGunUpgrades = 0;
        playerUpgrades.ShootingRateUpgrades = 0;
        playerUpgrades.HP = 3;
        UIManager.main.ShowGameOverMenu();
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
        UIManager.main.TheEnd();
    }

    public void SetPlayerUpgrades(PlayerUpgrades upgrades)
    {
        Debug.Log($"Setting player upgrades {playerUpgrades.SideGunUpgrades}");
        playerUpgrades = upgrades;
        Debug.Log($"Player upgrades updated {playerUpgrades.SideGunUpgrades}");
        // playerHealth.Initialize();
    }
}
