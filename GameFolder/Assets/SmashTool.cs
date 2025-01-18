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
            // ���� Y�� ���� Ȯ��
            float currentY = transform.eulerAngles.y;

            // Unity�� EulerAngles�� 0~360���� ǥ���ǹǷ�, -120���� 240���� ��ȯ�ؼ� ��
            if (currentY > 180f)
            {
                currentY -= 360f; // -180 ~ 180 ���̷� ����
            }

            // Y�� ȸ��
            if (currentY > -120f)
            {
                transform.Rotate(0, -speed * 1.5f * Time.deltaTime, 0);
            }
            else
            {
                break;
            }

            // ���� �����ӱ��� ���
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
            // ���� Y�� ���� Ȯ��
            float currentY = transform.eulerAngles.y;
            
            // Unity�� EulerAngles�� 0~360���� ǥ���ǹǷ�, -120���� 240���� ��ȯ�ؼ� ��
            if (currentY > 180f)
            {
                currentY -= 360f; // -180 ~ 180 ���̷� ����
            }

            // Y�� ȸ��
            if (currentY < 0f)
            {
                transform.Rotate(0, speed * Time.deltaTime, 0);
            }
            else
            {
                // 0�� �����ϸ� ��Ȯ�� ����
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z);
                break;
            }

            // ���� �����ӱ��� ���
            yield return null;
        }

        state = State.Charging;
    }
}
