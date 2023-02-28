using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Grid : MonoBehaviour
{
   public float Width, Height;
   public Slider heightSlide;
   public Slider widthSlide;
    [SerializeField] Transform cam;
    [SerializeField] Camera camera;
    [SerializeField] Tile tilePrefab;

    private Vector3 startPos;
    private float startZoom;

   public void RenderGrid()
   {
        Height = heightSlide.value;
        Width = widthSlide.value;
        // Debug.Log(height);
        // Debug.Log(width);
        for(int x = 0; x < Width; x++) { 
            for(int y = 0; y<Height; y++) { 
        
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x,y), Quaternion.identity);
                spawnedTile.name = $"Tile ({x},{y})";

                var isOffset = (x + y) % 2 == 1;
                spawnedTile.init(isOffset);
        }
    }
    
        float maximum = Mathf.Max(Width, Height);
    
        camera.orthographicSize = maximum/1.5f + maximum/100;
    
        float cameraHeight = 2.0f * camera.orthographicSize;
        float cameraWidth = cameraHeight * camera.aspect;
        cam.transform.position = new Vector3(Width/2.0f + cameraWidth/8.0f , Height/1.85f - 0.5f, -10);
        camera.backgroundColor = new Color(0.75f, 0.75f, 0.75f, 1.0f);

        startPos = camera.transform.position;
        startZoom = camera.orthographicSize;
     
        gameObject.GetComponent<TileManager>().occupied = new int[(int) Width,(int) Height];
   }

   public void resetGrid() {
        camera.transform.position = startPos;
        camera.orthographicSize = startZoom;
   }

}
