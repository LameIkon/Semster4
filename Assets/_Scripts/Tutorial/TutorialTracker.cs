using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTracker : MonoBehaviour
{
    [SerializeField] private SOTutorialData[] SO_tutoialData;
    [SerializeField] private GameObject[] _highlightDots;
    [SerializeField] private TextMeshProUGUI _text;
    private int _index;

    private void Start()
    {
        _text.text = SO_tutoialData[_index].SO_Description;
    }


    public void OnContinueButtonPressed() // Button
    {
        if (_index < _highlightDots.Length) 
        {
            _highlightDots[_index].SetActive(false);
        }

        _index++;

        if (_index < SO_tutoialData.Length) 
        {
            _text.text = SO_tutoialData[_index].SO_Description;
            _highlightDots[_index].SetActive(true);
        }
        else
        {
            Debug.Log("something will happen else");
        }
    }
  
}
