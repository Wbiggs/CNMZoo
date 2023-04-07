using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject choiceButton;

    public DialogueReader currentReader {get; set;}
    public DialogueLeaf currentLeaf {get; set;}

    private List<DialogueChoice> currentLeafCoices = new List<DialogueChoice>();

    private void Start()
    {
        Messenger<int>.AddListener("ChoiceMade", OnChoice);    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            dialogueText.gameObject.SetActive(true);
            DisplayDialogueLeaf();
        }
    }

    public void DisplayDialogueLeaf()
    {
        GetChoices(currentLeaf);

        dialogueText.text = currentLeaf.text;
    }

    public void OnChoice(int choiceBranchIndex)
    {
        RequestTraverse(choiceBranchIndex);
    }

    private void GetChoices(DialogueLeaf leaf)
    {
        foreach(var choice in leaf.choices)
        {
            Debug.Log(choice.text);

            currentLeafCoices.Add(choice);
        }
    }

    private void RequestTraverse(int branchIndex)
    {
        currentReader.TraverseTree(branchIndex);
    }
}
