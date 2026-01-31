using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] hurtSounds;
    public AudioClip deathSound;

    public AudioClip lowLifeAlertSound;

    public AudioClip[] pointSounds;

    public AudioClip levelUpSound;

    public AudioClip[] putOnMaskSounds;
    public AudioClip[] putOffMaskSounds;

    public AudioSource audioS;
    public AudioSource heartbeatAudioS;

    bool playingHeartbeat = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playingHeartbeat = false;
    }

    void Update()
    {
        if (playingHeartbeat) heartbeatAudioS.volume = Mathf.Lerp(heartbeatAudioS.volume, 1, Time.deltaTime);
        else heartbeatAudioS.volume = 0;
    }

    public void PlayHurtSound()
    {
        audioS.PlayOneShot(hurtSounds[Random.Range(1, hurtSounds.Length)]);
    }

    public void PlayDeathSound()
    {
        audioS.Stop();
        playingHeartbeat = false;
        audioS.PlayOneShot(deathSound);
    }

    public void PlayLowLifeAlertSound()
    {
        playingHeartbeat = true;
        audioS.PlayOneShot(lowLifeAlertSound);
    }

    public void PlayGetPointSound()
    {
        audioS.PlayOneShot(pointSounds[Random.Range(1, pointSounds.Length)]);
    }

    public void PlayLevelUpSound()
    {
        audioS.PlayOneShot(levelUpSound);
        playingHeartbeat = false;
    }

    public void PlayMaskPutOnSound()
    {
        audioS.PlayOneShot(putOnMaskSounds[Random.Range(1, putOnMaskSounds.Length)], 0.5f);
    }

    public void PlayMaskPutOffSound()
    {
        audioS.PlayOneShot(putOffMaskSounds[Random.Range(1, putOffMaskSounds.Length)], 0.5f);
    }
}
