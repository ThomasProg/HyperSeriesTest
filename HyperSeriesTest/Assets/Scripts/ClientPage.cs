using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClientPage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI episodeName = null;
    [SerializeField]
    private TextMeshProUGUI nbViewsText = null;

    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, Screen.safeArea.height - 100);
    }

    void OnEnable()
    {
        LoadPage();
    }

    public void LoadPage()
    {
        episodeName.text = PlayerPrefs.GetString("episodeName");
        nbViewsText.text = PlayerPrefs.GetInt("nbViews").ToString();
    }

}
