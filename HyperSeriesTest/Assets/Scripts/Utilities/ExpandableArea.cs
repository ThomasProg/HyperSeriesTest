using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class ExpandableArea : MonoBehaviour
{
    [SerializeField]
    private bool isExpanded = false;

    [SerializeField]
    private RectTransform baseArea = null;

    [SerializeField]
    private RectTransform expandedArea = null;

    [SerializeField]
    private float baseAreaHeight = 100f;

    [SerializeField]
    private float animDuration = 0.1f;

    [SerializeField]
    private int nbStepsPerSecond = 30;

    [SerializeField]
    private float expandedHeight = 100f;

    private Coroutine animCoroutine = null;

    [SerializeField]
    private Image arrowImage  = null;

    public bool IsExpanded
    {
        get { return isExpanded; }
        set 
        {
            if (isExpanded == value)
                return;

            if (animCoroutine != null)
                StopCoroutine(animCoroutine);

            isExpanded = value;
            if (isExpanded)
            {
                animCoroutine = StartCoroutine(OpenAnimation());
            }
            else
            {
                animCoroutine = StartCoroutine(CloseAnimation());
            }
        }
    }

    public void FlipFlop()
    {
        if (IsExpanded)
            IsExpanded = false;
        else
            IsExpanded = true;
    }

    private IEnumerator OpenAnimation()
    {
        float nbTotalSteps = nbStepsPerSecond * animDuration;
        for (int step = 1; step < nbTotalSteps; step++)
        {
            float ratio = step / nbTotalSteps;

            Vector2 baseAreaSize = baseArea.sizeDelta;
            baseAreaSize.y = baseAreaHeight + expandedHeight * ratio;
            baseArea.sizeDelta = baseAreaSize;

            Vector3 expandedAreaScale = expandedArea.localScale;
            expandedAreaScale.y = ratio;
            expandedArea.localScale = expandedAreaScale;

            arrowImage.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(0, - 90f, ratio), Vector3.forward);

            yield return new WaitForSeconds(1f / nbStepsPerSecond);
        }

        OpenFullyImmediatly();
    }

    private IEnumerator CloseAnimation()
    {
        float nbTotalSteps = nbStepsPerSecond * animDuration;
        for (int step = 1; step < nbTotalSteps; step++)
        {
            float ratio = (1f - step / nbTotalSteps);

            Vector2 baseAreaSize = baseArea.sizeDelta;
            baseAreaSize.y = baseAreaHeight + expandedHeight * ratio;
            baseArea.sizeDelta = baseAreaSize;

            Vector3 expandedAreaScale = expandedArea.localScale;
            expandedAreaScale.y = ratio;
            expandedArea.localScale = expandedAreaScale;

            arrowImage.transform.rotation = Quaternion.AngleAxis(Mathf.Lerp(0, - 90f, ratio), Vector3.forward);

            yield return new WaitForSeconds(1f / nbStepsPerSecond);
        }

        CloseFullyImmediatly();
    }

    public void OpenFullyImmediatly()
    {
        Vector2 baseAreaSize = baseArea.sizeDelta;
        baseAreaSize.y = baseAreaHeight + expandedHeight;
        baseArea.sizeDelta = baseAreaSize;

        Vector3 expandedAreaScale = expandedArea.localScale;
        expandedAreaScale.y = 1f;
        expandedArea.localScale = expandedAreaScale;

        arrowImage.transform.rotation = Quaternion.AngleAxis(-90f, Vector3.forward);
    }

    public void CloseFullyImmediatly()
    {
        Vector2 baseAreaSize = baseArea.sizeDelta;
        baseAreaSize.y = baseAreaHeight;
        baseArea.sizeDelta = baseAreaSize;

        Vector3 expandedAreaScale = expandedArea.localScale;
        expandedAreaScale.y = 0f;
        expandedArea.localScale = expandedAreaScale;

        arrowImage.transform.rotation = Quaternion.AngleAxis(-0f, Vector3.forward);
    }

    public void FlipFlopImmediatly()
    {
        if (isExpanded)
        {
            isExpanded = false;
            CloseFullyImmediatly();
        }
        else
        {
            isExpanded = true;
            OpenFullyImmediatly();
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ExpandableArea))]
public class ExpandableAreaEditor : Editor
{
    override public void OnInspectorGUI()
    {
        ExpandableArea expandableArea = (ExpandableArea)target;
        string buttonText = expandableArea.IsExpanded ? "Collapse" : "Expand";
        if (GUILayout.Button(buttonText))
        {
            expandableArea.FlipFlopImmediatly();
        }
        DrawDefaultInspector();
    }
}
#endif