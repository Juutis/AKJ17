using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;
    public void StartGame()
    {
        levelManager.LoadFirstLevel();
    }
}
