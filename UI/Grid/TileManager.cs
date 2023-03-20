using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class TileManager : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject[] furnitureObjects;
    public GameObject[] furnitureSelected;
    public int[,] occupied;
    public bool movedItem = false;

    public void createOccupied(float width, float height) {

        occupied = new int[(int) width,(int) height];
    }

    public void furniturePlaced(int[] oldEdges, int[] newEdges) 
    {
        //edges[0] = leftEdge
        //edges[1] = rightEdge
        //edges[2] = bottomEdge
        //edges[3] = topEdge

        for(int i = oldEdges[0]; i < oldEdges[1]; i++) {
            for(int j = oldEdges[2]; j < oldEdges[3]; j++) {
                occupied[i,j] = 0;
            }
        }

        if(!(newEdges[0] < 0 || newEdges[1] > occupied.GetLength(0) || newEdges[2] < 0 || newEdges[3] > occupied.GetLength(1))) {

            for(int i = newEdges[0]; i < newEdges[1]; i++) {
                for(int j = newEdges[2]; j < newEdges[3]; j++) {
                    occupied[i,j] = 1;
                }
            }
        }

        
        // Debug.Log("-------------------------------------");
        // string res = "\n";
        // for(int i = occupied.GetLength(1) - 1; i >= 0; i--) {
        //     for(int j = 0; j < occupied.GetLength(0); j++) {
        //         res = res + occupied[j,i];
        //     }
        //     Debug.Log(res);
        //     res = "\n";
        // } 
    }
}
