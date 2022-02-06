using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] // [System.Serializable] shows this Sound class settings on the inspector
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
