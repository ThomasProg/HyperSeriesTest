using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditorPage : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField episodeName = null;
    [SerializeField]
    private TMP_InputField nbViewsText = null;

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, Screen.safeArea.height - 100);
    }

    public void LoadPage()
    {
        episodeName.text = PlayerPrefs.GetString("episodeName");
        nbViewsText.text = PlayerPrefs.GetInt("nbViews").ToString();
    }

    public void SavePage()
    {
        PlayerPrefs.SetString("episodeName", episodeName.text);

        // TODO : Display Error Message if not able to convert to int
        int nbViews;
        if (int.TryParse(nbViewsText.text, out nbViews))
        {
            PlayerPrefs.SetInt("nbViews", nbViews);
        }

    }
}
