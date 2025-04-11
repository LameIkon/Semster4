using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Animator), typeof(AudioSource)),
 RequireComponent(typeof(Rigidbody))]
public class TrashBin : MonoBehaviour, ILoggable
{
    /*
     * The implementation of the TrashBin handles the sending of data to the TrashManager,
     * It is also responsible for calling the Trashing method on the ITrashables
     */

    // References
    [SerializeField]
    private SOTrashBinData _binData; // The Trash type comes from the SO 

    // Events
    public static event Action<GameObject, float> s_OnTrashedEvent; // The event that connects to the TrashManager
    public static event Action<float, SOTrashData> s_OnTrashedEvent2;
    public static event Action<string, string> s_OnLogEvent;
    public static event Action s_OnTrashedEvent3; // Used for tutorial
    public static event Action s_OnTrashedEvent4; // Used for tutorial

    // Audio
    private AudioSource _audioSource;

    // Animations
    private Animator _animator;
    private readonly int _expandCorrectAnimation = Animator.StringToHash("ExpandCorrect");
    private readonly int _expandIncorrectAnimation = Animator.StringToHash("ExpandIncorrect");

    [SerializeField] private float _vomitForce;
    private Transform _mouthVomitPoint;

    [SerializeField] private bool _jonasBool = true;
    [SerializeField] private bool _johanBool;

    private void OnTriggerEnter(Collider target)
    {
        if (_binData == null)
        {
            Debug.LogError($"Data for the TrashBin has not been assigned");
            return;
        }

        target.TryGetComponent<ITrashable>(out ITrashable trash); // Check if the GameObject entering the Trigger has an ITrashable
        float? points = trash?.Trashing(_binData._AllowedType);    // and calling the Trashing method, the method returns a float

        if (points != null)
        {
            s_OnTrashedEvent?.Invoke(gameObject, (float)points); // Casts the points as a float and invokes the OnTrashedEvent
            s_OnTrashedEvent2?.Invoke((float)points, trash.TrashData());
            s_OnTrashedEvent3?.Invoke(); // This should be changed. It needs to know if the trashcan accepted or declined the trash
            Invoke(nameof(CheckFortrash),0.1f); // We need a small delay to check for trash after it gets deleted. This should be changed. It needs to know if the trashcan accepted or declined the trash
            if (s_OnLogEvent != null)
            {
                s_OnLogEvent.Invoke(target.gameObject.name, gameObject.name);
            }

            EnablePolish(points);
        }

        AudioClip clip = trash?.TrashingSound();
        if (clip != null)
        {
            PlayTrashSound(clip);
        }

        

        if (trash.Vomit(_binData._AllowedType) && trash != null)
        {
            Rigidbody rb = target.GetComponent<Rigidbody>();
            if (_jonasBool)
            {
                Vector3 vomitDir = Quaternion.AngleAxis(30, _mouthVomitPoint.transform.right) * _mouthVomitPoint.transform.forward;
                rb.AddForce(vomitDir * _vomitForce, ForceMode.Impulse);
                Debug.DrawRay(_mouthVomitPoint.position, _mouthVomitPoint.forward * 2, Color.green);
                Debug.DrawRay(_mouthVomitPoint.position, vomitDir * 2, Color.red);
            }
            else if (_johanBool)
            {
                Vector3 startPos = target.transform.position;
                Vector3 targetPos = _mouthVomitPoint.position;
                Vector3 vomitDir = (targetPos - startPos).normalized;
                rb.AddForce(vomitDir * _vomitForce, ForceMode.Impulse);
            }
        }
    }


    private void CheckFortrash()
    {
        s_OnTrashedEvent4?.Invoke();
    }

    private void PlayTrashSound(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    private void EnablePolish(float? points) // Checks if the value is positive or negative
    {
        if (_animator == null)
        {
            Debug.LogError($"Animator has not been assigned for this object: {gameObject}");
            return;
        }

        _animator.Play(points > 0 ? _expandCorrectAnimation : _expandIncorrectAnimation); // Play corresponding animation
    }

    public void Log() { }


#region UnityMethods
    private void Awake()
    {
        gameObject.tag = "TrashBin";
    }

    private void Start()
    {
        Reset();
    }

    // Makes sure that the BoxCollider is a set to a Trigger
    public void Reset()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
        _audioSource.spatialBlend = 1f;
        GetComponent<BoxCollider>().isTrigger = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.detectCollisions = true;
        _mouthVomitPoint = gameObject.GetComponentInChildren<Transform>();
    }
#endregion
}