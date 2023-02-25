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
    }

    [SerializeField]
    private Shooting shooting;

    public void IncreaseBubbleFireRate(int increase)
    {
        shooting.IncreaseFireRate(increase);
    }

    public void UpgradeMainGun()
    {
        shooting.UpgradeMainGun();
    }

    public void UpgradeSideGun()
    {
        shooting.UpgradeSideGun();
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
    }

    public void NextLevel()
    {
        levelManager.NextLevel();
    }

    public void TheEnd()
    {

    }
}
