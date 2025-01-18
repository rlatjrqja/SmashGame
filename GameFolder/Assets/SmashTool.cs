using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashTool : MonoBehaviour
{
    public enum State { Charging, Full, Swing, Wait }
    public State state = State.Charging;

    public float charge_amount;
    public float swing_speed;
    public float cool_down;

    public BoxCollider bat_col;

    // Start is called before the first frame update
    void Start()
    {
        bat_col = GetComponentInChildren<BoxCollider>();
        bat_col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            switch(state)
            {
                case State.Charging:
                    Charge();
                    break;
                case State.Full:
                    StartCoroutine(Swing(swing_speed));
                    break;
                case State.Swing:
                    break;
                case State.Wait:
                    break;
            }
        }

        //transform.Rotate(0, -1, 0);
    }

    void Charge()
    {
        transform.Rotate(0, charge_amount, 0);
        if (transform.eulerAngles.y > 100f) state = State.Full;
    }

    IEnumerator Swing(float speed)
    {
        state = State.Swing;
        bat_col.enabled = true;

        while (transform.eulerAngles.y > -120f)
        {
            // 현재 Y축 각도 확인
            float currentY = transform.eulerAngles.y;

            // Unity의 EulerAngles는 0~360도로 표현되므로, -120도를 240도로 변환해서 비교
            if (currentY > 180f)
            {
                currentY -= 360f; // -180 ~ 180 사이로 조정
            }

            // Y축 회전
            if (currentY > -120f)
            {
                transform.Rotate(0, -speed * 1.5f * Time.deltaTime, 0);
            }
            else
            {
                break;
            }

            // 다음 프레임까지 대기
            yield return null;
        }

        yield return new WaitForSeconds(cool_down);
        StartCoroutine(Recharge(swing_speed));
    }

    IEnumerator Recharge(float speed)
    {
        state = State.Wait;
        bat_col.enabled = false;

        while (true)
        {
            // 현재 Y축 각도 확인
            float currentY = transform.eulerAngles.y;
            
            // Unity의 EulerAngles는 0~360도로 표현되므로, -120도를 240도로 변환해서 비교
            if (currentY > 180f)
            {
                currentY -= 360f; // -180 ~ 180 사이로 조정
            }

            // Y축 회전
            if (currentY < 0f)
            {
                transform.Rotate(0, speed * Time.deltaTime, 0);
            }
            else
            {
                // 0에 도달하면 정확히 고정
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                break;
            }

            // 다음 프레임까지 대기
            yield return null;
        }

        state = State.Charging;
    }
}
