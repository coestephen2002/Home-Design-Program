using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridCreation : MonoBehaviour
{
   float width, height;
   public Slider heightSlide;
   public Slider widthSlide;
    [SerializeField] Transform cam;
    [SerializeField] Camera camera;
    [SerializeField] Tile tilePrefab;

    private Vector3 startPos;
    private float startZoom;

   public void GenerateGrid()
   {
        height = heightSlide.value;
        width = widthSlide.value;
        Debug.Log(height);
        Debug.Log(width);
        for(int x = 0; x < width; x++) { 
            for(int y = 0; y<height; y++) { 
        
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile ({x},{y})";

                var isOffset = (x + y) % 2 == 1;
                spawnedTile.init(isOffset);
        }
    }
    
        float maximum = Mathf.Max(width, height);
    
        camera.orthographicSize = maximum/1.5f + maximum/100;
    
        float cameraHeight = 2.0f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        cam.transform.position = new Vector3(width/2.0f + cameraWidth/5.2f , height/2.0f - 0.5f, -10);
        camera.backgroundColor = new Color(0.75f, 0.75f, 0.75f, 1.0f);

        startPos = camera.transform.position;
        startZoom = camera.orthographicSize;
     

   }

   public void resetGrid() {
        camera.transform.position = startPos;
        camera.orthographicSize = startZoom;
   }
}
