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
