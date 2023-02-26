using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager manager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        manager.LoadFirstLevel();
    }
}
