using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Use this class to increase score by 1 point
/// simply attach it to the object detecting the collision and it should increase the score
/// you must assign score to TextMeshProUGUI score
/// </summary>
public class ScoreIncrease : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        score.SendMessage("changeScore");
    }
}
