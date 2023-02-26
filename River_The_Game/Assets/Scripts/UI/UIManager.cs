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

    [SerializeField]
    private Transform uiMenuContainer;

    [SerializeField]
    private UIMenuScreen menuScreenPrefab;
    [SerializeField]
    private MenuScreen pauseMenu;
    [SerializeField]
    private MenuScreen gameOverMenu;
    [SerializeField]
    private MenuScreen theEndMenu;

    private UIMenuScreen menuScreen;

    private bool menuIsOpen = false;

    private UIMenuScreen InitMenu()
    {
        if (menuScreen == null)
        {
            menuScreen = Instantiate(menuScreenPrefab, uiMenuContainer);
        }
        return menuScreen;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            ShowPauseMenu();
        }
    }

    public void ShowGameOverMenu()
    {
        UIMenuScreen screen = InitMenu();
        screen.Initialize(gameOverMenu);
        screen.Open();
        menuIsOpen = true;
    }
    public void TheEnd()
    {
        UIMenuScreen screen = InitMenu();
        screen.Initialize(theEndMenu);
        screen.Open();
        menuIsOpen = true;
    }

    public void ShowPauseMenu()
    {

        if (menuIsOpen)
        {
            return;
        }
        UIMenuScreen screen = InitMenu();
        screen.Initialize(pauseMenu);
        screen.Open();
        menuIsOpen = true;
    }


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

    public void MenuWasClosed()
    {
        menuIsOpen = false;
    }
}
