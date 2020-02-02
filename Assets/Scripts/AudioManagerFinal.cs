using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerFinal : MonoBehaviour
{

    private AudioSource Music,WMsound;
    public AudioClip music,roombaMusic,wm_shot, pickUpSound, player_shot, player_hit;

    void Start()
    {
        Music = transform.GetComponents<AudioSource>()[0];
        WMsound = transform.GetComponents<AudioSource>()[1];
    }

    public void PlayWMShot()
    {
        WMsound.PlayOneShot(wm_shot);
    }

    public void PlayMusic()
    {
        Music.PlayOneShot(music);
    }
}
