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
        if(Input.GetMouseButtonDown(1))
        {
            if(clickCount == 0)
            {
                text.text = "�̵��� �㰡�մϴ�.";
                clickCount++; //1
            }
            else if(clickCount == 1)
            {
                text.text = "�����̷��� W, A, S, D�� \n����մϴ�.";

                //�÷��̾ WASD�� ��� �������� �������� �Ѿ�� �Ϸ��� ������ �ϴ� ������ ����
                //if(Input.GetKeyDown(KeyCode.W))
                //{
                //    print("�ƴ� �� �ȵ�??;;;");
                //    clickCount++; //2
                //}
                clickCount++; //2
            }
            else if(clickCount == 2)
            {
                text.text = "Ž���� �㰡�մϴ�.";
                clickCount++; //3
            }
            else if (clickCount == 3)
            {
                text.text = "���콺�� �̿��Ͽ� \n�ֺ��� Ž���մϴ�.";
                clickCount++; //4
            }
            else if (clickCount == 4)
            {
                text.text = "Ž�� ���� ���� ����ü���� \n������ �㰡�մϴ�.";
                clickCount++; //5
            }
            else if (clickCount == 5)
            {
                text.text = "���콺 ���� ��ư�� \n����Ͽ� �����մϴ�.";
                clickCount++; //6
            }
            else if (clickCount == 6)
            {
                text.text = "���߽��ϴ�.\n�⺻Ű�� �� �������ϴ�.";
                clickCount++; //7
            }
            else if (clickCount == 7)
            {
                talkPanel.SetActive(false);
                //clickCount++; //8
            }

            Debug.Log(clickCount);
        }
    }
}
