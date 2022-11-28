using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SquareFootageCalculation : MonoBehaviour
{
    public Slider slider1;
    public Slider slider2;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        text.text = slider1.value * slider2.value + " '";
    }

    
}
