using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PageBanner : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI bannerTitle = null;

    [SerializeField]
    private ScrollSnap scrollSnap = null;

    private void Start()
    {
        scrollSnap.snapEvent += UpdateTitle;
        UpdateTitle(scrollSnap.CurrentElementID);
    }

    private void UpdateTitle(int elemID)
    {
        switch (elemID)
        {
            case 0:
                bannerTitle.text = "CLIENT";
                break;

            case 1:
                bannerTitle.text = "EDITOR";
                break;
        }
    }
}
