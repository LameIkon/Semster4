using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerToComplete : MonoBehaviour
{
    [SerializeField] private float _timeLimit;
    private float _timeRemaining;
    private Coroutine _countDown;
    private bool _gameIsOver = false;
    [SerializeField] private TextMeshProUGUI _timerText;

    [SerializeField] private bool _testStart = false;

    private void Awake()
    {
        _timeRemaining = _timeLimit;
        UpdateTimer(_timeRemaining);
    }

    void FixedUpdate()
    {
        if (_testStart)
        {
            StartCountDown();
        }
    }


    IEnumerator CountDown()
    {
        while (_timeRemaining >= 0)
        {
            yield return new WaitForSeconds(1f);
            _timeRemaining--;
            UpdateTimer(_timeRemaining);
        }

        GameOver();
    }

    private void GameOver()
    {
        Debug.Log("Time has run out, game over");
        _gameIsOver = true;
    }

    private void GameIsComplete()
    {
        StopCountDown();

        if (!_gameIsOver)
        {
            // Implement logic for when completed before timer ran out.
        }
    }

    public void StartCountDown()
    {
        if (_countDown == null)
        {
            _countDown = StartCoroutine(CountDown());
        }
    }

    private void StopCountDown()
    {
        if (_countDown != null)
        {
            StopCoroutine(_countDown);
            _countDown = null;
        }
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void OnEnable()
    {
        CompletionTrackerBoard.s_OnComplete += GameIsComplete;
    }

    public void OnDisable()
    {
        CompletionTrackerBoard.s_OnComplete -= GameIsComplete;
    }
}