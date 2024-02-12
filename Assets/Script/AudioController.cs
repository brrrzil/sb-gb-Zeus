using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
        StartCoroutine(AudioFadeIn());
    }

    private IEnumerator AudioFadeIn()
    {
        for(int i = 0; audioSource.volume <= 1; i++)
        {
            yield return new WaitForSeconds(0.1f);
            audioSource.volume += 0.01f;
        }
    }
}