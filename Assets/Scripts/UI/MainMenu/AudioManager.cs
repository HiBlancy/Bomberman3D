using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioMixer mixer;

    public const string MUSIC_KEY = "MusicVolumeGame";
    public const string SFX_KEY = "SfxVolumeGame";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void LoadVolume()
    {
        float MusicVolumeGame = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float SfxVolumeGame = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(MusicVolumeGame) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(SfxVolumeGame) * 20);
    }
}
