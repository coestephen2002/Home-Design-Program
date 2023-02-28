using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDeletion : MonoBehaviour
{
    private GameObject WallManager;

    // Start is called before the first frame update
    void Start()
    {
        WallManager = GameObject.FindWithTag("WallManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(WallManager.GetComponent<WallManager>().deleting) {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            Vector3 position0 = gameObject.GetComponent<LineRenderer>().GetPosition(0);
            Vector3 position1 = gameObject.GetComponent<LineRenderer>().GetPosition(1);
            if(position0.x == position1.x) {
                float min = Mathf.Min(position0.y, position1.y);
                float max = Mathf.Max(position0.y, position1.y);
                
                if(mousePos.x > position0.x - 0.15f && mousePos.x < position0.x + 0.15f && mousePos.y > min && mousePos.y < max) {
                    gameObject.GetComponent<LineRenderer>().startColor = Color.red;
                    gameObject.GetComponent<LineRenderer>().endColor = Color.red;

                    if(Input.GetMouseButtonDown(0)) {
                        Destroy(gameObject);
                        WallManager.GetComponent<WallManager>().setDeleting();
                    }
                } 
                else {
                    gameObject.GetComponent<LineRenderer>().startColor = Color.black;
                    gameObject.GetComponent<LineRenderer>().endColor = Color.black;
                }
            }
            else if(position0.y == position1.y) {
                float min = Mathf.Min(position0.x, position1.x);
                float max = Mathf.Max(position0.x, position1.x);

                if(mousePos.y > position0.y - 0.15f && mousePos.y < position0.y + 0.15f && mousePos.x > min && mousePos.x < max) {
                    gameObject.GetComponent<LineRenderer>().startColor = Color.red;
                    gameObject.GetComponent<LineRenderer>().endColor = Color.red;

                    if(Input.GetMouseButtonDown(0)) {
                        Destroy(gameObject);
                        WallManager.GetComponent<WallManager>().setDeleting();
                    }
                }
                else {
                    gameObject.GetComponent<LineRenderer>().startColor = Color.black;
                    gameObject.GetComponent<LineRenderer>().endColor = Color.black;
                }
            }
        }
    }
}
