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
}
