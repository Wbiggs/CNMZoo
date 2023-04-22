using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuPopup : MonoBehaviour
{

    //[SerializeField] TMP_InputField nameInput;
    //[SerializeField] Slider speedSlider;
    public void Start()
    {
        //nameInput.text = PlayerPrefs.GetString("Name", "Blondie");
        //speedSlider.value = PlayerPrefs.GetFloat("Speed", 1);
        Close();
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    public void Close()
    {
        gameObject.SetActive(false);
    }

    /*public void OnSubmitName(string name)
    {
        Debug.Log(name);
        PlayerPrefs.SetString("Name", name);
    }

    public void OnSpeedValue(float speed)
    {
        //Debug.Log($"Speed: {speed}");
        PlayerPrefs.SetFloat("Speed", speed);
    }*/


}
