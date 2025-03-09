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
        
        public static void DisplayInfoMessage(float? points, String trashName, String trashBin)
        {
            if (s_instance is null)
                Debug.LogError("Error: An instance of Infoboard.cs does not currently exist.");

            if (points is null || String.IsNullOrEmpty(trashName) || String.IsNullOrEmpty(trashBin))
                Debug.LogError("Error: DisplayInfoMessage had an unexpected error.");
        
            if (points >= 0)
            {
                s_instance._infoMessage.text = "";
                // $"You may not discord {trashName} in {trashBin1}"
                return;
            }

            // Temporary, will be deleted once the TrashManager.cs is fully implemented 
            {
                trashName = "NOT DEFINED";
                trashBin  = "NOT DEFINED";
            }

            s_instance._infoMessage.text = $"You may not discard {trashName} in {trashBin}";
        }
    }
}