using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueTree))]
public class DialogueReader : MonoBehaviour
{
    [SerializeField] private DialogueTree tree;
    [SerializeField] private DialogueUIManager uiManager;

    private Queue<DialogueLeaf> currentBranchLeaves = new Queue<DialogueLeaf>();
    public DialogueLeaf currentLeaf { get; set; }

    //Testing code
    private void Start() 
    {
        TraverseTree(0);
    }

    public void TraverseTree(int branchIndex)
    {
        uiManager.currentReader = this;

        foreach (var leaf in tree.branches[branchIndex].leaves)
        {
            currentBranchLeaves.Enqueue(leaf);
        }

        GetNextLeaf();
    }

    private void TriggerLeafEvents(DialogueLeaf leaf)
    {
        foreach (var leafEvent in leaf.events)
        {
            leafEvent.Trigger();
        }
    }

    public void GetNextLeaf()
    {
        currentLeaf = currentBranchLeaves.Dequeue();
        uiManager.currentLeaf = currentLeaf;

        if(currentLeaf.events.Count > 0)
        {
            TriggerLeafEvents(currentLeaf);
        }
    }
}
