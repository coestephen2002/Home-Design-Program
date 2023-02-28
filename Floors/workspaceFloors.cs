using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class workspaceFloors : MonoBehaviour
{
    [SerializeField] GameObject floorList;
    [SerializeField] GameObject floorListParent;
    
    public void instantiateFloorList(){
        GameObject list = Instantiate(floorList, floorListParent.transform);
    }
}
