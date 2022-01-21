using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[RequireComponent(typeof(ScrollRect))]
public class ScrollSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

    [SerializeField]
    private int startingElementID = 0;
    [SerializeField]
    private float decelerationRate = 10f;

    float elemHeight;
    private ScrollRect scrollRect;
    private RectTransform container;
    
    private int nbElements;
    private int currentElementID;
    public int CurrentElementID { get { return currentElementID; } }
    private Vector2 lerpTargetPosition;

    private Coroutine transitionCoroutine = null;

    private List<Vector2> elementPositions = new List<Vector2>();

    public delegate void SnapAction(int elemID);
    public event SnapAction snapEvent;

    void Start() 
    {
        scrollRect = GetComponent<ScrollRect>();
        container = scrollRect.content;
        nbElements = container.childCount;
        elemHeight = container.rect.height; 

        UpdateElements();
        SetElement(startingElementID);
	}

    IEnumerator RunTransitionCoroutine()
    {
        float treshold = 0.25f;
        while (Vector2.SqrMagnitude(container.anchoredPosition - lerpTargetPosition) > treshold)
        {

            float decelerate = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
            container.anchoredPosition = Vector2.Lerp(container.anchoredPosition, lerpTargetPosition, decelerate);

            yield return null;
        }

        container.anchoredPosition = lerpTargetPosition;

        SetActiveCurrentElemOnly();
    }

    // Update the size and the position of the elements
    public void UpdateElements() 
    {
        elementPositions.Clear();

        for (int i = 0; i < nbElements; i++) 
        {
            RectTransform child = container.GetChild(i).GetComponent<RectTransform>();
            child.sizeDelta = new Vector2(child.sizeDelta.x, elemHeight);
            Vector2 childPosition = new Vector2(0f, - i * elemHeight);
            child.anchoredPosition = childPosition;
            elementPositions.Add(-childPosition);
        }
    }

    public void SetActiveCurrentElemOnly()
    {
        for (int i = 0; i < nbElements; i++)
        {
            container.GetChild(i).gameObject.SetActive(i == currentElementID);
        }
    }

    public void SetActiveAllElems()
    {
        for (int i = 0; i < nbElements; i++)
        {
            container.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void SetElement(int elementID) 
    {
        elementID = Mathf.Clamp(elementID, 0, nbElements - 1);
        container.anchoredPosition = elementPositions[elementID];
        currentElementID = elementID;
        SetActiveCurrentElemOnly();
    }

    public void LerpToElement(int elementID) 
    {
        SetActiveAllElems();

        elementID = Mathf.Clamp(elementID, 0, nbElements - 1);
        lerpTargetPosition = elementPositions[elementID];
        transitionCoroutine = StartCoroutine(RunTransitionCoroutine());
        currentElementID = elementID;
        snapEvent?.Invoke(currentElementID);
    }

    private int GetNearestElement() 
    {
        Vector2 currentPosition = container.anchoredPosition;

        float distance = float.MaxValue;
        int nearestElement = currentElementID;

        for (int i = 0; i < elementPositions.Count; i++) 
        {
            float testDist = Vector2.SqrMagnitude(currentPosition - elementPositions[i]);
            if (testDist < distance) 
            {
                distance = testDist;
                nearestElement = i;
            }
        }

        return nearestElement;
    }

    public void OnBeginDrag(PointerEventData aEventData) 
    {
        for (int i = 0; i < nbElements; i++)
        {
            container.GetChild(i).gameObject.SetActive(true);
        }

        // Stops animation if dragging
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);
    }

    public void OnEndDrag(PointerEventData aEventData) 
    {
        LerpToElement(GetNearestElement());
    }
}
