using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    #region Singleton
    //=====Singleton=====
    private static StructureManager _instance;

    public static StructureManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StructureManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<StructureManager>();
                }
            }
            return _instance;
        }
        private set { _instance = value; }
    }
    #endregion

    public GridBuild gridBuildScript;
    public RectTransform structureRectTransform;
    public LeanTweenType structureBarTweenType;
    public float moveToFromY;
    public float smoothTime;
    public bool isBarOpened = false;
    public int maxStructures;
    public LayerMask layerMask;

    private Vector3 newPos;
    private Vector3 oldPos;

    public List<GameObject> buildedStructures = new List<GameObject>();
    private void Start()
    {
        
        newPos = new Vector2(structureRectTransform.localPosition.x, structureRectTransform.localPosition.y + moveToFromY);
        oldPos = structureRectTransform.localPosition;

    }
    public void OpenCloseStructureBar(bool open)
    {
        if (open)
        {
            if (isBarOpened) return;
            isBarOpened = true;
            LeanTween.move(structureRectTransform, newPos, smoothTime).setEase(structureBarTweenType);
        }
        else if (!open)
        {
            if (!isBarOpened) return;
            isBarOpened = false;
            LeanTween.move(structureRectTransform, oldPos, smoothTime).setEase(structureBarTweenType);
        }
    }
    public void AddStructure(GameObject structure)
    {
        bool found = false;
        foreach (GameObject structureforeach in buildedStructures)
        {
            if (structureforeach == structure)
            {
                found = true;
                break;
            }
            else
            {
                continue;
            }   
        }
        if (!found)
        {
            buildedStructures.Add(structure);
        }
    }
    public bool CheckStructureCapacity()
    {
        if (buildedStructures.Count >= maxStructures)
        {
            //Make Univerlsal floating text above player
            Debug.Log("Structure Limit Reached");
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckStructureRequirements(GameObject structure)
    {
        if (structure.GetComponent<MineStructure>() != null)
        {
            float range = 1.2f;

            Collider2D[] foundedMaterialsColliders = Physics2D.OverlapCircleAll(structure.transform.position, range, layerMask);
            List<GameObject> materialsList = new List<GameObject>();

            foreach (Collider2D collider in foundedMaterialsColliders)
            {
                materialsList.Add(collider.gameObject);
            }
            if (materialsList.Count < 1) return false;

            bool ironFound = false;
            foreach (GameObject material in materialsList)
            {
                if (material.GetComponent<MaterialScript>().materialSO.materialEnum == MaterialScriptableObject.MaterialEnum.Iron)
                {
                    ironFound = true;
                    break;
                }

                else
                {
                    ironFound = false;
                    continue;
                }
            }
            if (!ironFound) return false;

            if(Physics2D.OverlapCircle(structure.transform.position, range, layerMask))
            {
                return true;
            }
            else
            {
                return false;
            }
}
        else
        {
            return true;
        }
    }

    public void SelectStructure(int index)
    {
        gridBuildScript.selectedBuilding = gridBuildScript.structureIndex[index];
        gridBuildScript.HoldingStructurePosition();
    }
    public void DeselectStructure()
    {
        Destroy(gridBuildScript.holdingObject);
        gridBuildScript.script = null;
        gridBuildScript.holdingObject = null;
        gridBuildScript.selectedBuilding = null;
    }
}
