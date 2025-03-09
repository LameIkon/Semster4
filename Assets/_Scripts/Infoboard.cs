using System;
using TMPro;
using UnityEngine;

namespace _Scripts
{
    public class Infoboard : MonoBehaviour
    {
        private static           Infoboard       s_instance;
        [SerializeField] private TextMeshProUGUI _infoMessage;

        private void Awake()
        {
            if (s_instance is null)
                s_instance = this;
            
            else Destroy(gameObject);
        }

        private void Start()
        {
            if (_infoMessage is not null)
                _infoMessage.text = String.Empty;
        }

        public static void UpdateInfoMessage()
        {
            if (s_instance is null)
                Debug.LogError("Error: An instance of Infoboard.cs does not currently exist");
            
            
        }
    }
}