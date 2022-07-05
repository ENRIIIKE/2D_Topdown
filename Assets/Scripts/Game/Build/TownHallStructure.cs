using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallStructure : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SetStructureButtons(true);
        GameManager.Instance.DisableTownHallBuild();
        StructureManager.Instance.DeselectStructure();
    }
}
