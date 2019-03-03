using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenuBehavior : MonoBehaviour
{
    public AudioMixer Mixer;
    
    public void SetVolume(float volume)
    {
        Mixer.SetFloat("volume", volume);
    }

    public void QualityChange(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
