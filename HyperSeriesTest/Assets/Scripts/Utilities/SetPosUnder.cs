using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetPosUnder : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectToMove;

    [SerializeField]
    private RectTransform to;

    private void Start()
    {
        UpdatePos();
    }

    public void UpdatePos()
    {
        rectToMove.anchoredPosition = new Vector2(0, - to.rect.size.y);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SetPosUnder))]
public class SetPosUnderEditor : Editor
{
    override public void OnInspectorGUI()
    {
        SetPosUnder setPosUnder = (SetPosUnder)target;
        if (GUILayout.Button("Update"))
        {
            setPosUnder.UpdatePos();
        }
        DrawDefaultInspector();
    }
}
#endif