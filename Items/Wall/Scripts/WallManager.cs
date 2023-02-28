using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallManager : MonoBehaviour {

    [SerializeField] GameObject WorkspaceManager;
    [SerializeField] GameObject PointPrefab;
    [SerializeField] GameObject LinePrefab;
    [SerializeField] GameObject CameraController;

    [SerializeField] GameObject CreateButton;
    [SerializeField] GameObject DeleteButton;
    [SerializeField] GameObject CancelCreate;
    [SerializeField] GameObject CancelDelete;

    [SerializeField] GameObject ResetButton;
    [SerializeField] GameObject ExportButton;
    [SerializeField] GameObject DeleteWorkspaceButton;

    public bool created;
    public bool creating;
    public bool deleting;

    private bool first = true;
    private bool firstCreated = false;
    private bool firstSet = false;
    private GameObject firstPoint;

    private bool second = false;
    private bool secondCreated = false;
    private bool secondSet = false;
    private GameObject secondPoint;
    public int count = 0;
    
    public void setCreating() {
        
        if(deleting) {
            setDeleting();
        }
        
        if(creating) {
            turnOffCreating();
            ResetButton.SetActive(true);
            ExportButton.SetActive(true);
            DeleteWorkspaceButton.SetActive(true);
        }
        else {
            ResetButton.SetActive(false);
            ExportButton.SetActive(false);
            DeleteWorkspaceButton.SetActive(false);
            creating = true;
        }

    }

    public void setDeleting() {
        
        if(deleting) {
            DeleteButton.SetActive(true);
            CancelDelete.SetActive(false);
            ResetButton.SetActive(true);
            ExportButton.SetActive(true);
            DeleteWorkspaceButton.SetActive(true);
        }
        else {
            CancelDelete.SetActive(true);
            DeleteButton.SetActive(false);
            ResetButton.SetActive(false);
            ExportButton.SetActive(false);
            DeleteWorkspaceButton.SetActive(false);
        }
        
        deleting = !deleting;
        if(creating) {
            turnOffCreating();
        }
    }

    public void turnOffDeleting() {
        deleting = false;
        ResetButton.SetActive(true);
        ExportButton.SetActive(true);
        DeleteWorkspaceButton.SetActive(true);
        DeleteButton.SetActive(true);
        CancelDelete.SetActive(false);

    }

    public void turnOffCreating() {

        if(count == 0) {
            creating = false;
            Destroy(firstPoint);
            firstPoint = null;
            first = true;
            firstCreated = false;
            secondCreated = false;
            firstSet = false;
            secondSet = false;
            count = 0;
        }
        else if(count >= 1) {
            Destroy(firstPoint);
            Destroy(secondPoint);
            firstPoint = null;
            secondPoint = null;
            
            creating = false;
            first = true;
            firstCreated = false;
            secondCreated = false;
            firstSet = false;
            secondSet = false;
            count = 0;
        }

        CreateButton.SetActive(true);
        CancelCreate.SetActive(false);
        
        if(!deleting) {
            ResetButton.SetActive(true);
            ExportButton.SetActive(true);
            DeleteWorkspaceButton.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        if(selected != null) {
            turnOffCreating();
            if(deleting) {
                setDeleting();
            }
        }
        
        GameObject workspace = WorkspaceManager.GetComponent<workspace_data>().currentWorkspace;
        
        if(creating) {
            
            created = true;
            Transform parentFloor = workspace.GetComponent<workspaceInfo>().currentFloor.transform;
            
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(first) {
                if(!firstCreated) {
                    if(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f) {
                        firstPoint = Instantiate(PointPrefab, new Vector3(Mathf.Ceil(mousePos.x) - 0.5f, Mathf.Ceil(mousePos.y) - 0.5f, 1.0f), Quaternion.identity, parentFloor);
                        firstCreated = true;
                    }
                }
                else {
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(firstPoint.transform.position);
                    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
                    firstPoint.transform.position = new Vector3(Mathf.Ceil(cursorPosition.x) - 0.5f, Mathf.Ceil(cursorPosition.y) - 0.5f, cursorPosition.z);
                    if(!(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f)) {
                        Destroy(firstPoint);
                        firstCreated = false;
                    }
                    if(Input.GetMouseButtonUp(0) && CameraController.GetComponent<CameraController>().moved == false && EventSystem.current.IsPointerOverGameObject() == false) {
                        firstSet = true;
                        first = false;
                        second = true;
                        count = 1;
                    }
                }
                
            }
            else if(second) {
                if(!secondCreated) {
                   if(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f) {
                        secondPoint = Instantiate(PointPrefab, new Vector3(Mathf.Ceil(mousePos.x) - 0.5f, Mathf.Ceil(mousePos.y) - 0.5f, 1.0f), Quaternion.identity, parentFloor);
                        if(firstPoint.transform.position.x == secondPoint.transform.position.x ^ firstPoint.transform.position.y == secondPoint.transform.position.y) {
                            secondPoint.GetComponent<SpriteRenderer>().color = new Color(0.60f, 1.0f, 0.60f, 1.0f);
                        }
                        else {
                            secondPoint.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.25f, 0.20f, 1.0f);
                        }
                        secondCreated = true;
                    } 
                }
                else {
                    Vector3 screenPoint = Camera.main.WorldToScreenPoint(firstPoint.transform.position);
                    Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                    Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint);
                    secondPoint.transform.position = new Vector3(Mathf.Ceil(cursorPosition.x) - 0.5f, Mathf.Ceil(cursorPosition.y) - 0.5f, cursorPosition.z);

                    if(firstPoint.transform.position.x == secondPoint.transform.position.x ^ firstPoint.transform.position.y == secondPoint.transform.position.y) {
                        secondPoint.GetComponent<SpriteRenderer>().color = new Color(0.60f, 1.0f, 0.60f, 1.0f);
                    }
                    else {
                        secondPoint.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.25f, 0.20f, 1.0f);
                    }

                    if(!(mousePos.x > -0.5f && mousePos.x < workspace.GetComponent<workspaceInfo>().width - 0.5f && mousePos.y > -0.5f && mousePos.y < workspace.GetComponent<workspaceInfo>().height - 0.5f)) {
                        Destroy(secondPoint);
                        secondCreated = false;
                    }
                    if(Input.GetMouseButtonUp(0) && CameraController.GetComponent<CameraController>().moved == false && EventSystem.current.IsPointerOverGameObject() == false) {
                        if(firstPoint.transform.position.x == secondPoint.transform.position.x ^ firstPoint.transform.position.y == secondPoint.transform.position.y) {
                            secondSet = true;
                            second = false;
                            count = 2;
                        } 
                    }
                }
            }
             
            if(firstSet && secondSet) {
                GameObject line = Instantiate(LinePrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, parentFloor);
                line.name = "Wall";
                line.GetComponent<LineRenderer>().startWidth = 0.15f;
                line.GetComponent<LineRenderer>().endWidth = 0.15f;
                line.GetComponent<LineRenderer>().positionCount = 2;
                line.GetComponent<LineRenderer>().useWorldSpace = true;    
                
                //For drawing line in the world space, provide the x,y,z values
                line.GetComponent<LineRenderer>().SetPosition(0, firstPoint.transform.position); //x,y and z position of the starting point of the line
                line.GetComponent<LineRenderer>().SetPosition(1, secondPoint.transform.position); //x,y and z position of the end point of the line

                //created = true;
                setCreating();
            }
        }
    }

    void LateUpdate() {

        if(Input.GetMouseButtonUp(0)) {
            CameraController.GetComponent<CameraController>().moved = false;
            created = false;
        }
    }
}
