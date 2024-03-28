using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteImage : MonoBehaviour
{
    [SerializeField] Sprite MuteOn;
    [SerializeField] Sprite MuteOff;

    Image _muteImage;
    AudioController AC => FindAnyObjectByType<AudioController>();

    void Awake()
    {
        _muteImage = gameObject.GetComponent<Image>();
        _muteImage.sprite = AC.VolumeState() ? MuteOn : MuteOff;
    }

    public void Mute()
    {
        AC.SwitchVolume();
        Sprite curr_s;
        if (AC.VolumeState())
        {
            curr_s = MuteOn;
        }
        else
        {
            AC.StopAudio();
            curr_s = MuteOff;
        }
        _muteImage.sprite = curr_s;
    }
}
