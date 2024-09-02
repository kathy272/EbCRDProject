using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    //just like the tile manager, we will spawn coins in the game
    public GameObject[] coinPrefabs;
    public Transform playerTransform;
    private List<GameObject> activeCoins = new List<GameObject>();
    public float safeZone = 50; // Additional buffer zone before deleting coins
    public float coinOffset = 10; // Offset from the center for coins
    public float coinLength = 30; // Length of the coins
    public float numberOfCoins = 5;
    private float zSpawn = 0; // Current spawn position for coins
    private float laneDistance = 4; // Distance between two lanes

    public void Start()
    {
        for (int i = 0; i < numberOfCoins; i++)
        {
            SpawnCoins();
        }
    }

    public void Update()
    {

        while (activeCoins.Count > 0 && activeCoins[0] != null && playerTransform.position.z > activeCoins[0].transform.position.z + safeZone)
        {
            DeleteCoins();
        }

        if (playerTransform.position.z > zSpawn - (5 * coinLength))
        {
            SpawnCoins();
        }
    }

    // Spawn coins in one of the lanes
    private void SpawnCoins()
    {
        int lane = Random.Range(0, 3);
        float coinX = 0;
        switch (lane)
        {
            case 0:
                coinX = -laneDistance;
                break;
            case 1:
                coinX = 0;
                break;
            case 2:
                coinX = laneDistance;
                break;
        }

        GameObject coin = Instantiate(coinPrefabs[0], new Vector3(coinX, 1.2f, zSpawn+3), Quaternion.identity);
        activeCoins.Add(coin);
        zSpawn += coinLength;
    }

    private void DeleteCoins()
    {
        if (activeCoins.Count > 0)
        {
            GameObject coinToDelete = activeCoins[0];
            activeCoins.RemoveAt(0);
            if (coinToDelete != null)
            {
                Destroy(coinToDelete);
            }
        }
    }
}
