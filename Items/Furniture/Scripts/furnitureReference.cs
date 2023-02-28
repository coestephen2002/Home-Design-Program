using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furnitureReference : MonoBehaviour
{
    public GameObject parentFurniture;
    void Start()
    {
        parentFurniture = GameObject.FindWithTag("SelectedFurniture");
    }

    public void delete()
    {
        parentFurniture.GetComponent<FurnitureState>().deletePressed();
    }

    public void rotate()
    {
        parentFurniture.GetComponent<FurnitureState>().rotatePressed();
    }

    public void color()
    {
        
    }
    // Update is called once per frame
}
