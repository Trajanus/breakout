using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource levelSource;
    public AudioSource musicSource;
    public static SoundManager instance = null;

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void PlayOneShot(AudioClip clip)
    {
        efxSource.PlayOneShot(clip);
    }

    public IEnumerator PlayAudioSequentially(AudioClip[] clips)
    {
        yield return null;
        for (int i = 0; i < clips.Length; i++)
        {
            levelSource.clip = clips[i];
            levelSource.Play();
            while (levelSource.isPlaying)
            {
                yield return null;
            }
        }
    }
}
