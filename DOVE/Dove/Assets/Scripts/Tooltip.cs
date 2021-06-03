using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    private static Tooltip current;
    public Text contentField;

    public RectTransform rectTransform;


    public void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        current = this;
        Hide();
    }

    private void Update() {
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }

    public void SetText(string content)
    {
        contentField.text = content;
    }

    public static void Show(string content) 
    {
        Show(content);
        current.gameObject.SetActive(true);
    }
    
    public static void Hide() 
    {
        current.gameObject.SetActive(true);
    }

}
