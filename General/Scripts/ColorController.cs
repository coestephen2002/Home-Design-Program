using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
public class ColorController : MonoBehaviour
{
    public TMP_InputField redText;
    public TMP_InputField blueText;
    public TMP_InputField greenText;
    public TMP_InputField alphaText;
    public Color newColor;
    public Camera camera;
    float redValue = 1.0f;
    float greenValue = 1.0f;
    float blueValue = 1.0f;
    float alphaValue = 1.0f;
    void Start()
    {
    }

    
    void Update()
    {
        string redString = redText.text;
        string greenString = greenText.text;
        string blueString = blueText.text;
        string alphaString = alphaText.text;
        if(float.TryParse(redString, out float redFloat))
        {
            Debug.Log(redFloat);
            redValue = redFloat;
        }
         if(float.TryParse(greenString, out float greenFloat))
        {
            Debug.Log(greenFloat);
            greenValue = greenFloat;
        }
          if(float.TryParse(blueString, out float blueFloat))
        {
            Debug.Log(blueFloat);
            blueValue = blueFloat;
        }
          if(float.TryParse(alphaString, out float alphaFloat))
        {
            Debug.Log(alphaFloat);
            alphaValue = alphaFloat;
        }
      
        newColor = new Color(redValue, greenValue, blueValue, alphaValue);
        camera.backgroundColor = newColor;
    }
}
