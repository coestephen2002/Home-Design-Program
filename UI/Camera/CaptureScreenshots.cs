using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CaptureScreenshots : MonoBehaviour
{
    public float delayBetweenScreenshots = 0.3f;
    public Button captureButton;
    public Transform cam;

    private void Start()
    {
        captureButton.onClick.AddListener(TakeScreenshots);
    }

    private void TakeScreenshots()
    {
        StartCoroutine(CaptureScreenshotsCoroutine());
    }

    private IEnumerator CaptureScreenshotsCoroutine()
    {
        workspaceInfo floorScript = FindObjectOfType<workspaceInfo>();
        List<GameObject> floors = floorScript.floors;
        
        string workspaceName = floors[0].transform.parent.gameObject.name;

        ResetGrid(true);

        // Loop through each floor in the workspace and capture a screenshot of each one
        foreach (GameObject floor in floors)
        {
            yield return new WaitForEndOfFrame();
            floorScript.currentFloor.SetActive(false);
            floor.SetActive(true);

            yield return new WaitForSeconds(delayBetweenScreenshots); 
            string fileName = $"{workspaceName}_{floor.name}.png";
            ScreenCapture.CaptureScreenshot(fileName);

            yield return new WaitForSeconds(delayBetweenScreenshots);
            floor.SetActive(false);
        }

        floorScript.currentFloor.SetActive(true);

        ResetGrid(false);
    }

    // Sets the grid up to take a screenshot
    private void ResetGrid(bool setup){
        GridCreation gridScript = FindObjectOfType<GridCreation>();
        gridScript.resetGrid();

        DeactivateUI(setup);

        if(setup){
            // Center camera on floors
            Vector3 gridCenter = new Vector3(gridScript.width / 2.0f, gridScript.height / 2.0f, -10.0f);
            cam.position = gridCenter;
        }
    }

    private void DeactivateUI(bool condition){
        GameObject mask = captureButton.transform.parent.Find("Mask").gameObject;
        mask.SetActive(condition);

        if(condition){
            Camera.main.cullingMask= ~(1<<LayerMask.NameToLayer("UI"));
        } else {
            Camera.main.cullingMask |=(1<<LayerMask.NameToLayer("UI"));
        }
    }
}
