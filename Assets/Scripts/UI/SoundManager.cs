using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    public AudioSource efxSource;
    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    [SerializeField]
    List<AudioClip> clips;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("SoundManager already exists.");
            return;
        }
        Instance = this;
    }

    //播放指定的音乐clip
    public void PlaySingle(ClipsType index)
    {
        efxSource.clip = clips[(int)index];
        efxSource.Play();
    }

    //在指定的音乐列表中clips以随机音量播放一段随机音乐
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.clip = clips[randomIndex];
        efxSource.pitch = randomPitch;
        efxSource.Play();
    }
}
