using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GridBuild gridBuild;
    public float delaySeconds;

    [Header("Axe Tool")]
    public bool axeReadyToUse = true;
    public bool axeSelected = false;

    [Header("Build Tool")]
    public bool buildReadyToUse = true;
    public bool buildSelected = false;

    [Space]
    public PlayerState state;
    public bool ableToSwitch = true;

    private void Start()
    {
        axeSelected = true;
    }
    void Update()
    {
        ChangeTool();

        if (Input.GetMouseButton(0))
        {
             switch (state)
            {
                case PlayerState.Axe:
                    if (!axeReadyToUse) return;

                    axeReadyToUse = false;
                    ableToSwitch = false;
                    break;
                case PlayerState.Build:
                    if (!buildReadyToUse) return;

                    buildReadyToUse = false;
                    ableToSwitch = false;
                    break;
            }
        }
    }
    private void ChangeTool()
    {
        //Changer to Axe
        if (Input.GetKeyDown(KeyCode.Alpha1) && ableToSwitch)
        {
            if (axeSelected) return;

            if (gridBuild.selectedBuilding != null)
            {
                StructureManager.Instance.DeselectStructure();
            }
            PlayerManager.Instance.UpdatePlayerState(PlayerState.Axe);
            StructureManager.Instance.OpenCloseStructureBar(false);
            state = PlayerState.Axe;
            axeSelected = true;
            buildSelected = false;

            StartCoroutine(SwitchDelay(1));
        }

        //Change to Build
        else if (Input.GetKeyDown(KeyCode.Tab) && ableToSwitch)
        {
            if (buildSelected) return;
            PlayerManager.Instance.UpdatePlayerState(PlayerState.Build);
            StructureManager.Instance.OpenCloseStructureBar(true);
            state = PlayerState.Build;
            buildSelected = true;
            axeSelected = false;

            StartCoroutine(SwitchDelay(2));
        }

        //Cancel
        if (Input.GetKeyDown(KeyCode.Escape) && state == PlayerState.Build)
        {
            StructureManager.Instance.DeselectStructure();
        }
    }

    /*
     * Axe = 1
     * Build = 2
     */
    private IEnumerator SwitchDelay(int tool)
    {
        if (tool == 1)
        {
            axeReadyToUse = false;
        }
        else if (tool == 2)
        {
            buildReadyToUse = false;
        }
        ableToSwitch = false;
        yield return new WaitForSeconds(delaySeconds);
        ableToSwitch = true;

        if (tool == 1)
        {
            axeReadyToUse = true;
        }
        else if (tool == 2)
        {
            buildReadyToUse = true;
        }
    }
}
