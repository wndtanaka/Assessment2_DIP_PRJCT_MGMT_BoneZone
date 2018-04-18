using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is for sound input in the future

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    AudioClip[] clips;
    [SerializeField]
    float delayBetweenClips;

    bool canPlay = true;
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (!canPlay)
        {
            return;
        }
        Debug.Log("Footsteps");
        GameManager.Instance.Timer.Add(() => { canPlay = true; }, delayBetweenClips);

        canPlay = false;

        int clipIndex = Random.Range(0, clips.Length);
        AudioClip clip = clips[clipIndex];
        source.PlayOneShot(clip);
    }
}
