using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() {

        // This is a singleton which will ensure that at any given point in time, we only have a single AudioManager instance
        // This will allow a single AudioManager to persist between scenes
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject); // this ensures that this AudioManager persists between scenes

        // We are adding a component for each sound that is specified in the AudioManager at the runtime.
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume; // copies the volume to the newly created component
            s.source.pitch = s.pitch; // copies the pitch to the newly created component
            s.source.loop = s.loop; // copies the loop to the newly created component
        }
    }

    void Start() {
        Play("SpaceTravel");
    }

    public void StopPlaying(string sound) {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
