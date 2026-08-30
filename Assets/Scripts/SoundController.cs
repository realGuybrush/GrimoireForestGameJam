using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private bool randomOrSequence, byCall;

    [SerializeField]
    private List<AudioClip> sounds;

    [SerializeField]
    private AudioSource audioSource;

    private int index;

    private void OnEnable()
    {
        Enabling();
    }

    private void Update()
    {
        Updating();
    }

    protected virtual void Enabling()
    {
        if (randomOrSequence && !byCall && sounds.Count > 0)
        {
            audioSource.clip = sounds[Random.Range(0, sounds.Count)];
            audioSource.Play();
        }
    }

    protected virtual void Updating()
    {
        if (!randomOrSequence && !audioSource.isPlaying && sounds.Count > 0)
        {
            if (index >= sounds.Count)
                index = 0;
            else
            {
                audioSource.clip = sounds[index];
                audioSource.Play();
                index++;
            }
        }
    }

    public virtual void PlayRandom()
    {
        if (sounds.Count <= 0) return;
        audioSource.clip = sounds[Random.Range(0, sounds.Count)];
        audioSource.Play();
    }
}
