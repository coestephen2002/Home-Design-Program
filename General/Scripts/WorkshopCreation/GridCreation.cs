using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GridCreation : MonoBehaviour
{
   public float width, height;
   public Slider heightSlide;
   public Slider widthSlide;
    [SerializeField] Transform cam;
    [SerializeField] Camera camera;
    [SerializeField] Tile tilePrefab;
    public string name;
    [SerializeField]
    public GameObject workspace;
    private Vector3 startPos;
    private float startZoom;




   public void GenerateGrid(GameObject parent)
   {
        height = heightSlide.value;
        width = widthSlide.value;

        Debug.Log(height);
        Debug.Log(width);
        for(int x = 0; x < width; x++) { 
            for(int y = 0; y<height; y++) { 
                
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.transform.SetParent(parent.transform);
                spawnedTile.name = $"Tile ({x},{y})";

                var isOffset = (x + y) % 2 == 1;
                spawnedTile.init(isOffset);
        }
    }
    
        float maximum = Mathf.Max(width, height);
    
        camera.orthographicSize = maximum/1.5f + maximum/100;
    
        float cameraHeight = 2.0f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        cam.transform.position = new Vector3(width/2.0f + cameraWidth/8.0f , height/1.85f - 0.5f, -10);
        camera.backgroundColor = new Color(0.75f, 0.75f, 0.75f, 1.0f);

        startPos = camera.transform.position;
        startZoom = camera.orthographicSize;
     
        //gameObject.GetComponent<TileManager>().occupied = new int[(int) width,(int) height];
   }

   public void resetGrid() {
         float maximum = Mathf.Max(width, height);
    
        camera.orthographicSize = maximum/1.5f + maximum/100;
    
        float cameraHeight = 2.0f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        cam.transform.position = new Vector3(width/2.0f + cameraWidth/8.0f , height/1.85f - 0.5f, -10);
        camera.backgroundColor = new Color(0.75f, 0.75f, 0.75f, 1.0f);

        startPos = camera.transform.position;
        startZoom = camera.orthographicSize;
        camera.transform.position = startPos;
        camera.orthographicSize = startZoom;
   }
}
