using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMoving : MonoBehaviour
{

    //class to move the coin up and down
    public float speed = 1.0f;
    public float height = 0.5f;
    private Vector3 pos;
    private Vector3 prevPos;


    public void Start()
    {
        pos = transform.position;
        prevPos = pos;
    }
    public void Update()
    {
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed);
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(pos.x, pos.y + newY * height, pos.z);
    }
}
