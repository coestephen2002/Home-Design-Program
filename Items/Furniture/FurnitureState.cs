using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FurnitureState : MonoBehaviour
{   
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    public bool isSelected = true;
    [SerializeField]
    public bool isMoving = true;
    [SerializeField]
    public bool isFirstCreated = true;
    [SerializeField]
    public Vector2 position;
    [SerializeField]
    public float posx;
    [SerializeField]
    public float posy;
    float gridHeight;
    float gridWidth;
    public float furnitureWidth;
    public float temp;
    public float furnitureHeight;
    GameObject parent;
    GameObject workspace;
    public GameObject furnitureUI;
    GameObject furnUI;
    public float leftEdge;
    public float rightEdge;
    public float bottomEdge;
    public float topEdge;
    public bool rotated = false;
    public bool rotatedF = false;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.gameObject;
        workspace = parent.transform.parent.gameObject;
        gridWidth = workspace.GetComponent<workspaceInfo>().width;
        Debug.Log(gridWidth);
        gridHeight = workspace.GetComponent<workspaceInfo>().height;
        Debug.Log(gridHeight);
        isFirstCreated = true;
        furnitureWidth = gameObject.transform.localScale.x;
        furnitureHeight = gameObject.transform.localScale.y;
        
    }

    public void createFurnitureUI()
    {
       furnUI = Instantiate(furnitureUI, GameObject.FindWithTag("ExampleUI").transform, false);
       furnUI.transform.localScale = new Vector3(65,65,1);
       furnUI.transform.localPosition = new Vector3(500,185,0);
    }
    public void destoryFurnitureUI()
    {
        Destroy(furnUI);
    }
    
    public void deletePressed()
    {
        int[] oldEdges = new int[4] {(int) leftEdge, (int) rightEdge, (int) bottomEdge, (int) topEdge};
        parent.GetComponent<TileManager>().furniturePlaced(oldEdges, new int [4] {-1, 0, 0, 0});

        Destroy(gameObject);
        Destroy(furnUI);
    }
    public void rotatePressed()
    {

        if((int) furnitureWidth == (int) furnitureHeight) {
            return;
        }

        int[] oldEdges = new int[4] {(int) leftEdge, (int) rightEdge, (int) bottomEdge, (int) topEdge};
        
        temp = furnitureWidth;
        furnitureWidth = furnitureHeight;
        furnitureHeight = temp;
        
        Vector3 newScale = new Vector3(furnitureWidth, furnitureHeight, 1);
        //gameObject.transform.localScale = new Vector3(furnitureWidth, furnitureHeight, 1);

        Vector3 newPosition = new Vector3();
        if(furnitureHeight % 2 != 0 && furnitureWidth % 2 != 0)
        {
            newPosition = gameObject.transform.position;
        }
        else if(furnitureHeight % 2 == 0 && furnitureWidth % 2 == 0)
        {
            newPosition = gameObject.transform.position;
        }
        else if(furnitureHeight != furnitureWidth && rotated == false)
        {
            newPosition = new Vector3(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
        }
        else if(furnitureHeight != furnitureWidth && rotated == true)
        {
            newPosition = new Vector3(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.5f, gameObject.transform.position.z);
        }

        int newLeftEdge = (int) (newPosition.x - ((newScale.x - 1.0f) * 0.5f));
        int newRightEdge = (int) (newLeftEdge + newScale.x);
        int newBottomEdge = (int) (newPosition.y - ((newScale.y - 1.0f) * 0.5f));
        int newTopEdge = (int) (newBottomEdge + newScale.y);

        Debug.Log("left: " + newLeftEdge);
        Debug.Log("right: " + newRightEdge);
        Debug.Log("bottom: " + newBottomEdge);
        Debug.Log("top: " + newTopEdge);

        
        bool valid = true;
        if(newLeftEdge < 0 || newRightEdge > parent.GetComponent<TileManager>().occupied.GetLength(0) || 
            newBottomEdge < 0 || newTopEdge > parent.GetComponent<TileManager>().occupied.GetLength(1)) {

            valid = false;
        }
        else { 
            
            for(int i = newLeftEdge; i < newRightEdge; i++) {
                for(int j = newBottomEdge; j < newTopEdge; j++) {
                    if(parent.GetComponent<TileManager>().occupied[i,j] == 1) {
                                
                        if(!(i >= oldEdges[0] && i < oldEdges[1] && j >= oldEdges[2] && j < oldEdges[3])) {
                            valid = false;
                            break;
                        }
                    }
                }
            }
        }
        
        if(valid) {
            gameObject.transform.position = newPosition;
            gameObject.transform.localScale = newScale;

            TMP_Text text = (TMP_Text) gameObject.transform.GetChild(0).GetComponent<TMP_Text>();

        
            if(furnitureHeight >= furnitureWidth) {
                text.transform.localScale = new Vector3((furnitureHeight/furnitureWidth) * (1.0f/furnitureHeight), 1.0f * (1.0f/furnitureHeight), 1.0f);
            }
            else if(furnitureWidth > furnitureHeight) {
                text.transform.localScale = new Vector3(1.0f * (1.0f/furnitureWidth), (furnitureWidth/furnitureHeight) * (1.0f/furnitureWidth), 1.0f);
            }

            rotated = !rotated;
            rotatedF = true;
            gameObject.GetComponent<FurnitureMovement>().oldEdges = oldEdges;
            gameObject.GetComponent<FurnitureMovement>().calculateEdges(); 
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            temp = furnitureWidth;
            furnitureWidth = furnitureHeight;
            furnitureHeight = temp;

            var furniture = Instantiate(prefab, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, parent.transform);
            furniture.GetComponent<SpriteRenderer>().color = Color.red;
            furniture.GetComponent<Renderer>().sortingOrder = 2;
            TMP_Text text = (TMP_Text) furniture.transform.GetChild(0).GetComponent<TMP_Text>();
            text.text = "";
            furniture.transform.localScale = newScale;
            furniture.transform.position = newPosition;
            furniture.tag = "Trash";
            
            Destroy(furniture, 0.1f);
            
            
        }
        
    }
    // Update is called once per frame
    void Update()
    {

        if(isMoving == false && isSelected == true)
        {
            if(gameObject.tag != "Trash") {
                gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
        }
        if(gameObject.GetComponent<FurnitureMovement>().movedItem == true || isFirstCreated == true)
        {
           posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
           if(posx + (gameObject.transform.localScale.x / 2.0f) > gridWidth || posy + (gameObject.transform.localScale.y / 2.0f) > gridHeight || 
               posx - (gameObject.transform.localScale.x / 2.0f) < -1 || posy - (gameObject.transform.localScale.y / 2.0f) < -1) {

                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
           else {
               if(gameObject.tag != "Trash") {

                    bool tilesOccupied = false;

                    for(int i = (int) leftEdge; i < (int) rightEdge; i++) {
                        for(int j = (int) bottomEdge; j < (int) topEdge; j++) {
                            if(parent.GetComponent<TileManager>().occupied[i,j] == 1) {

                                if(!(i >= gameObject.GetComponent<FurnitureMovement>().oldEdges[0] && i < gameObject.GetComponent<FurnitureMovement>().oldEdges[1] 
                                    && j >= gameObject.GetComponent<FurnitureMovement>().oldEdges[2] && j < gameObject.GetComponent<FurnitureMovement>().oldEdges[3])) {
                                    tilesOccupied = true;
                                    break;
                                }
                            }
                        }
                    }


                    if(tilesOccupied) {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    }
                    else {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    }
           }
           }
           

        }
        else
        {
            posx = gameObject.transform.position.x;
            posy = gameObject.transform.position.y;
            position = new Vector2(posx, posy);
            if(isSelected == false)
            {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
            } 
            if(posx + (gameObject.transform.localScale.x / 2.0f) > gridWidth || posy + (gameObject.transform.localScale.y / 2.0f) > gridHeight || 
               posx - (gameObject.transform.localScale.x / 2.0f) < -1 || posy - (gameObject.transform.localScale.y / 2.0f) < -1 && isMoving == false)
            {
                Destroy(furnUI);
                Destroy(gameObject);
            }
        }
    }
}
