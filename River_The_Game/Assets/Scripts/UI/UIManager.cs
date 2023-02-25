using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager main;
    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private UIHealth health;

    public void SetLives(int value)
    {
        health.SetLives(value);
    }

    public void LoseLife()
    {
        health.LoseLife();
    }

    public void GainLife()
    {
        health.GainLife();
    }
}
