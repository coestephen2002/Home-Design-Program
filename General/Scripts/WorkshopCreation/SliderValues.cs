using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderValues : MonoBehaviour
{
    [SerializeField]
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void textUpdate(float value)
    {
        text.text = Mathf.Floor(value) + "'";
    }
}
