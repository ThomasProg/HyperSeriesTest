using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// Slider needs a child to have a RaycastTarget.
// However, if it is an image, it has to draw it, even if its alpha is 0.
// This class has a RaycastTarget, but doesn't draw anything, resolving this performance issue.
// Ref : https://answers.unity.com/questions/1091618/ui-panel-without-image-component-as-raycast-target.html
public class RaycastTargetArea : Graphic
{
    public override void SetMaterialDirty() { return; }
    public override void SetVerticesDirty() { return; }

    /// Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        return;
    }
}

#if UNITY_EDITOR
// Without this class, the “Color” and “Material” fields would show in the Inspector.
// However, they are not used, so it's better not to show them.
[CanEditMultipleObjects, CustomEditor(typeof(RaycastTargetArea), false)]
public class RaycastTargetAreaEditor : UnityEditor.UI.GraphicEditor
{
    public override void OnInspectorGUI()
    {
        base.serializedObject.Update();
        EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
        // skipping AppearanceControlsGUI
        base.RaycastControlsGUI();
        base.serializedObject.ApplyModifiedProperties();
    }
}
#endif