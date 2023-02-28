using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class AssignLabelAndSize : MonoBehaviour
{
    [SerializeField] public string label;
    [SerializeField] public int width;
    [SerializeField] public int length;
    void Start()
    {
        GameObject labelText = gameObject.transform.GetChild(0).gameObject;
        GameObject dimensionsText = gameObject.transform.GetChild(1).gameObject;
        label = labelText.GetComponent<TMP_Text>().text;
        string dimensions = dimensionsText.GetComponent<TMP_Text>().text;
        dimensions = dimensions.Substring(1, dimensions.Length - 2);
        string[] nums = dimensions.Split('x');
        width = int.Parse(nums[0]);
        length = int.Parse(nums[1]);

    }

    public void FindLabelAndSize()
    {
         GameObject labelText = gameObject.transform.GetChild(0).gameObject;
        GameObject dimensionsText = gameObject.transform.GetChild(1).gameObject;
        label = labelText.GetComponent<TMP_Text>().text;
        string dimensions = dimensionsText.GetComponent<TMP_Text>().text;
        dimensions = dimensions.Substring(1, dimensions.Length - 2);
        string[] nums = dimensions.Split('x');
        width = Convert.ToInt32(nums[0]);
        length = Convert.ToInt32(nums[1]);

    }
    // Update is called once per frame
    void Update()
    {
    }
}
