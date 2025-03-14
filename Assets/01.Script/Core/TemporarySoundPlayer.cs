using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class TemporarySoundPlayer : MonoBehaviour
{
    private AudioSource mAudioSource;
    public string ClipName
    {
        get
        {
            return mAudioSource.clip.name;
        }
    }

    public void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioMixerGroup audiomixer, float delay, bool isLoop)
    {
        mAudioSource.outputAudioMixerGroup = audiomixer;
        mAudioSource.loop = isLoop;
        mAudioSource.Play();

        if (!isLoop) { StartCoroutine(COR_DestroyWhenFinish(mAudioSource.clip.length)); }
    }

    public void InitSound2D(AudioClip clip)
    {
        mAudioSource.clip = clip;
    }

    public void InitSound3D(AudioClip clip, float minDistance, float maxDistance)
    {
        mAudioSource.clip = clip;
        mAudioSource.spatialBlend = 1.0f;
        mAudioSource.rolloffMode = AudioRolloffMode.Linear;
        mAudioSource.minDistance = minDistance;
        mAudioSource.maxDistance = maxDistance;
    }

    private IEnumerator COR_DestroyWhenFinish(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        Destroy(gameObject);
    }
}