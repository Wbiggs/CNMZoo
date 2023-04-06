using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    [SerializeField] private List<DialogueBranch> branches = new List<DialogueBranch>();

    public DialogueBranch currentBranch {get; set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
