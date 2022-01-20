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

    private void Awake()
    {
        video.frameReady += OnVideoStartAtNewTime;
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
        video.sendFrameReadyEvents = true;
        isMovingSlider = false;

        if (wasPlayingBeforeMovingSlider)
            VideoPlay();
    }

    // When the user was moving the video slider at a time where frames weren't loaded yet,
    // the slider would go back to the previous time for some frames
    // This callback fixes this issue.
    void OnVideoStartAtNewTime(VideoPlayer source, long frameIdx)
    {
        video.sendFrameReadyEvents = false;
        //if (wasPlayingBeforeMovingSlider)
        //    VideoPlay();
    }

    public void FlipFlopFullScreen()
    {
        if (isFullScreen)
        {
            // Not really opti, but if the video is set non active, then it reloads the frames at the beginning
            portraitGOToEnable.SetActive(true);
            Screen.orientation = ScreenOrientation.AutoRotation;
            transform.SetParent(portraitNewLayout.transform);
            landscapeGOToEnable.SetActive(false);
        }
        else
        {
            // Not really opti, but if the video is set non active, then it reloads the frames at the beginning
            landscapeGOToEnable.SetActive(true);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            transform.SetParent(landscapeNewLayout.transform);
            portraitGOToEnable.SetActive(false);
        }

        isFullScreen = !isFullScreen;
    }
}
