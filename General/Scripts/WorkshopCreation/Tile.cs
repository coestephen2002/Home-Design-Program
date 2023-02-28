using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
   [SerializeField] public Color normalColor;
   [SerializeField] public Color offsetColor;
   [SerializeField] public Color tileColor;
   [SerializeField] public SpriteRenderer renderer;
   [SerializeField] public bool isOccupied = false;
   public void init(bool isOffset)
   {
    renderer.color = isOffset ? offsetColor : normalColor;
    tileColor = renderer.color;
   }
}
