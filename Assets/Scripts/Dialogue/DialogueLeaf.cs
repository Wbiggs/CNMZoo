using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class DialogueLeaf : IDialogueNode
{
    enum EndAction : ushort
    {
        NextNode = 0,
        Choice = 1,
        End = 2
    }
    [SerializeField] private EndAction endAction = EndAction.NextNode;

    [TextArea(3, 4)] public string text;

    public List<DialogueChoice> choices = new List<DialogueChoice>();
    public List<DialogueEvent> events = new List<DialogueEvent>();

    public string Execute()
    {
        if(endAction == EndAction.NextNode)
        {
            //ToDo
        }
        else if(endAction == EndAction.Choice)
        {
            //ToDo
        }
        else if (endAction == EndAction.End)
        {
            //ToDo
        }

        return "Success";
    }
}
