using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuild : MonoBehaviour
{
    public PlayerInteraction playerInteraction;

    [Header("Grids")]
    public Grid worldGrid;
    public Transform structureParent;
    public Transform materialParent;

    private Vector3 gridClicked;
    private Vector3Int cellPosition;
    private Vector3 centerCellPosition;
    

    [Header("Structures")]
    public GameObject selectedBuilding;
    public Structure script;
    public GameObject holdingObject;
    public GameObject[] structureIndex;

    public int numberOfBuildings = 0;

    [Space]
    public GameObject structureBuildingPS;
    public GameObject particleParent;

    [Space]
    public Color green;
    public Color red;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedBuilding == null) return;

            PlayerClick();
        }

        if (holdingObject != null)
        {
            UpdateHoldingStructurePosition();
        }
    }

    //PlayerClick Method will firstly find out if structure can be built in that specific grid
    //If not, then red particle will be shown indicating that this grid is occupied.
    
    //POSSIBLE CHANGE ---> Change Indicator to text floating above the selected grid 
    //If so, keep green indicator which indicates that structure has been built.

    private void PlayerClick()
    {
        if (selectedBuilding == null) return;

        gridClicked = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cellPosition = worldGrid.WorldToCell(gridClicked);
        centerCellPosition = worldGrid.GetCellCenterLocal(cellPosition);

        bool blocked = false;

        //Both foreach loops checks selected grid is available for use
        foreach (Transform occupiedCellStructure in structureParent)
        {
            foreach(Transform occupiedCellMaterial in materialParent)
            {
                if (occupiedCellStructure.position == centerCellPosition || occupiedCellMaterial.position == centerCellPosition)
                {
                    //Instantiate particle to show that grid is not available
                    GameObject particleStructure = Instantiate(structureBuildingPS, centerCellPosition, Quaternion.identity, particleParent.transform);
                    ParticleSystem particleSystem = particleStructure.GetComponentInChildren<ParticleSystem>();
                    var main = particleSystem.main;
                    main.startColor = red;
                    float durationPS = particleSystem.main.duration;
                    Destroy(particleStructure, durationPS + 10f);


                    blocked = true;
                    break;
                }
            }
        }
        if (blocked) return;

        int woodCost = script.StructureMaterialCostWood;
        int stoneCost = script.StructureMaterialCostStone;
        int ironCost = script.StructureMaterialCostIron;

        bool canBuild = PlayerInventory.Instance.CheckMaterial(woodCost, stoneCost, ironCost);

        if (!canBuild) return;

        BuildStructure();
    }


    //Instantiate structure
    private void BuildStructure()
    {
        bool full = StructureManager.Instance.CheckStructureCapacity();
        if (full) return;

        bool requirement = StructureManager.Instance.CheckStructureRequirements(holdingObject);
        if (!requirement) return;


        GameObject buildedStructure = Instantiate(selectedBuilding, centerCellPosition, Quaternion.identity, structureParent);
        StructureManager.Instance.AddStructure(buildedStructure);
        buildedStructure.GetComponent<Structure>().preview = false;

        //Instantiate particle to show that selected grid is available and structure will be build
        GameObject particleStructure = Instantiate(structureBuildingPS, centerCellPosition, Quaternion.identity, particleParent.transform);
        ParticleSystem particleSystem = particleStructure.GetComponentInChildren<ParticleSystem>();
        var main = particleSystem.main;
        main.startColor = green;
        float durationPS = particleSystem.main.duration;
        Destroy(particleStructure, durationPS + 10f);
        
        numberOfBuildings++;
    }

    #region Holder

    //These methods will help player to show where the structure will be built
    public void HoldingStructurePosition()
    {
        GameObject buildedStructure = Instantiate(selectedBuilding, centerCellPosition, Quaternion.identity);
        holdingObject = buildedStructure;

        script = holdingObject.GetComponent<Structure>();
        script.preview = true;
    }
    private void UpdateHoldingStructurePosition()
    {
        Vector3 holdinGrid = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int holdingCellPosition = worldGrid.WorldToCell(holdinGrid);
        Vector3 holdingCenterCellPosition = worldGrid.GetCellCenterLocal(holdingCellPosition);

        holdingObject.transform.position = holdingCenterCellPosition;
    }
    #endregion

    public void ChangeBool()
    {
        playerInteraction.buildReadyToUse = true;
        playerInteraction.ableToSwitch = true;
    }
}
