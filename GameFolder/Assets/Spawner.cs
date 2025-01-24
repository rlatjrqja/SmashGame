using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject Enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            float x = Random.Range(-1.5f, 1.5f);
            Instantiate(Enemy,this.transform.position + new Vector3(x,0,0), Quaternion.identity);

            if(false)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);
        }
    }
}
