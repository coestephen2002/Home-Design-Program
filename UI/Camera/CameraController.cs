using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
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
    Vector3 oldPos;
    public Slider length;
    public Slider width;
    public bool isDraggable = true;
    public bool moved;
    // Update is called once per frame
     public void getPos() {
        bottomLeftPos = camera.WorldToScreenPoint(bottomLeft.transform.position);
        topRightPos = camera.WorldToScreenPoint(topRight.transform.position);
        
        Debug.Log(bottomLeftPos);
        Debug.Log(topRightPos);
        
    }

    void Update()
   {
    if(Input.mousePosition.x > bottomLeftPos.x && Input.mousePosition.x < topRightPos.x && Input.mousePosition.y > bottomLeftPos.y && Input.mousePosition.y < topRightPos.y && EventSystem.current.IsPointerOverGameObject() == false)
    {
    Zoom(Input.GetAxis("Mouse ScrollWheel"));
    Drag();
    }
   }

    private void Drag()
    {
        if(isDraggable == true)
        {
        if(Input.GetMouseButtonDown(0)) {
            oldPos = Camera.main.transform.position;
        }
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
            //Vector3 oldPos = Camera.main.transform.position;
            Camera.main.transform.position = Origin - Difference;

            if(Mathf.Abs(Camera.main.transform.position.x - oldPos.x) > 0.10f || Mathf.Abs(Camera.main.transform.position.y - oldPos.y) > 0.10f) {
                moved = true;
            }
            
        }
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

