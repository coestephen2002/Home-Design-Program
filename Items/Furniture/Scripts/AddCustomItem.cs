using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class AddCustomItem : MonoBehaviour
{
    
    [SerializeField] GameObject FurnitureBank;
    [SerializeField] GameObject WallBank;
    [SerializeField] TMP_InputField inputLabel;
    [SerializeField] TMP_InputField widthInputField;
    [SerializeField] TMP_InputField lengthInputField;

    public void Start()
    {
        widthInputField.onValidateInput += delegate(string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
        lengthInputField.onValidateInput += delegate(string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
    }

    private char MyValidate(char charToValidate)
    {
        if (!Char.IsDigit(charToValidate))
        {
            charToValidate = '\0';
        }
        return charToValidate;
    }

    public void addItem() {
        if(FurnitureBank.activeSelf){
            create(FurnitureBank);
        }else if (WallBank.activeSelf){
            create(WallBank);
        }
        clearFields();
    }

    private void create(GameObject bank) {
        String width = widthInputField.text;
        String length = lengthInputField.text;

        Transform container = RecursiveFindChild (bank.transform, "Custom Panel");

        GameObject copy = container.Find("PlaceHolder").gameObject;
        GameObject newObject = Instantiate(copy, container);

        newObject.transform.Find("Label").gameObject.GetComponentInChildren<TMP_Text>().SetText(inputLabel.text);
        string dimensions = $"({width}x{length})";
        newObject.transform.Find("Dimensions").gameObject.GetComponentInChildren<TMP_Text>().SetText(dimensions);
        newObject.SetActive(true);
       
    }

    private void clearFields() {
        inputLabel.text = null;
        widthInputField.text = null;
        lengthInputField.text = null;
    }

    private Transform RecursiveFindChild(Transform parent, string childName) {

        foreach (Transform child in parent) {
            if(child.name == childName) {
                return child;
            }
            else {
                Transform found = RecursiveFindChild(child, childName);
                if (found != null) {
                        return found;
                }
            }
        }
        
        return null;
    }

}

