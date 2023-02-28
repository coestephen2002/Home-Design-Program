using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class toggleSizeChoice : MonoBehaviour
{
    [SerializeField]
    GameObject gameObject;
    [SerializeField]
    TMP_Text statusText;
    [SerializeField]
    bool enabled;
    GameObject[] buttonsBelow;
    GameObject wallBank;
    GameObject furnitureBank;
    [SerializeField]
    GameObject content;
    float childColumns;
   IEnumerator Start()
   {
    bool enabled = true;
    setState();
    yield return new WaitForSeconds(0.5f);
    GameObject.FindGameObjectWithTag("Categories").GetComponent<VerticalLayoutGroup>().enabled = false;
   }
    // Start is called before the first frame update
   public void setState()
   {
    enabled = !enabled;
    gameObject.SetActive(!gameObject.activeInHierarchy);
    if(enabled == true)
    {
        buttonsBelow = GameObject.FindGameObjectsWithTag("ItemCategory");
        foreach (GameObject button in buttonsBelow)
        {
            if(gameObject.transform.position.y > button.transform.position.y)
            {
                 childColumns = (Mathf.Ceil((gameObject.transform.childCount/3.0f)));
                 RectTransform transform = button.GetComponent<RectTransform>();
                 transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + Mathf.Ceil((gameObject.transform.childCount/2.0f))* 120);

            }
            
        }
       //LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
        statusText.text = "+";
    }
    if(enabled == false)
    {
        buttonsBelow = GameObject.FindGameObjectsWithTag("ItemCategory");
        foreach (GameObject button in buttonsBelow)
        {
            if(gameObject.transform.position.y > button.transform.position.y)
            {
                 childColumns = (Mathf.Ceil((gameObject.transform.childCount/3.0f)));
                 RectTransform transform = button.GetComponent<RectTransform>();
                 transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - Mathf.Ceil((gameObject.transform.childCount/2.0f)) * 120);
                
            }
             
        }
           //LayoutRebuilder.ForceRebuildLayoutImmediate(content.GetComponent<RectTransform>());
         statusText.text = "-";
    }
   }

   public void resetOnLeave()
   {
         buttonsBelow = GameObject.FindGameObjectsWithTag("ItemCategory");
         foreach (GameObject x in buttonsBelow)
         {
            Debug.Log(x.GetComponent<toggleSizeChoice>().enabled);
            Debug.Log("Done");
            if(x.GetComponent<toggleSizeChoice>().enabled == false)
            {
                x.GetComponent<toggleSizeChoice>().setState();
            }
         }
   }

   public void loadWallBank()
   {
     wallBank = GameObject.FindGameObjectWithTag("wallBank");
     wallBank.SetActive(true);
     wallBank.SetActive(false);
     wallBank.SetActive(true);
   }
}
