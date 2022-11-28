using UnityEngine;
using UnityEngine.UI;
public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mouseWorldPosStart;
    public Camera camera;
    private float zoomScale = 10.0f;
    private float zoomMin = 5.0f;
    private float zoomMax = 50.0f;
    private Vector3 Origin;
    private Vector3 Difference;
    private bool drag = false;
    public GameObject bottomLeft;
    public GameObject topRight;
    Vector3 bottomLeftPos;
    Vector3 topRightPos;
    public Slider length;
    public Slider width;
    // Update is called once per frame
     public void getPos() {
        bottomLeftPos = camera.WorldToScreenPoint(bottomLeft.transform.position);
        topRightPos = camera.WorldToScreenPoint(topRight.transform.position);
        
        Debug.Log(bottomLeftPos);
        Debug.Log(topRightPos);
        
    }

    void Update()
   {
    if(Input.mousePosition.x > bottomLeftPos.x && Input.mousePosition.x < topRightPos.x && Input.mousePosition.y > bottomLeftPos.y && Input.mousePosition.y < topRightPos.y )
    {
    Zoom(Input.GetAxis("Mouse ScrollWheel"));
    Drag();
    }
   }

    private void Drag()
    {
        if(Input.GetMouseButton(0))
        {
            Difference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if(drag == false)
            {
                drag = true;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            drag = false;
        }
        if(drag)
        {
            Camera.main.transform.position = Origin - Difference;
        }
        
    }

    private void Zoom(float zoomDiff)
    {
        if(zoomDiff != 0)
        {
            mouseWorldPosStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - zoomDiff * zoomScale, zoomMin, zoomMax);
            Vector3 mouseWorldPosDiff = mouseWorldPosStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            camera.transform.position += mouseWorldPosDiff;
            
        }
    }

}

