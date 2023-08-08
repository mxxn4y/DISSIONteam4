using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//아영 작성 스크립트 NPC 대화 다이얼로그 활성화

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue; 
}

public class TalkScripts : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite_DialogueBox; 
    [SerializeField] private Text txt_Dialogue; 

    private bool isDialogue = false; 

    private int count = 0; 

    [SerializeField] private Dialogue[] dialogue; 

    public void ShowDialogue()
    {
        OnOff(true); 
        count = 0; 
        NextDialogue();
    }

    private void OnOff(bool _flag)
    {
        sprite_DialogueBox.gameObject.SetActive(_flag);
        txt_Dialogue.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }

    private void NextDialogue()
    {
        txt_Dialogue.text = dialogue[count].dialogue;
        count++;
    }

    void Start()
    {

    }

    void Update()
    {
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (count < dialogue.Length)
                {
                    NextDialogue();
                }
                else
                {
                    OnOff(false);
                }
            }
        }
    }

}
