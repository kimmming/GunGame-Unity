using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rbody;
    public float force;
    private void Start()
    {
        rbody.AddForce(transform.forward * force);
    }
}
