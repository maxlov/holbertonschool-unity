using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepAudio : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    private AudioClip[] walkingGrassClips;
    [SerializeField]
    private AudioClip[] walkingStoneClips;

    [SerializeField]
    private AudioClip[] landingGrassClips;
    [SerializeField]
    private AudioClip[] landingStoneClips;

    private AudioSource steppingSource;
    private AudioSource landingSource;

    private void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        steppingSource = sources[0];
        landingSource = sources[1];
    }

    private AudioClip getRandomClip(AudioClip[] clips)
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void Step()
    {
        AudioClip clip;

        if (playerController.groundName == "Stone (Instance)")
            clip = getRandomClip(walkingStoneClips);
        else
            clip = getRandomClip(walkingGrassClips);

        steppingSource.PlayOneShot(clip);
    }

    public void Land()
    {
        AudioClip clip;

        if (playerController.groundName == "Stone (Instance)")
            clip = getRandomClip(landingStoneClips);
        else
            clip = getRandomClip(landingGrassClips);

        landingSource.PlayOneShot(clip);
    }
}
