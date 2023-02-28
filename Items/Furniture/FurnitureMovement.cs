using UnityEngine;
using System.Collections;

public class FurnitureMovement : MonoBehaviour {
    public GameObject cameraController;
    public GameObject WallManager;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool isDragging;
    private bool creationMode;
    public bool movedItem = false;
    public GameObject GridManager;
    public GameObject parent;
    public int[] oldEdges =  new int[4];
    private int[] newEdges = new int[4];
    int newLeftEdge;
    int newRightEdge;
    int newBottomEdge;
    int newTopEdge;
    public bool firstFrameCreated = true;

    Vector3 oldPosition;
    public void Start() {
        cameraController =  GameObject.FindWithTag("CameraController");
        GridManager = GameObject.FindWithTag("GridManager");
        parent = gameObject.transform.parent.gameObject;
        WallManager = GameObject.FindWithTag("WallManager");
    }         

    void Select() {

        gameObject.GetComponent<FurnitureState>().isSelected = true;
        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        
        //Debug.Log(GameObject.ReferenceEquals(selected, gameObject));
        if(!GameObject.ReferenceEquals(selected, gameObject)) {
            if(selected != null) {
                selected.GetComponent<FurnitureState>().destoryFurnitureUI();
                selected.GetComponent<FurnitureState>().isSelected = false;
                selected.tag = "Furniture";
            }
            gameObject.GetComponent<FurnitureState>().createFurnitureUI();
            gameObject.tag = "SelectedFurniture";
        }
        
    }

    void Deselect() {

        gameObject.GetComponent<FurnitureState>().isSelected = false;
        gameObject.GetComponent<FurnitureState>().destoryFurnitureUI();
        gameObject.tag = "Furniture";

    }
    
    public void calculateEdges()
    {
        Vector3 objectScale = gameObject.transform.localScale;
        
        gameObject.GetComponent<FurnitureState>().leftEdge = gameObject.transform.position.x - ((objectScale.x - 1.0f) * 0.5f);
        gameObject.GetComponent<FurnitureState>().rightEdge = gameObject.GetComponent<FurnitureState>().leftEdge + objectScale.x;
        gameObject.GetComponent<FurnitureState>().bottomEdge = gameObject.transform.position.y - ((objectScale.y - 1.0f) * 0.5f);
        gameObject.GetComponent<FurnitureState>().topEdge = gameObject.GetComponent<FurnitureState>().bottomEdge + objectScale.y;
    }
    void Update()
    {
        Vector3 objectScale = gameObject.transform.localScale;
        float halfX = objectScale.x / 2.0f;
        float halfY = objectScale.y / 2.0f;
        float halfZ = objectScale.z / 2.0f;
        Vector3 objectPos = gameObject.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //selecting
        if((Input.GetMouseButtonUp(0)))
        { 
            
            if (mousePos.x > objectPos.x - halfX && 
                mousePos.x < objectPos.x + halfX &&
                mousePos.y > objectPos.y - halfY && 
                mousePos.y < objectPos.y + halfY &&
                mousePos.z > objectPos.z - halfZ && 
                mousePos.z < objectPos.z + halfZ)
            {
                
                if(cameraController.GetComponent<CameraController>().moved == false && parent.GetComponent<TileManager>().movedItem == false 
                    && WallManager.GetComponent<WallManager>().created == false) {
                    
                    if(gameObject.GetComponent<FurnitureState>().isSelected == false) {
                        Select();
                    }
                    else{
                        Deselect();
                    }
                    Debug.Log("entered 1");
                    
                }
                
                //gameObject.GetComponent<FurnitureState>().isSelected = !gameObject.GetComponent<FurnitureState>().isSelected;
                 
            }
        }

        if((Input.GetMouseButtonDown(0) && gameObject.GetComponent<FurnitureState>().isSelected)
        
        || firstFrameCreated == true) {

            if(mousePos.x > objectPos.x - halfX && 
            mousePos.x < objectPos.x + halfX &&
            mousePos.y > objectPos.y - halfY && 
            mousePos.y < objectPos.y + halfY &&
            mousePos.z > objectPos.z - halfZ && 
            mousePos.z < objectPos.z + halfZ) {

                if(gameObject.GetComponent<FurnitureState>().isFirstCreated == true) {
                    Select();
                }

                cameraController.GetComponent<CameraController>().isDraggable = false;
                //need this line for some reason
                firstFrameCreated = false;
                isDragging = true;
                screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

                oldPosition = gameObject.transform.position;
                oldEdges[0] = (int) gameObject.GetComponent<FurnitureState>().leftEdge;
                oldEdges[1] = (int) gameObject.GetComponent<FurnitureState>().rightEdge;
                oldEdges[2] = (int) gameObject.GetComponent<FurnitureState>().bottomEdge;
                oldEdges[3] = (int) gameObject.GetComponent<FurnitureState>().topEdge;

            }
        }   
                

        if ((Input.GetMouseButton(0) && isDragging))
        { 
            //gameObject.GetComponent<FurnitureState>().destoryFurnitureUI();
            Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
            gameObject.GetComponent<FurnitureState>().isMoving = true;
            float offsetX = 0.0f;
            float offsetY = 0.0f;
            float offsetZ = 0.0f;
            if(objectScale.x % 2 == 0) {
                offsetX = 0.5f;
            }
            if(objectScale.y % 2 == 0) {
                offsetY = 0.5f;
            }
            if(objectScale.z % 2 == 0) {
                offsetZ = 0.5f;
            }
            
            Vector3 newPosition = new Vector3(Mathf.Floor(cursorPosition.x) + offsetX, Mathf.Floor(cursorPosition.y) + offsetY, Mathf.Floor(cursorPosition.z) + offsetZ);
            
            if(gameObject.transform.position.x != newPosition.x || gameObject.transform.position.y != newPosition.y) {
                
                newLeftEdge = (int) (newPosition.x - ((objectScale.x - 1.0f) * 0.5f));
                newRightEdge = (int) (newLeftEdge + objectScale.x);
                newBottomEdge = (int) (newPosition.y - ((objectScale.y - 1.0f) * 0.5f));
                newTopEdge = (int) (newBottomEdge + objectScale.y);

                movedItem = true;
                parent.GetComponent<TileManager>().movedItem = true;

                gameObject.transform.position = newPosition; 
                
                
                calculateEdges();
                /*
                newEdges[0] = (int) gameObject.GetComponent<FurnitureState>().leftEdge;
                newEdges[1] = (int) gameObject.GetComponent<FurnitureState>().rightEdge;
                newEdges[2] = (int) gameObject.GetComponent<FurnitureState>().bottomEdge;
                newEdges[3] = (int) gameObject.GetComponent<FurnitureState>().topEdge;
                */
            }
        }   
        
        if (Input.GetMouseButtonUp(0))
        {
            
            bool tilesOccupied = false;
            if(movedItem == true) {
                
                if(!(newLeftEdge < 0 || newRightEdge > parent.GetComponent<TileManager>().occupied.GetLength(0) || newBottomEdge < 0 || newTopEdge > parent.GetComponent<TileManager>().occupied.GetLength(1))) {
                    
                    for(int i = newLeftEdge; i < newRightEdge; i++) {
                        for(int j = newBottomEdge; j < newTopEdge; j++) {
                            if(parent.GetComponent<TileManager>().occupied[i,j] == 1) {
                                
                                if(!(i >= oldEdges[0] && i < oldEdges[1] && j >= oldEdges[2] && j < oldEdges[3])) {
                                    tilesOccupied = true;
                                    break;
                                }
                            }
                        }
                    }

                    if(tilesOccupied) {
                        if(gameObject.GetComponent<FurnitureState>().isFirstCreated == true) {
                            Destroy(gameObject);
                            gameObject.GetComponent<FurnitureState>().destoryFurnitureUI();
                        }
                        else {
                            gameObject.transform.position = oldPosition;
                            calculateEdges();
                        }
                    }
                }

                /*
                if (mousePos.x > objectPos.x - halfX && 
                    mousePos.x < objectPos.x + halfX &&
                    mousePos.y > objectPos.y - halfY && 
                    mousePos.y < objectPos.y + halfY &&
                    mousePos.z > objectPos.z - halfZ && 
                    mousePos.z < objectPos.z + halfZ) {

                    Debug.Log("entered 3");
                    Select();
                }
                */

                //Debug.Log("entered 3");
                //Select();
            }
            

            if(movedItem == true || gameObject.GetComponent<FurnitureState>().rotatedF == true) {
                newEdges[0] = (int) gameObject.GetComponent<FurnitureState>().leftEdge;
                newEdges[1] = (int) gameObject.GetComponent<FurnitureState>().rightEdge;
                newEdges[2] = (int) gameObject.GetComponent<FurnitureState>().bottomEdge;
                newEdges[3] = (int) gameObject.GetComponent<FurnitureState>().topEdge;

                if(!(gameObject.GetComponent<FurnitureState>().isFirstCreated == true && tilesOccupied == true)) {
                    parent.GetComponent<TileManager>().furniturePlaced(oldEdges, newEdges);
                }
            }
            cameraController.GetComponent<CameraController>().isDraggable = true;
            gameObject.GetComponent<FurnitureState>().isMoving = false;
            isDragging = false;
            gameObject.GetComponent<FurnitureState>().isFirstCreated = false;
            movedItem = false;
           
        }
    }

    void LateUpdate() {

        if(Input.GetMouseButtonUp(0)) {
            cameraController.GetComponent<CameraController>().moved = false;
            gameObject.GetComponent<FurnitureState>().rotatedF = false;
            parent.GetComponent<TileManager>().movedItem = false;
        }
    }
}