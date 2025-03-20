using System;
using TMPro;
using UnityEngine;

public sealed class Infoboard : MonoBehaviour
{
    private static Infoboard s_instance;
    [SerializeField] private TextMeshProUGUI _infoMessage;

    #region Unity Methods
    private void Awake()
    {
        if (s_instance == null)
            s_instance = this;

        else Destroy(gameObject);
    }

    private void Start()
    {
        if (_infoMessage != null)
            _infoMessage.text = String.Empty;
    }

    private void OnEnable()
    {
        TrashBin.s_OnTrashedEvent2 += DisplayInfoMessage;
    }

    private void OnDisable()
    {
        TrashBin.s_OnTrashedEvent2 -= DisplayInfoMessage;
    }

    #endregion

    /// <summary>
    /// This method displays information regarding how well trash is sorted on the Infoboard. It takes 2 parameters
    /// of type float and SOTrashData. 
    /// </summary>
    /// <param name="points">Used for checking how well trash was sorted.</param>
    /// <param name="soTrashData">Used for accessing infomation about trash.</param>
    private static void DisplayInfoMessage(float points, SOTrashData soTrashData)
    {
        if (s_instance == null)
        {
            Debug.LogError("Error: An instance of Infoboard.cs does not currently exist.");
            return;
        }

        //if (points is null || soTrashData is null)
        //{
        //    Debug.LogError(
        //            $"Error: DisplayInfoMessage encountered a null parameter. points: {points}, soTrashData: {soTrashData}");
        //    return;
        //}

        s_instance._infoMessage.text = (points >= 0)
                ? $"{soTrashData.SO_Description}\n\n{soTrashData.InfoIfSortedCorrectly}"
                : $"{soTrashData.SO_Description}\n\n{soTrashData.InfoIfSortedWrongly}";
    }
}