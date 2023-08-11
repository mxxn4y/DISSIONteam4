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
        if(Input.GetKeyDown(KeyCode.Return)) //엔터키
        {
            if(clickCount == 0)
            {
                text.text = "이동을 허가합니다.";
                clickCount++; //1
            }
            else if(clickCount == 1)
            {
                text.text = "움직이려면 W, A, S, D를 \n사용합니다.";

                //if (Input.GetKeyDown(KeyCode.W)) // 'W' 키를 눌렀을 때
                //{
                //    Debug.Log("W key pressed");
                //    clickCount++;
                //}

                clickCount++; //2
            }
            else if (clickCount == 2)
            {
                text.text = "SHIFT를 누르면 \n빠르게 이동할 수 있습니다.";
                clickCount++; //3
            }
            else if(clickCount == 3)
            {
                text.text = "탐색을 허가합니다.";
                clickCount++; //4
            }
            else if (clickCount == 4)
            {
                text.text = "마우스를 이용하여 \n주변을 탐색합니다.";
                clickCount++; //5
            }
            else if (clickCount == 5)
            {
                text.text = "탐지 되지 않은 생명체에게 \n공격을 허가합니다.";
                clickCount++; //6
            }
            else if (clickCount == 6)
            {
                text.text = "마우스 왼쪽 버튼을 \n사용하여 공격합니다.";
                clickCount++; //7
            }
            else if (clickCount == 7)
            {
                text.text = "자성을 허가합니다.";
                clickCount++; //8
            }
            else if (clickCount == 8)
            {
                text.text = "마우스 오른쪽 버튼을 사용하여 \n 자성 능력을 사용합니다.";
                clickCount++; //9
            }
            else if (clickCount == 9)
            {
                text.text = "잘했습니다.\n기본키를 다 익혔습니다.";
                clickCount++; //10
            }
            else if (clickCount == 10)
            {
                text.text = "가동을 시작합니다.";
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
