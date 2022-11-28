using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FurnitureCreation : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] public Slider widthSlider;
    [SerializeField] public Slider lengthSlider;
    [SerializeField] TMP_InputField input;

    public void createFurniture() {
        
        float width = Mathf.Floor(widthSlider.value);
        float length = Mathf.Floor(lengthSlider.value);

        var furniture = Instantiate(prefab, new Vector3(width / 2.0f - 0.5f, length / 2.0f - 0.5f, 1.0f), Quaternion.identity);
        
        if(input.text == "") {
            furniture.name = "Furniture";
        }
        else{
            furniture.name = input.text;
        }
        

        TMP_Text text = (TMP_Text) furniture.transform.GetChild(0).GetComponent<TMP_Text>();
        text.text = furniture.name;

        furniture.transform.localScale = new Vector3(width, length, 1.0f);

        if(length >= width) {
             text.transform.localScale = new Vector3((length/width) * (1.0f/length), 1.0f * (1.0f/length), 1.0f);
        }
        else if(width > length) {
             text.transform.localScale = new Vector3(1.0f * (1.0f/width), (width/length) * (1.0f/width), 1.0f);
        }
        
        widthSlider.value = 1.0f;
        lengthSlider.value = 1.0f;
        input.text = "";
        
    }
}
