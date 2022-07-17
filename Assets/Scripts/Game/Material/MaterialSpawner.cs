using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    public GameObject[] gameObjectsToSpawn;
    public Transform materialParent;
    [Tooltip("Index = Material Index")]
    public int[] numberOfMaterialsToSpawn;

    [Space]
    public Transform ground;
    public float distanceBetweenMaterial;
    public LayerMask layerMask;

    [Space]
    public Grid worldGrid;

    private Vector3Int cellPosition;
    private Vector3 centerCellPosition;

    private void Start()
    {
        SpawnMaterial();   
    }
    private void SpawnMaterial()
    {
        float groundHeight = (ground.localScale.y / 2) - 2;
        float groundWidth = (ground.localScale.x / 2) - 2;

        float randomX;
        float randomY;

        int materialIndex = 0;



        foreach (GameObject gameObject in gameObjectsToSpawn)
        {

            int spawned = 0;

            //Debug.Log("Spawning: " + numberOfMaterialsToSpawn[materialIndex] + " " + gameObjectsToSpawn[materialIndex].name);

            while (spawned != numberOfMaterialsToSpawn[materialIndex])
            {
                randomX = Random.Range(-groundWidth, groundWidth);
                randomY = Random.Range(-groundHeight, groundHeight);

                Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

                if (Physics2D.OverlapCircle(spawnPosition, distanceBetweenMaterial, layerMask)) continue;

                cellPosition = worldGrid.WorldToCell(spawnPosition);
                centerCellPosition = worldGrid.GetCellCenterLocal(cellPosition);

                GameObject material =Instantiate(gameObjectsToSpawn[materialIndex], centerCellPosition, Quaternion.identity, 
                    materialParent);

                material.name = gameObject.name;

                material.transform.Rotate(0, 0, Random.Range(0, 360));

                spawned++;  
            }
            materialIndex++;
        }
        GameManager.Instance.RecalculateGraph();
    }
    public void DeleteAllSpawnedMaterials()
    {
        GameObject[] spawnedMaterial = GameObject.FindGameObjectsWithTag("Material");

        foreach (GameObject gameObject in spawnedMaterial)
        {
            Destroy(gameObject);
        }
    }
}
