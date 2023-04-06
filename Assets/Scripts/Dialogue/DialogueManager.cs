using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance {get; private set; }

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
}
