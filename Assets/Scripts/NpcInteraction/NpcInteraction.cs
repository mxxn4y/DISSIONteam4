using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아영 작성 스크립트 NPC 머리 위 말풍선(오브젝트) 활성화

public class NpcInteraction : MonoBehaviour
{

    public GameObject NpcTalkAv;
    public GameObject NPCTalkOnButton;

    void Start()
    {
        NpcTalkAv.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player") 
        {
            NpcTalkAv.SetActive(true);
            if(Input.GetMouseButtonDown(0)){
                NPCTalkOnButton.SetActive(true);
            }
        }
    }
}
