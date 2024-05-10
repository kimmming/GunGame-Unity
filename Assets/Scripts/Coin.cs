using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;

    void Update()
    {
        transform.Rotate( rotateSpeed * Time.deltaTime * Vector3.up, Space.World);
    }
}
