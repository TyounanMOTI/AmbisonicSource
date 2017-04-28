using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbisonicSpeaker : MonoBehaviour {
    static readonly Vector3[] positions = {
        new Vector3(0, 0, 5),   // front
        new Vector3(-5, 0, 0),  // left
        new Vector3(0, 0, -5),  // back
        new Vector3(5, 0, 0),   // right
        new Vector3(0, 5, 0),   // ceiling
        new Vector3(0, -5, 0)   // underground
    };

    string[] names = {
        "Front",
        "Left",
        "Back",
        "Right",
        "Ceiling",
        "Underground"
    };

    static readonly Vector3[] weight =
    {
        new Vector3(1.0f, 0.0f, 0.0f),  // front
        new Vector3(0.0f, 1.0f, 0.0f),  // left
        new Vector3(-1.0f, 0.0f, 0.0f), // back
        new Vector3(0.0f, -1.0f, 0.0f), // right
        new Vector3(0.0f, 0.0f, 1.0f),  // ceiling
        new Vector3(0.0f, 0.0f, -1.0f)  // underground
    };

    AudioSource[] audioSources = new AudioSource[6];

    public void SetupSpeakers() {
        int index = 0;
        foreach (var position in positions) {
            var go = new GameObject(names[index], typeof(AudioSource));
            go.transform.SetParent(transform);
            go.transform.position = position;
            audioSources[index] = go.GetComponent<AudioSource>();
            audioSources[index].spatialize = true;
            audioSources[index].spatialBlend = 1.0f;
            audioSources[index].dopplerLevel = 0.0f;
            index++;
        }
    }

    void Update () {
        transform.position = Camera.main.transform.position;
    }

    public void SetData(float[] data, int frequency) {
        int speakerIndex = 0;
        float[] decoded = new float[data.Length / 4];
        foreach (var audioSource in audioSources) {
            Decode(decoded, data, weight[speakerIndex]);
            audioSource.clip = AudioClip.Create(names[speakerIndex], data.Length / 4, 1, frequency, false);
            audioSource.clip.SetData(decoded, 0);
            speakerIndex++;
        }
    }

    void Decode(float[] output, float[] data, Vector3 weight) {
        for (int i = 0; i < output.Length; i++) {
            output[i] = (data[i * 4]
                      + data[i * 4 + 3] * weight[0]
                      + data[i * 4 + 1] * weight[1]
                      + data[i * 4 + 2] * weight[2]) / 4.0f;
        }
    }

    public void SetOutput(AudioMixerGroup output) {
        foreach (var audioSource in audioSources) {
            audioSource.outputAudioMixerGroup = output;
        }
    }

    public void SetMute(bool mute) {
        foreach (var audioSource in audioSources) {
            audioSource.mute = mute;
        }
    }

    public void SetBypassEffects(bool bypassEffects) {
        foreach (var audioSource in audioSources) {
            audioSource.bypassEffects = bypassEffects;
        }
    }

    public void SetBypassListenerEffects(bool bypassListenerEffects) {
        foreach (var audioSource in audioSources) {
            audioSource.bypassListenerEffects = bypassListenerEffects;
        }
    }

    public void SetBypassReverbZones(bool bypassReverbZones) {
        foreach (var audioSource in audioSources) {
            audioSource.bypassReverbZones = bypassReverbZones;
        }
    }

    public void SetPlayOnAwake(bool playOnAwake) {
        foreach (var audioSource in audioSources) {
            audioSource.playOnAwake = playOnAwake;
        }
    }

    public void SetLoop(bool loop) {
        foreach (var audioSource in audioSources) {
            audioSource.loop = loop;
        }
    }

    public void SetPriority(int priority) {
        foreach (var audioSource in audioSources) {
            audioSource.priority = priority;
        }
    }

    public void SetVolume(float volume) {
        foreach (var audioSource in audioSources) {
            audioSource.volume = volume;
        }
    }

    public void SetPitch(float pitch) {
        foreach (var audioSource in audioSources) {
            audioSource.pitch = pitch;
        }
    }

    public void SetReverbZoneMix(float reverbZoneMix) {
        foreach (var audioSource in audioSources) {
            audioSource.reverbZoneMix = reverbZoneMix;
        }
    }

    public void Play() {
        foreach (var audioSource in audioSources) {
            audioSource.Play();
        }
    }

    public void PlayDelayed(float delay) {
        foreach (var audioSource in audioSources) {
            audioSource.PlayDelayed(delay);
        }
    }

    public void PlayOneShot(AudioClip clip, float volumeScale = 1.0f) {
        foreach (var audioSource in audioSources) {
            audioSource.PlayOneShot(clip, volumeScale);
        }
    }

    public void PlayScheduled(double time) {
        foreach (var audioSource in audioSources) {
            audioSource.PlayScheduled(time);
        }
    }

    public void SetScheduledStartTime(double time) {
        foreach (var audioSource in audioSources) {
            audioSource.SetScheduledStartTime(time);
        }   
    }

    public void SetScheduledEndTime(double time) {
        foreach (var audioSource in audioSources) {
            audioSource.SetScheduledEndTime(time);
        }
    }

    public float GetTime() {
        return audioSources[0].time;
    }

    public void SetTime(float time) {
        foreach (var audioSource in audioSources) {
            audioSource.time = time;
        }
    }

    public int GetTimeSamples() {
        return audioSources[0].timeSamples;
    }

    public void SetTimeSamples(int timeSamples) {
        foreach (var audioSource in audioSources) {
            audioSource.timeSamples = timeSamples;
        }
    }

    public void Stop() {
        foreach (var audioSource in audioSources) {
            audioSource.Stop();
        }
    }

    public void Pause() {
        foreach (var audioSource in audioSources) {
            audioSource.Pause();
        }
    }

    public void UnPause() {
        foreach (var audioSource in audioSources) {
            audioSource.UnPause();
        }
    }

    public bool GetIsPlaying() {
        return audioSources[0].isPlaying;
    }
}
