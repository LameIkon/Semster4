using UnityEngine;

public class TrashItem : MonoBehaviour
{
    [SerializeField] private TrashData trashData;

    void Start()
    {
        Debug.Log(trashData.ToString());
    }
}