using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //gameObject.GetComponent<BoxCollider>()
        other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 150f);
    }
}
