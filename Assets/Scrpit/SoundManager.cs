using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioListener audioListener;

    [SerializeField]
    AudioClip[] clip;

    public AudioSource audioSource;


    void Start()
    {
        audioSource.clip = clip[0];
    }

    public void sound0()
    {
        audioSource.clip = clip[0];
    }
    public void sound1()
    {
        audioSource.clip = clip[1];
    }
}
