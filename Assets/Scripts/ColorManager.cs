using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public UIManager uiManager;
    private void OnMouseDown() {
        uiManager.GetColor(gameObject);
    }
}
