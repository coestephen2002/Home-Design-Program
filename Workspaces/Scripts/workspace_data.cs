using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class workspace_data : MonoBehaviour
{
    [SerializeField]
    public GameObject workspace;
    public List<GameObject> workspaces;
    public List<GameObject> buttons;
    public string name;
    public GameObject currentWorkspace;
    [SerializeField]
    public GameObject workspaceButton;
    [SerializeField]
    public GameObject buttonHolder;
    public GameObject floor;
    public GameObject floorList;
    public List<GameObject> floorListList;
    public GameObject ExampleUI;
    public GameObject currentFloorList;
    public int workspaceCount = 0;
    public GameObject WorkspaceCreateUI;
    private GameObject WallManager;
      
    void Awake() {
        WallManager = GameObject.FindWithTag("WallManager");
    }

    public void Deselect() {

        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        if(selected != null) {
            selected.GetComponent<FurnitureState>().destoryFurnitureUI();
            selected.GetComponent<FurnitureState>().isSelected = false;
            selected.tag = "Furniture";
            selected.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
        }
    }
      
      
    public void workspaceCreated()
    {
       WallManager.GetComponent<WallManager>().turnOffCreating();
       WallManager.GetComponent<WallManager>().turnOffDeleting();
       
       name = GameObject.FindWithTag("ProjectNameInput").GetComponent<TMP_InputField>().text;
       GameObject created = Instantiate(workspace, GameObject.FindWithTag("WorkspaceManager").transform);
       created.name = name;
       GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().GenerateGrid(created);
       AddNewWorkspaceToList(created);
       currentWorkspace = created;
       created.GetComponent<workspaceInfo>().width =  GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().width;
       created.GetComponent<workspaceInfo>().height= GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().height;
    //    created.GetComponent<workspaceFloors>().instantiateFloorList();

       GameObject newbutton = Instantiate(workspaceButton);
       newbutton.transform.SetParent(buttonHolder.transform);
       newbutton.transform.GetChild(0).GetComponent<TMP_Text>().text = created.name;
       newbutton.transform.GetChild(1).GetComponent<TMP_Text>().text = created.GetComponent<workspaceInfo>().width + " x " +  created.GetComponent<workspaceInfo>().height;
       newbutton.transform.localScale = new Vector3(1.0f,1.0f,1.0f);
       newbutton.transform.localPosition = new Vector3(newbutton.transform.localPosition.x,newbutton.transform.localPosition.y,0.0f);
       newbutton.GetComponent<Image>().color = new Color(0.55f, 1.0f, 0.55f, 1.0f);
       newbutton.GetComponent<Button>().onClick.AddListener(() => SwitchWorkspace(newbutton));
       AddNewWorkspaceToButtonList(newbutton);
       GameObject theFloorList = Instantiate(floorList, ExampleUI.transform);
       theFloorList.name = "theFloorList" + name;
       AddNewFloorListToFloorListList(theFloorList);
       currentFloorList = theFloorList;
       created.GetComponent<workspaceInfo>().addFloor();
       currentWorkspace = created;
       workspaceCount++;
        
        foreach (GameObject theworkspace in workspaces)
        {
             if(theworkspace != created)
             {
                theworkspace.SetActive(false);
             }   
        }
        foreach (GameObject button in buttons)
        {
             if(button != newbutton)
             {
                button.GetComponent<Image>().color = Color.white;
             }   
        }
          foreach (GameObject floorList in floorListList)
        {
             if(floorList != theFloorList)
             {
                floorList.SetActive(false);
             }   
        }
    }

    public void workspaceDeleted()
    {
        WallManager.GetComponent<WallManager>().turnOffCreating();
        WallManager.GetComponent<WallManager>().turnOffDeleting();
        
        if(workspaceCount >= 1)
        {
            int position = workspaces.IndexOf(currentWorkspace);
            if(workspaceCount == 1)
            {
                Destroy(workspaces[position].gameObject);
                Destroy(floorListList[position].gameObject);
                Destroy(buttons[position].gameObject);
                workspaces.RemoveAt(position);
                floorListList.RemoveAt(position);
                buttons.RemoveAt(position);
                workspaceCount--;
                return;
            }
            
            if(currentWorkspace == workspaces[workspaceCount - 1].gameObject)
            {
            SwitchWorkspace(buttons[position-1]);
            }
            else if(currentWorkspace == workspaces[0].gameObject)
            {
            SwitchWorkspace(buttons[position+1]);
            }
            else
            {
                SwitchWorkspace(buttons[position - 1]);
            }
            Destroy(workspaces[position].gameObject);
            Destroy(floorListList[position].gameObject);
            Destroy(buttons[position].gameObject);
            workspaces.RemoveAt(position);
            floorListList.RemoveAt(position);
            buttons.RemoveAt(position);
            workspaceCount--;
        }
    }


    public void AddNewWorkspaceToList(GameObject workspace)
    {
        workspaces.Add(workspace);
    }

     public void AddNewWorkspaceToButtonList(GameObject thebutton)
    {
        buttons.Add(thebutton);
    }
    public void AddNewFloorListToFloorListList(GameObject theFloor)
    {
        floorListList.Add(theFloor);
    }
    public void SwitchWorkspace(GameObject theButton)
    {
        WallManager.GetComponent<WallManager>().turnOffCreating();
        WallManager.GetComponent<WallManager>().turnOffDeleting();

        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        if(selected != null) {
            selected.GetComponent<FurnitureState>().destoryFurnitureUI();
            selected.GetComponent<FurnitureState>().isSelected = false;
            selected.tag = "Furniture";
            selected.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
        }
        
        
        if(currentWorkspace != null)
        {
        currentWorkspace.SetActive(false);
        }
        int position = buttons.IndexOf(theButton);
        GameObject switchToWorkspace = workspaces[position];
        GameObject switchToFloorList = floorListList[position];
        switchToWorkspace.SetActive(true);
        switchToFloorList.SetActive(true);

         foreach (GameObject theworkspace in workspaces)
        {
             if(theworkspace != switchToWorkspace)
             {
                theworkspace.SetActive(false);
             }   
        }
         foreach (GameObject thefloorlist in floorListList)
        {
             if(thefloorlist != switchToFloorList)
             {
                thefloorlist.SetActive(false);
             }   
        }
         foreach (GameObject button in buttons)
        {
             if(button != theButton)
             {
                button.GetComponent<Image>().color = Color.white;
             }
        }
        theButton.GetComponent<Image>().color = new Color(0.55f, 1.0f, 0.55f, 1.0f);
        currentWorkspace = switchToWorkspace;
        currentFloorList = switchToFloorList;
        GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().width = switchToWorkspace.GetComponent<workspaceInfo>().width;
        GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().height = switchToWorkspace.GetComponent<workspaceInfo>().height;
        GameObject.FindWithTag("GridManager").GetComponent<GridCreation>().resetGrid();
    }

    public void addWorkspaceClicked()
    {
        if(workspaceCount >= 8)
        {
            Debug.Log("Max workspaces reached");
            return;
        }
        ExampleUI.SetActive(false);
        WorkspaceCreateUI.SetActive(true);
        GameObject selected = GameObject.FindWithTag("SelectedFurniture");
        if(selected != null) {
            selected.GetComponent<FurnitureState>().destoryFurnitureUI();
            selected.GetComponent<FurnitureState>().isSelected = false;
            selected.tag = "Furniture";
            selected.GetComponent<SpriteRenderer>().color = new Color(0,0,0,1);
        }
        
        if(workspaceCount >= 1 && currentWorkspace != null)
        {
        currentWorkspace.SetActive(false);
        }
    }

    public void addFloorClicked()
    {
        currentWorkspace.GetComponent<workspaceInfo>().addFloor();
    }
}
