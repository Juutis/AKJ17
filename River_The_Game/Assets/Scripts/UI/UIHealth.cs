using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIHealth : MonoBehaviour
{
    [SerializeField]
    private Transform lifeContainer;
    [SerializeField]
    private UILife lifePrefab;

    private List<UILife> lives = new List<UILife>();
    public void SetLives(int value)
    {
        foreach (UILife life in lives)
        {
            life.Kill();
        }
        lives.Clear();
        for (int index = 0; index < value; index += 1)
        {
            AddLife();
        }
    }

    public void LoseLife()
    {
        int index = lives.Count - 1;
        UILife life = lives[index];
        life.Kill();
        lives.Remove(life);
    }
    public void GainLife()
    {
        AddLife();
    }


    private void AddLife()
    {
        UILife life = Instantiate(lifePrefab, lifeContainer);
        lives.Add(life);
    }
}
