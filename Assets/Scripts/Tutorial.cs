using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject talkPanel;
    public Text text;
    int clickCount = 0;

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return)) //����Ű
        {
            if(clickCount == 0)
            {
                text.text = "�̵��� �㰡�մϴ�.";
                clickCount++; //1
            }
            else if(clickCount == 1)
            {
                text.text = "�����̷��� W, A, S, D�� \n����մϴ�.";

                //if (Input.GetKeyDown(KeyCode.W)) // 'W' Ű�� ������ ��
                //{
                //    Debug.Log("W key pressed");
                //    clickCount++;
                //}

                clickCount++; //2
            }
            else if (clickCount == 2)
            {
                text.text = "SHIFT�� ������ \n������ �̵��� �� �ֽ��ϴ�.";
                clickCount++; //3
            }
            else if(clickCount == 3)
            {
                text.text = "Ž���� �㰡�մϴ�.";
                clickCount++; //4
            }
            else if (clickCount == 4)
            {
                text.text = "���콺�� �̿��Ͽ� \n�ֺ��� Ž���մϴ�.";
                clickCount++; //5
            }
            else if (clickCount == 5)
            {
                text.text = "Ž�� ���� ���� ����ü���� \n������ �㰡�մϴ�.";
                clickCount++; //6
            }
            else if (clickCount == 6)
            {
                text.text = "���콺 ���� ��ư�� \n����Ͽ� �����մϴ�.";
                clickCount++; //7
            }
            else if (clickCount == 7)
            {
                text.text = "�ڼ��� �㰡�մϴ�.";
                clickCount++; //8
            }
            else if (clickCount == 8)
            {
                text.text = "���콺 ������ ��ư�� ����Ͽ� \n �ڼ� �ɷ��� ����մϴ�.";
                clickCount++; //9
            }
            else if (clickCount == 9)
            {
                text.text = "���߽��ϴ�.\n�⺻Ű�� �� �������ϴ�.";
                clickCount++; //10
            }
            else if (clickCount == 10)
            {
                text.text = "������ �����մϴ�.";
                clickCount++; //11
            }
            else if (clickCount == 11)
            {
                talkPanel.SetActive(false);
                //clickCount++; //12
            }

            Debug.Log(clickCount);
        }
    }
}
