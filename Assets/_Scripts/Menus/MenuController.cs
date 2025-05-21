using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : Singleton<MenuController>
{
    public BaseMenu[] persistentMenus;

    public enum MenuStates
    {
        MainMenu,
        Settings,
        Statistics,
        Upgrades,
        Records,
        HUD,
        Pause
    }

    private BaseMenu currentState;
    private Dictionary<MenuStates, BaseMenu> menuDictionary = new Dictionary<MenuStates, BaseMenu>();
    private Stack<MenuStates> menuStack = new Stack<MenuStates>();


    void Start()
    {
        InitializeMenus(persistentMenus);

        // Optionally start in MainMenu
        SetActiveState(MenuStates.MainMenu);

        // Automatically register scene menus on load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BaseMenu[] sceneMenus = FindObjectsOfType<BaseMenu>(true);
        RegisterSceneMenus(sceneMenus);
    }

    private void InitializeMenus(BaseMenu[] menus)
    {
        if (menus == null) return;

        foreach (BaseMenu menu in menus)
        {
            if (menu == null) continue;

            menu.InitState(this);
            if (!menuDictionary.ContainsKey(menu.state))
            {
                menuDictionary.Add(menu.state, menu);
                menu.gameObject.SetActive(false);
            }
        }
    }

    public void RegisterSceneMenus(BaseMenu[] sceneMenus)
    {
        foreach (BaseMenu menu in sceneMenus)
        {
            if (menu == null || menuDictionary.ContainsKey(menu.state)) continue;

            menu.InitState(this);
            menuDictionary.Add(menu.state, menu);
            menu.gameObject.SetActive(false);
        }
    }

    public void SetActiveState(MenuStates newState, bool isJumpingBack = false)
    {
        if (!menuDictionary.ContainsKey(newState)) return;

        if (currentState != null)
        {
            currentState.ExitState();
            currentState.gameObject.SetActive(false);
        }

        currentState = menuDictionary[newState];
        currentState.gameObject.SetActive(true);
        currentState.EnterState();

        if (!isJumpingBack)
        {
            menuStack.Push(newState);
        }
    }

    public void JumpBack()
    {
        if (menuStack.Count <= 1)
        {
            SetActiveState(MenuStates.MainMenu);
        }
        else
        {
            menuStack.Pop();
            SetActiveState(menuStack.Peek(), true);
        }
    }

    public void UnregisterSceneMenus(MenuStates[] statesToRemove)
    {
        foreach (MenuStates state in statesToRemove)
        {
            if (menuDictionary.ContainsKey(state))
            {
                menuDictionary.Remove(state);
            }
        }
    }
}