using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource back_music;
    [SerializeField] AudioSource good_sign;
    [SerializeField] AudioSource bad_sign;

    bool _isPlaying = false;

    public bool VolumeState()
    {
        return Env.Instance.getMute();
    }

    public void SwitchVolume()
    {
        Env.Instance.setMute(!Env.Instance.getMute());
    }

    public void StopAudio()
    {
        if (_isPlaying)
        {
            _isPlaying = false;
            back_music.Stop();
            good_sign.Stop();
            bad_sign.Stop();
        }
    }

    public void PlayGood()
    {
        if (Env.Instance.getMute())
        {
            _isPlaying = true;
            good_sign.Play();
        }
    }

    public void PlayBad()
    {
        if (Env.Instance.getMute())
        {
            _isPlaying = true;
            bad_sign.Play();
        }
    }

    public void PlayBack()
    {
        if (Env.Instance.getMute())
        {
            _isPlaying = true;
            back_music.Play();
        }  
    }
}
