using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVManager : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private VideoClip[] clips; // what clips to be shown

    private int _videoindexNumber;
    private bool _isDraggingSlider;

    private void OnEnable()
    {
        _videoPlayer.loopPointReached += SomethingWillhappen;
    }

    private void OnDisable()
    {
        _videoPlayer.loopPointReached -= SomethingWillhappen;
    }


    void Start()
    {
        VideoToPlay(); // Called to set video max length in beginning
    }

    void Update()
    {
        VideoLength();
    }

    private void VideoLength()
    {
        if (_videoPlayer.clip != null && _videoPlayer.isPlaying && !_isDraggingSlider)
        {
            float currentTime = (float)_videoPlayer.time; // Check how much time of the video has passed since start
            _progressBar.value = currentTime; // Apply time to progress bar
        }
    }
    
    private void ChangeVideo(int number)
    {
        _videoindexNumber = (_videoindexNumber + number + clips.Length) % clips.Length; // used that way to ensure it wont be negative numbers. (example: 0-1+4 % 4 = 0)
        VideoToPlay();
    }

    private void VideoToPlay()
    {
        _videoPlayer.clip = clips[_videoindexNumber];
        _progressBar.maxValue = (float)_videoPlayer.clip.length;
        _videoPlayer.Play();
    }

    private void SomethingWillhappen(VideoPlayer vp)
    {
        Debug.Log("Video finished");
        // Use this if we want something to happen when the video is done
    }

    #region Buttons
    public void DraggingSlider() // Called when dragging slider
    {
        _isDraggingSlider = true;
    }

    public void DraggingSliderEnd() // Called when dragging slider
    {
        _isDraggingSlider = false;
        _videoPlayer.Play();
        _videoPlayer.time = _progressBar.value;

    }

    public void RestartVideoButton() // Button call
    {
        _videoPlayer.time = 0;
        _videoPlayer.Play();
    }

    public void PauseVideoButton() // Button call
    {
        _videoPlayer.Pause();
    }

    public void StartVideoButton() // Button call
    {
        if (_progressBar.value >= _progressBar.maxValue)
        {
            return;
        }
        _videoPlayer.Play();
    }

    public void NextVideo() // Button call
    {
        ChangeVideo(1);
    }

    public void PreviousVideoButton() // Button call
    {
        ChangeVideo(-1);
    }
    #endregion
}
