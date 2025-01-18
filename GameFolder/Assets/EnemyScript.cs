using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float move_speed;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0,0,-move_speed * Time.deltaTime);
    }
}
