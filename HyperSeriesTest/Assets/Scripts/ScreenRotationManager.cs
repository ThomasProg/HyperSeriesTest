using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenRotationManager : MonoBehaviour
{
    [SerializeField]
    private GameObject portraitGOToEnable = null;
    [SerializeField]
    private GameObject landscapeGOToEnable = null;
    [SerializeField]
    private GameObject portraitNewVideoLayout = null;
    [SerializeField]
    private GameObject landscapeNewVideoLayout = null;
    [SerializeField]
    private VideoManager video = null;

    private bool isFullScreen = false;
    private bool hasFlipFloppedFullscreenRecently = false;

    private void Update()
    {
        if ((isFullScreen && (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight))
            || (!isFullScreen && (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)))
        {
            hasFlipFloppedFullscreenRecently = false;
        }

        if (!hasFlipFloppedFullscreenRecently && isFullScreen
            && (Input.deviceOrientation == DeviceOrientation.Portrait || Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown))
        {
            Screen.orientation = ScreenOrientation.AutoRotation;
            SetPortrait();
        }
        else if (!hasFlipFloppedFullscreenRecently && !isFullScreen
            && (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight))
        {
            SetFullScreen();
            Screen.orientation = ScreenOrientation.AutoRotation;
        }
    }


    public void SetFullScreen()
    {
        // Not really opti, but if the video is set non active, then it reloads the frames at the beginning
        landscapeGOToEnable.SetActive(true);
        video.transform.SetParent(landscapeNewVideoLayout.transform);
        portraitGOToEnable.SetActive(false);
        isFullScreen = true;
    }

    public void SetPortrait()
    {
        // Not really opti, but if the video is set non active, then it reloads the frames at the beginning
        portraitGOToEnable.SetActive(true);
        video.transform.SetParent(portraitNewVideoLayout.transform);
        landscapeGOToEnable.SetActive(false);
        isFullScreen = false;
    }

    public void FlipFlopFullScreen()
    {
        if (isFullScreen)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            SetPortrait();
        }
        else
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            SetFullScreen();
        }

        hasFlipFloppedFullscreenRecently = true;
    }
}
