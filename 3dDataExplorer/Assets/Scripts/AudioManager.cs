using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AudioChoice { Normal, Intense, None };

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip intense;
    public AudioClip normal;
    public Slider audioSlider;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.loop = true;
        AudioChoice ac = StartMenuHandler.audioChoice;
        switch(ac)
        {
            case AudioChoice.Normal:
                audioSource.clip = normal;
                audioSource.Play();
                break;
            case AudioChoice.Intense:
                audioSource.clip = intense;
                audioSource.Play();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Update audio volume
        //audioSource.volume = audioSlider.value;
    }
}
