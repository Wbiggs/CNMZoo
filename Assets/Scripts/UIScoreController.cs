using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreLable;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreLable.text = score.ToString();

    }

    // Send Message to changeScore to increment score by +1
    public void changeScore()
    {
        score += 1;
        scoreLable.text = score.ToString();
    }

    //example below of sending message to score (make sure to add score to GameObject
    /*
     * [SerializeField] GameObject[] score;
     * 
    private void OnTriggerExit(Collider other)
    {
        score.SendMessage("changeScore");
        
    }*/
}
