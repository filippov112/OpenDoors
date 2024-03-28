using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ������ ��� ���� ��������, �� ������� �������������� ������� ������
public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds; // ������ ������ ��� ����������� �������

    private AudioSource audioSrc => GetComponent<AudioSource>(); // � ������� ������ ������� �������� �����

 
    // ���� ��������� ��� ������ ���� �������� ��������� ��������� ��������� ��������� ������ 
    // ����� ����������� ��������� ������ ��� ����������
    [SerializeField] SoundArrays[] randSounds;
    [System.Serializable]
    public class SoundArrays
    {
        public AudioClip[] soundArray;
        public int length() { return soundArray.Length; }

        public SoundArrays(AudioClip[] soundArray)
        {
            this.soundArray = soundArray;
        }
    }


    public void PlaySound(int i, float volume = 1f, bool destroyed = true, float p1 = 0.5f, float p2 = 0.5f, bool random = false)
    {
        AudioClip _sound = random ? randSounds[i].soundArray[Random.Range(0, randSounds[i].length())] : sounds[i];
        audioSrc.pitch = p1 == p2 ? p1 : Random.Range(p1, p2);
        if (destroyed)
        {
            // ������� ������� �������� ����� �� �����, � ������� ��� ����� ���������������
            AudioSource.PlayClipAtPoint(_sound, transform.position, volume); 
        } else
        {
            audioSrc.PlayOneShot(_sound, volume);
        }
        
    }
}
