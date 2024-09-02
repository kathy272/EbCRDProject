using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;
    public Transform playerTransform;
    public PlayerController playerController; 
    private List<GameObject> activeCoins = new List<GameObject>();
    //public int powerUpDuration = 5;
    private float laneDistance = 4.0f;
    private float zSpawn = 0;
    //reference the fruitsUI script
    public FruitsUI fruitsUI;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnPowerUp();
        }
    }

    private void Update()
    {
        while (activeCoins.Count > 0 && activeCoins[0] != null && playerTransform.position.z > activeCoins[0].transform.position.z + 50)
        {
            DeletePowerUp();
        }

        if (playerTransform.position.z > zSpawn - 50)
        {
            SpawnPowerUp();
        }
    }

    private void SpawnPowerUp()
    {
        int lane = Random.Range(0, 3);
        float powerUpX = 0;
        switch (lane)
        {
            case 0:
                powerUpX = -laneDistance;
                break;
            case 1:
                powerUpX = 0;
                break;
            case 2:
                powerUpX = laneDistance;
                break;
        }

        GameObject powerUp = Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], new Vector3(powerUpX, 0.8f, zSpawn+5), Quaternion.identity);
        activeCoins.Add(powerUp);
        zSpawn += 30;
    }

    private void DeletePowerUp()
    {
        Destroy(activeCoins[0]);
        activeCoins.RemoveAt(0);
    }

    public void ActivatePowerUp(PowerUpType powerUpType, float duration)
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing.");
            return;
        }
      

        switch (powerUpType)
        {
            case PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost(duration));
                fruitsUI.ShowPowerUpUI(powerUpType, duration);
                break;
            case PowerUpType.HighJump:
                StartCoroutine(HighJump(duration));
                fruitsUI.ShowPowerUpUI(powerUpType, duration);
                break;
            case PowerUpType.SpeedDecrease:
                StartCoroutine(SpeedDecrease(duration));
                fruitsUI.ShowPowerUpUI(powerUpType, duration);
                break;
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing during SpeedBoost.");
            yield break;
        }

        playerController.forwardVelocity += 5;
        yield return new WaitForSeconds(duration);
        playerController.forwardVelocity -= 5;
    }

    private IEnumerator HighJump(float duration)
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing during HighJump.");
            yield break;
        }

        playerController.jumpForce += 5;
        yield return new WaitForSeconds(duration);
        playerController.jumpForce -= 5;
    }

    private IEnumerator SpeedDecrease(float duration)
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing during SpeedDecrease.");
            yield break;
        }

        playerController.forwardVelocity -= 5;
        yield return new WaitForSeconds(duration);
        playerController.forwardVelocity += 5;
    }
}

public enum PowerUpType
{
    SpeedBoost,
    HighJump,
    SpeedDecrease
}
