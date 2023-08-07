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
                text.text = "이동을 허가합니다.";
                clickCount++; //1
            }
            else if(clickCount == 1)
            {
                text.text = "움직이려면 W, A, S, D를 \n사용합니다.";

                //플레이어가 WASD를 모두 눌러야지 다음으로 넘어가게 하려고 했으나 일단 실패한 상태
                //if(Input.GetKeyDown(KeyCode.W))
                //{
                //    print("아니 왜 안됨??;;;");
                //    clickCount++; //2
                //}
                clickCount++; //2
            }
            else if(clickCount == 2)
            {
                text.text = "탐색을 허가합니다.";
                clickCount++; //3
            }
            else if (clickCount == 3)
            {
                text.text = "마우스를 이용하여 \n주변을 탐색합니다.";
                clickCount++; //4
            }
            else if (clickCount == 4)
            {
                text.text = "탐지 되지 않은 생명체에게 \n공격을 허가합니다.";
                clickCount++; //5
            }
            else if (clickCount == 5)
            {
                text.text = "마우스 왼쪽 버튼을 \n사용하여 공격합니다.";
                clickCount++; //6
            }
            else if (clickCount == 6)
            {
                text.text = "잘했습니다.\n기본키를 다 익혔습니다.";
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
