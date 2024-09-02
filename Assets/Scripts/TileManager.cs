using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>();
    public float safeZone = 50; // Additional buffer zone before deleting tiles

    // Start is called before the first frame update
    void Start()
    {
        SpawnTile(0);
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
                continue;
            }
            SpawnTile(Random.Range(0, tilePrefabs.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(playerTransform.position.z > zSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            if (playerTransform.position.z > activeTiles[0].transform.position.z + safeZone)
            {
                DeleteTile();
            }
        }
    }
    public void SpawnTile(int tileIndex)
    {
       GameObject go= Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn, transform.rotation);

        activeTiles.Add(go);

        zSpawn += tileLength;
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
