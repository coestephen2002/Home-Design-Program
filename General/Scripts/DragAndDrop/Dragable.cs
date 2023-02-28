using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragable : MonoBehaviour
{
    // GameObject variable to store the object that is being dragged
    [SerializeField]
    public GameObject dragObject;
    public void OnMouseDown()
    {
        // Set the dragObject to the current object
        Debug.Log("The object was clicked. Mouse down.");
        dragObject = this.gameObject;
    }

    public void OnMouseDrag()
    {
        // Update the position of the dragObject based on the current mouse position
        Debug.Log("The object was dragged.");
        Vector3 mousePosition = Input.mousePosition;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        dragObject.transform.position = newPosition;
    }

    public void OnMouseUp()
    {
        // Reset the dragObject to null
        Debug.Log("The object was unclicked. Mouse up.");
        dragObject = null;
    }
}