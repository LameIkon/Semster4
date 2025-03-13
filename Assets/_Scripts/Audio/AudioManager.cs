using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

public class AudioManager : Singleton<AudioManager>
{

	[SerializeField] private AudioMixer _audioMixer;
	[SerializeField] private AudioMixerGroup _audioMixerGroup;
	private ISet<GameObject> _trashBins;
	private ISet<GameObject> _trash;

	#region UnityMethods
	protected void Start() 
	{
		StartCoroutine(InitTrash());
	}

	#endregion

	private IEnumerator InitTrash() 
	{
		_audioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];
		yield return new WaitForFixedUpdate();

		_trashBins = new HashSet<GameObject>(GameObject.FindGameObjectsWithTag("TrashBin"));
		_trash = new HashSet<GameObject>(GameObject.FindGameObjectsWithTag("Trash"));

		SetupAudioSource(_trashBins);
		SetupAudioSource(_trash);

		yield return null;

		_trashBins.Clear();
		_trash.Clear();
	}


	private void SetupAudioSource(ISet<GameObject> values) 
	{
		AudioSource source;

		foreach (GameObject obj in values)
		{
			if (obj == null) continue;

			if (obj.TryGetComponent<AudioSource>(out source) && _audioMixerGroup != null)
			{
				source.outputAudioMixerGroup = _audioMixerGroup;
				source.spatialBlend = 1;
			}

		}
	}
}
