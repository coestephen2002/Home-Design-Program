using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ViewSelector : MonoBehaviour
{
    public bool wallMode = false;
    public bool furnitureMode = true;
    [SerializeField]
    public GameObject wallButton;
    [SerializeField]
    public GameObject furnitureButton;
    [SerializeField]
    public GameObject furnitureBank;
    // Start is called before the first frame update
    void Start()
    {
        furnitureButton.GetComponent<Image>().color = new Color(0.55f, 1.0f, 0.55f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(furnitureBank.activeSelf == false)
        {
            furnitureButton.GetComponent<Image>().color = Color.white;
            furnitureMode = false;
            wallMode = true;
            wallButton.GetComponent<Image>().color = new Color(0.55f, 1.0f, 0.55f, 1.0f);
        }
        else
        {
            furnitureButton.GetComponent<Image>().color = new Color(0.55f, 1.0f, 0.55f, 1.0f);
            furnitureMode = true;
            wallMode = false;
             wallButton.GetComponent<Image>().color = Color.white;
        }
    }
}
