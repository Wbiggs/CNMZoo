using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager instance {get; private set; }

    [SerializeField] private TMP_Text dialogueText;

    private void Awake() 
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void DisplayNextLine()
    {
        //Todo
    }
}
