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

    void OnEnable()
    {
        LoadPage();
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
