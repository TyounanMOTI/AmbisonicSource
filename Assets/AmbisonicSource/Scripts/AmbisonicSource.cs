using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AmbisonicSource : MonoBehaviour {
    [SerializeField]
    AudioClip _clip;
    public AudioClip clip {
        get { return _clip; }
        set {
            _clip = value;
            LoadClip();
        }
    }

    [SerializeField]
    AudioMixerGroup _output;
    public AudioMixerGroup output {
        get { return _output; }
        set {
            _output = value;
            speaker.SetOutput(value);
        }
    }

    [SerializeField]
    bool _mute;
    public bool mute {
        get { return _mute; }
        set {
            _mute = value;
            speaker.SetMute(value);
        }
    }

    [SerializeField]
    bool _bypassEffects;
    public bool bypassEffects {
        get { return _bypassEffects; }
        set {
            _bypassEffects = value;
            speaker.SetBypassEffects(value);
        }
    }

    [SerializeField]
    bool _bypassListenerEffects;
    public bool bypassListenerEffects {
        get { return _bypassListenerEffects; }
        set {
            _bypassListenerEffects = value;
            speaker.SetBypassListenerEffects(value);
        }
    }

    [SerializeField]
    bool _bypassReverbZones;
    public bool bypassReverbZones {
        get { return _bypassReverbZones; }
        set {
            _bypassReverbZones = value;
            speaker.SetBypassReverbZones(value);
        }
    }

    [SerializeField]
    bool _playOnAwake = true;
    public bool playOnAwake {
        get { return _playOnAwake; }
        set {
            _playOnAwake = value;
            speaker.SetPlayOnAwake(value);
        }
    }

    [SerializeField]
    bool _loop;
    public bool loop {
        get { return _loop; }
        set {
            _loop = value;
            speaker.SetLoop(value);
        }
    }

    [SerializeField, Range(0, 256)]
    int _priority;
    public int priority {
        get { return _priority; }
        set {
            _priority = value;
            speaker.SetPriority(value);
        }
    }

    [SerializeField, Range(0.0f, 1.0f)]
    float _volume = 1.0f;
    public float volume {
        get { return _volume; }
        set {
            _volume = value;
            speaker.SetVolume(value);
        }
    }

    [SerializeField, Range(-3.0f, 3.0f)]
    float _pitch = 1.0f;
    public float pitch {
        get { return _pitch; }
        set {
            _pitch = value;
            speaker.SetPitch(value);
        }
    }

    [SerializeField, Range(0.0f, 1.1f)]
    float _reverbZoneMix = 1.0f;
    public float reverbZoneMix {
        get { return _reverbZoneMix; }
        set {
            _reverbZoneMix = value;
            speaker.SetReverbZoneMix(value);
        }
    }

    public float time {
        get { return speaker.GetTime(); }
        set { speaker.SetTime(value); }
    }

    public int timeSamples {
        get { return speaker.GetTimeSamples(); }
        set { speaker.SetTimeSamples(value); }
    }

    AmbisonicSpeaker speaker;

    void Start() {
        var speakerObject = new GameObject("AmbisonicSpeaker", typeof(AmbisonicSpeaker));
        speakerObject.transform.SetParent(transform);
        speaker = speakerObject.GetComponent<AmbisonicSpeaker>();
        speaker.SetupSpeakers();
        speaker.SetOutput(output);
        speaker.SetMute(mute);
        speaker.SetBypassEffects(bypassEffects);
        speaker.SetBypassListenerEffects(bypassListenerEffects);
        speaker.SetBypassReverbZones(bypassReverbZones);
        speaker.SetPlayOnAwake(playOnAwake);
        speaker.SetLoop(loop);
        speaker.SetPriority(priority);
        speaker.SetVolume(volume);
        speaker.SetPitch(pitch);
        speaker.SetReverbZoneMix(reverbZoneMix);

        if (clip != null) {
            LoadClip();
            if (playOnAwake) {
                speaker.Play();
            }
        }
    }

    void LoadClip() {
        if (clip.loadType != AudioClipLoadType.DecompressOnLoad) {
            Debug.LogError("AmbisonicSource is currently not support DecompressOnLoad.");
        }
        var data = new float[clip.samples * clip.channels];
        clip.GetData(data, 0);
        speaker.SetData(data, clip.frequency);
    }
}
