using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region Singleton
    //=====Singleton=====
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }
    #endregion

    public Transform structureBar;
    public GameObject townHallPanel;


    private void Start()
    {
        SetStructureButtons(false);
    }
    public void SetStructureButtons(bool enable)
    {
        foreach(Transform transform in structureBar)
        {
            Button button = transform.GetComponent<Button>();
            if (transform == townHallPanel.transform)
            {
                return;
            }
            if (!enable)
            {
                button.interactable = false;
            }
            else if (enable)
            {
                button.interactable = true;
            }
        }
    }
    public void DisableTownHallBuild()
    {
        townHallPanel.GetComponentInChildren<Button>().interactable = false;
    }

    public void RecalculateGraph()
    {
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }
}