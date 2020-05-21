using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTransition : MonoBehaviour
{
    public AudioClip loop_track;
    AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = gameObject.GetComponent<AudioSource>();
        audioSrc.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSrc)
            audioSrc = gameObject.GetComponent<AudioSource>();

        if(!audioSrc.isPlaying)
        {
            audioSrc.clip = loop_track;
            audioSrc.loop = true;
            audioSrc.Play();
        }
    }
}
