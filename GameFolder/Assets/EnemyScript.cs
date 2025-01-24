using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public enum State { Moving, Falling, Dead }
    public State state = State.Dead;

    public float move_speed;

    void Start()
    {
        state = State.Moving;
    }

    void Update()
    {
        if(state == State.Moving)
        {
            transform.Translate(0, 0, -move_speed * Time.deltaTime);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            state = State.Falling;
            gameObject.GetComponent<Rigidbody>().mass = 0.01f;

            float x = Random.Range(4.0f, 5.0f);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * x);
            float y = Random.Range(3.0f, 4.0f);
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * y);

            // 오브젝트 풀링
        }
    }
}
