using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Tooltip ("The mixer group which controls the overall music volume.")]
    [SerializeField] private AudioMixerGroup _MusicMixer = null;
    [Tooltip ("The mixer group which controls the overall sound effect volume.")]
    [SerializeField] private AudioMixerGroup _SFXMixer = null;

    public void MuteMusic (bool isMuted)
    {
        if (!isMuted)
            _MusicMixer.audioMixer.SetFloat ("Music Volume", -80.0f);
        else
            _MusicMixer.audioMixer.SetFloat ("Music Volume", 0.0f);
    }

    public void MuteSFX (bool isMuted)
    {
        if (!isMuted)
            _SFXMixer.audioMixer.SetFloat ("SFX Volume", -80.0f);
        else
            _SFXMixer.audioMixer.SetFloat ("SFX Volume", 0.0f);
    }
}
