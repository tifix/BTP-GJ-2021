using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler instance;
    public static AudioSource source;
    public static AudioListener listener;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        source = GetComponent<AudioSource>();
        listener = GetComponent<AudioListener>();
    }

    public static void PlayEffect(AudioClip audioClip)
    {
        source.PlayOneShot(audioClip);
    }

    public static void PlayRandomEffect(List<AudioClip> audioClips)
    {
        int selected = Random.Range(0, audioClips.Count);
        source.PlayOneShot(audioClips[selected]);
    }
    public static void PlayEffect(string audioClip)
    {
        source.PlayOneShot(Resources.Load("Audio/"+audioClip) as AudioClip);
    }

    public static void PlayRandomEffect(List<string> audioClips)
    {
        int selected = Random.Range(0, audioClips.Count);
        source.PlayOneShot(Resources.Load("Audio/" +audioClips[selected]) as AudioClip);
    }

}
