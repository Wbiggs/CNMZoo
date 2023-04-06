using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class DialogueNode : IDialogueNode
{
    enum EndAction : int
    {
        NextNode = 0,
        Choice = 1,
        End = 2
    }
    [SerializeField] private EndAction endAction = EndAction.NextNode;

    [SerializeField] private string text;

    public string Execute()
    {
        
        return "Success";
    }
}
