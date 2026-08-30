using System.Collections.Generic;
using UnityEngine;

public class SoundControllerRapid : SoundController
{

    [SerializeField]
    private List<AudioSource> audioSources;

    protected override void Enabling()
    {
    }

    protected override void Updating()
    {
    }

    public override void PlayRandom()
    {
        for(int i = 0; i< audioSources.Count; i++)
        {
            if (audioSources[i].isPlaying) continue;
            audioSources[i].Play();
            break;
        }
    }
}