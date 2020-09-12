using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    GameObject uiManager;
    private void Start() {
        uiManager = GameObject.FindWithTag("UI");
    }

    void OnMouseDown() {
        // Put the actions you want to perform to respond to the touch here.
        Debug.LogFormat("Object {0} got touched!", gameObject.name);
        var tool = uiManager.GetComponent<UIManager>().CurrentTool;
        switch (tool) {
            case UIManager.Tool.Pencil:
                gameObject.GetComponent<SpriteRenderer>().color = uiManager.GetComponent<UIManager>().CurrentColor;
                break;
            case UIManager.Tool.Picker:
                uiManager.GetComponent<UIManager>().CurrentColor = gameObject.GetComponent<SpriteRenderer>().color;
                uiManager.GetComponent<UIManager>().CurrentTool = UIManager.Tool.Pencil;
                break;
            case UIManager.Tool.Eraser:
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                break;
        }
    }
}
