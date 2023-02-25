using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private Shooting shooting;

    public void IncreaseBubbleFireRate(int increase)
    {
        shooting.IncreaseFireRate(increase);
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
    }
}
