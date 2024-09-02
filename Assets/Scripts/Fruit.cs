using UnityEngine;

public class Fruit : MonoBehaviour
{
    public PowerUpType powerUpType;
   public float duration = 10.0f;

    //fruit is rotating
    public float rotationSpeed = 100.0f;

    private void Update()
    {
        //rotate the fruit
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

}
