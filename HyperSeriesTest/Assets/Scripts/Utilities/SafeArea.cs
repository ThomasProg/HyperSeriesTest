using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    private void Awake()
    {
        Rect safeAreaRect = Screen.safeArea;

        Vector2 anchorMin = safeAreaRect.position;
        Vector2 anchorMax = anchorMin + safeAreaRect.size;
        
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }
}
