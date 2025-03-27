using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TVManager : MonoBehaviour
{
    [SerializeField] private GameObject _tv;
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

    private bool IsTVOn() // Checker for when tv is on 
    {
        return _tv.activeInHierarchy;
    }

    #region Buttons
    public void DraggingSlider() // Called when dragging slider
    {
        if (!IsTVOn()) return;
        _isDraggingSlider = true;
    }

    public void DraggingSliderEnd() // Called when dragging slider
    {
        if (!IsTVOn()) return;
        _isDraggingSlider = false;
        _videoPlayer.Play();
        _videoPlayer.time = _progressBar.value;

    }

    public void RestartVideoButton() // Button call
    {
        if (!IsTVOn()) return;
        _videoPlayer.time = 0;
        _videoPlayer.Play();
    }

    public void PauseVideoButton() // Button call
    {
        if (!IsTVOn()) return;
        _videoPlayer.Pause();
    }

    public void StartVideoButton() // Button call
    {
        if (!IsTVOn()) return;
        if (_progressBar.value >= _progressBar.maxValue)
        {
            return;
        }
        _videoPlayer.Play();
    }

    private double _videoLength;
    public void TurnTVONOFFButton() // Button call
    {
        if (_tv.activeInHierarchy) // If the tv is currently on
        {
            _videoLength = _videoPlayer.time; // Store the video length before turning tv off
            //_videoPlayer.Pause(); // Stop the video
        }

        _progressBar.gameObject.SetActive(!_tv.activeInHierarchy); // Turn on or off slider
        _tv.SetActive(!_tv.activeInHierarchy); // Turn on or off tv
        
        if (_tv.activeInHierarchy) // If the has been turned on. This wont be called if the tv just got turned off
        {
            _videoPlayer.time = _videoLength; // Set the tv length to the time before the tv was turned off
            _videoPlayer.Pause(); // Stop the video. 
        }
    }

    public void NextVideo() // Button call
    {
        if (!IsTVOn()) return;
        ChangeVideo(1);
    }

    public void PreviousVideoButton() // Button call
    {
        if (!IsTVOn()) return;
        ChangeVideo(-1);
    }
    #endregion
}
