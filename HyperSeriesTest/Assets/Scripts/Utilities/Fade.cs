using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup = null;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private Coroutine fadeOutCoroutine = null;
    [SerializeField]
    private float fadeOutToWaitSeconds = 1.5f;
    [SerializeField]
    private float fadeOutDuration = 0.5f;
    [SerializeField]
    private int fadeOutNbSteps = 30;

    private bool IsShowing { get { return canvasGroup.alpha > 0.5f; } }


    public void Show()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.gameObject.SetActive(true);
        if (fadeOutCoroutine != null)
            StopCoroutine(fadeOutCoroutine);
    }

    public IEnumerator FadeOutAnim(float waitDuration)
    {
        yield return new WaitForSeconds(waitDuration);

        float delay = 1f / (fadeOutNbSteps * fadeOutDuration);
        for (float currentAlpha = 1; currentAlpha > 0; currentAlpha -= delay)
        {
            canvasGroup.alpha = currentAlpha;

            yield return new WaitForSeconds(delay);
        }

        Hide();
    }

    public void FlipFlopVisibility()
    {
        if (IsShowing)
        {
            FadeOut(0f);
        }
        else
        {
            FadeOut(fadeOutToWaitSeconds);
        }
    }

    public void FadeOut(float waitDuration)
    {
        Show();
        fadeOutCoroutine = StartCoroutine(FadeOutAnim(waitDuration));
    }

    public void FadeOut()
    {
        FadeOut(fadeOutToWaitSeconds);
    }

    public void Hide()
    {
        canvasGroup.gameObject.SetActive(false);
        canvasGroup.alpha = 0f;
    }
}
