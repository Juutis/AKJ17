using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMenuScreen : MonoBehaviour
{
    [SerializeField]
    private Image imgBackground;

    [SerializeField]
    private TextMeshProUGUI txtTitle;
    [SerializeField]
    private TextMeshProUGUI txtDescription;

    [SerializeField]
    private Transform buttonContainer;
    [SerializeField]
    private UIMenuButton buttonPrefab;

    private MenuScreen menu;

    private List<UIMenuButton> buttons = new List<UIMenuButton>();


    private bool respawn = false;

    [SerializeField]
    private Animator animator;

    public void Initialize(MenuScreen newMenu)
    {
        menu = newMenu;
        imgBackground.sprite = menu.Background;
        txtTitle.text = menu.Title;
        txtDescription.text = menu.Description;
        foreach (UIMenuButton button in buttons)
        {
            Destroy(button.gameObject);
        }
        buttons.Clear();
        foreach (MenuButton button in menu.Buttons)
        {
            UIMenuButton menuButton = Instantiate(buttonPrefab, buttonContainer);
            menuButton.Initialize(button, this);
            buttons.Add(menuButton);
        }
        transform.localPosition = Vector2.zero;
    }

    public void OnClick(MenuButtonAction action)
    {
        Debug.Log($"Action: {action}");
        if (action == MenuButtonAction.MainMenu)
        {
            GameManager.main.OpenMainMenu();
        }
        if (action == MenuButtonAction.Restart)
        {
            GameManager.main.RestartGame();
        }
        if (action == MenuButtonAction.Continue)
        {
            Close();
        }
        if (action == MenuButtonAction.Respawn)
        {
            respawn = true;
            Close();
        }
        if (action == MenuButtonAction.NextLevel)
        {
            GameManager.main.NextLevel();
        }
    }

    public void Open(string title = "", string description = "", Sprite icon = null)
    {
        GameManager.main.PauseGame();
        animator.Play("uiMenuScreenOpen");

    }
    public void CloseFinished()
    {
        UIManager.main.MenuWasClosed();
        GameManager.main.ResumeGame();
    }
    private void Close()
    {
        animator.Play("uiMenuScreenClose");
    }
}

public enum MenuButtonAction
{
    Restart,
    MainMenu,
    Continue,
    Respawn,
    NextLevel
}

[System.Serializable]
public class MenuButton
{
    [SerializeField]
    private string title;
    public string Title { get { return title; } }
    [SerializeField]
    private MenuButtonAction action;

    public MenuButtonAction Action { get { return action; } }
}

[System.Serializable]
public class MenuScreen
{

    [SerializeField]
    private string title;
    public string Title { get { return title; } }

    [SerializeField, TextArea]
    private string description;
    public string Description { get { return description; } }

    [SerializeField]
    private Sprite background;
    public Sprite Background { get { return background; } }

    [SerializeField]
    private List<MenuButton> buttons;
    public List<MenuButton> Buttons { get { return buttons; } }
}