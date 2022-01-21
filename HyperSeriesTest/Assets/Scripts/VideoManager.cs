using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer video = null;

    [SerializeField]
    private TMPro.TextMeshProUGUI time = null;

    [SerializeField]
    private Slider progressBar = null;

    private bool isMovingSlider = false;
    private bool wasPlayingBeforeMovingSlider = false;
    private bool isFullScreen = false;
    private bool hasFlipFloppedFullscreenRecently = false;

    [SerializeField]
    private GameObject portraitGOToEnable = null;
    [SerializeField]
    private GameObject landscapeGOToEnable = null;
    [SerializeField]
    private GameObject portraitNewLayout = null;
    [SerializeField]
    private GameObject landscapeNewLayout = null;

    [SerializeField]
    private Fade fade = null;

    [SerializeField]
    private Image playImage = null;
    [SerializeField]
    private Sprite playSprite = null;
    [SerializeField]
    private Sprite pauseSprite = null;

    public float VideoLength
    { 
        get
        {
            return video.frameCount / video.frameRate;
        }
    }

    public void VideoPause()
    {
        video.Pause();
        fade.Show();

        playImage.sprite = playSprite;
    }

    public void VideoPlay()
    {
        video.Play();
        fade.FadeOut();

        playImage.sprite = pauseSprite;
    }

    public void FlipFlopVideo()
    {
        if (video.isPlaying)
        {
            VideoPause();
        }
        else
        {
            VideoPlay();
        }
    }

    private void Update()
    {
        if (video.isPlaying)
        {
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(video.time);
            time.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);

            progressBar.value = (float)video.time / VideoLength;
        }

        if (isMovingSlider)
        {
            video.time = progressBar.value * VideoLength;
        }

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

    public void SetTime(float newTime)
    {
        video.time = newTime;
    }

    public void StartMovingSlider()
    {
        wasPlayingBeforeMovingSlider = video.isPlaying;
        VideoPause();
        isMovingSlider = true;
    }

    public void EndMovingSlider()
    {
        isMovingSlider = false;

        if (wasPlayingBeforeMovingSlider)
            VideoPlay();
    }

    public void SetFullScreen()
    {
        // Not really opti, but if the video is set non active, then it reloads the frames at the beginning
        landscapeGOToEnable.SetActive(true);
        transform.SetParent(landscapeNewLayout.transform);
        portraitGOToEnable.SetActive(false);
        isFullScreen = true;
    }

    public void SetPortrait()
    {
        // Not really opti, but if the video is set non active, then it reloads the frames at the beginning
        portraitGOToEnable.SetActive(true);
        transform.SetParent(portraitNewLayout.transform);
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
