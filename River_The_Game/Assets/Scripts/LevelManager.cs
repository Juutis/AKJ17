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

        for (int index = 0; index < scenes.Count; index += 1)
        {
            if (currentScene.path == scenes[index].scenePath)
            {
                levelIndex = index;
                Debug.Log($"Level index was determined to be {levelIndex}");
                break;
            }
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
    }

    public void NextLevel(PlayerUpgrades upgrades)
    {
        if (IncreaseLevelIndex())
        {
            SceneManager.LoadScene(currentLevel);
            playerUpgrades = upgrades;
            SceneManager.sceneLoaded += OnSceneLoad;
        }
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(playerUpgrades);
        GameManager.main.SetPlayerUpgrades(playerUpgrades);
        SceneManager.sceneLoaded -= OnSceneLoad;
    }
}
