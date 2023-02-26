using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private List<ScenePicker> scenes;

    [SerializeField]
    private int debugLevelIndex = 0;

    private int levelIndex = 0;
    private PlayerUpgrades playerUpgrades;

    [SerializeField]
    private MusicManager musicManager;

    bool sceneIsMainMenu { get { return SceneManager.GetActiveScene().name == "mainMenu"; } }

    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("LevelManager").Length > 1)
        {
            Destroy(gameObject);
        }
        Object.DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Start levelmanager");
        for (int index = 0; index < scenes.Count; index += 1)
        {
            if (currentScene.path == scenes[index].scenePath)
            {
                levelIndex = index;
                debugLevelIndex = index;
                Debug.Log($"Level index was determined to be {levelIndex}");
                playerUpgrades = new PlayerUpgrades()
                {
                    HP = 3,
                    MainGunUpgrades = 0,
                    SideGunUpgrades = 0,
                    ShootingRateUpgrades = 0
                };
                break;
            }
        }
        if (sceneIsMainMenu)
        {
            musicManager.StartMusic(true);
        }
        else
        {
            musicManager.StartMusic(false);
        }
    }

    private int currentLevelIndex
    {
        get
        {
            if (!Application.isEditor)
            {
                return levelIndex;
            }
            else
            {
                return debugLevelIndex;
            }
        }
    }

    private string currentLevel
    {
        get
        {
            return scenes[currentLevelIndex].scenePath;
        }
    }

    private bool IncreaseLevelIndex()
    {
        if (Application.isEditor)
        {
            debugLevelIndex += 1;
            if (debugLevelIndex >= scenes.Count)
            {
                GameManager.main.TheEnd();
                return false;
            }
        }
        else
        {
            levelIndex += 1;
            if (levelIndex >= scenes.Count)
            {
                GameManager.main.TheEnd();
                return false;
            }
        }
        return true;
    }

    public void RestartGame()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        playerUpgrades = new PlayerUpgrades()
        {
            HP = 3,
            MainGunUpgrades = 0,
            SideGunUpgrades = 0,
            ShootingRateUpgrades = 0
        };
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("mainMenu");
        musicManager.SwitchMusic(true);
    }

    public void LoadFirstLevel()
    {
        Debug.Log("Loading first level");
        playerUpgrades = new PlayerUpgrades()
        {
            HP = 3,
            MainGunUpgrades = 0,
            SideGunUpgrades = 0,
            ShootingRateUpgrades = 0
        };
        SceneManager.LoadScene(currentLevel);
        SceneManager.sceneLoaded += OnSceneLoad;
        musicManager.SwitchMusic(false);
    }

    public void NextLevel(PlayerUpgrades upgrades)
    {
        bool mainMenu = sceneIsMainMenu;
        if (IncreaseLevelIndex())
        {
            SceneManager.LoadScene(currentLevel);
            playerUpgrades = upgrades;
            SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"HP: {playerUpgrades.HP}");
        GameManager.main.SetPlayerUpgrades(playerUpgrades);
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
