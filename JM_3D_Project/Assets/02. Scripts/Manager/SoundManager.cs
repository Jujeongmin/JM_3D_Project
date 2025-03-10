using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Sound[] bgmSounds, sfxSounds;
    public AudioMixer myMixer;
    public AudioSource bgmSource, sfxSource;

    public float MasterValue { get; private set; }
    public float BgmValue { get; private set; }
    public float SFXValue { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMusic("Background");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(string name)
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            MasterValue = PlayerPrefs.GetFloat("MasterVolume");
            myMixer.SetFloat("Master", Mathf.Log10(MasterValue) * 20);
        }
        else
        {
            MasterValue = 0.5f;
        }

        if (PlayerPrefs.HasKey("BGMVolume"))
        {
            BgmValue = PlayerPrefs.GetFloat("BGMVolume");
            myMixer.SetFloat("BGM", Mathf.Log10(BgmValue) * 20);
        }
        else
        {
            BgmValue = 0.5f;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            SFXValue = PlayerPrefs.GetFloat("SFXVolume");
            myMixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXValue")) * 20);
        }
        else
        {
            SFXValue = 0.5f;
        }


        Sound s = Array.Find(bgmSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            bgmSource.clip = s.clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        bgmSource.mute = !bgmSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
