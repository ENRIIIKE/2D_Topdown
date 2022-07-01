using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Singleton
    //=====Singleton=====
    private static PlayerManager _instance;

    public static PlayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<PlayerManager>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }
    #endregion

    public PlayerState playerState;

    public GameObject axeToolObject;
    public GameObject buildToolObject;

    private void Start()
    {
        UpdatePlayerState(playerState);
    }
    public void UpdatePlayerState(PlayerState newState)
    {
        playerState = newState;

        switch (playerState)
        {
            case PlayerState.Axe:
                UpdateToolState(1);
                break;
            case PlayerState.Build:
                UpdateToolState(2);
                break;
        }
    }
    /*
     * if whichTool == 1 --- Axe
     * if whichTool == 2 --- Build
     */
    private void UpdateToolState(int whichTool)
    {
        if (whichTool == 1)
        {
            axeToolObject.SetActive(true);
            buildToolObject.SetActive(false);
        }
        else if (whichTool == 2)
        {
            buildToolObject.SetActive(true);
            axeToolObject.SetActive(false);
        }
    }

}
public enum PlayerState { Axe, Build }
