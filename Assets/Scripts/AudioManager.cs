using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip[] hurtSounds;
    public AudioClip deathSoundYesRecord;
    public AudioClip deathSoundNoRecord;

    public AudioClip lowLifeAlertSound;

    public AudioClip[] pointSounds;

    public AudioClip levelUpSound;

    public AudioClip[] putOnMaskSounds;
    public AudioClip[] putOffMaskSounds;

    public AudioClip errorSound;

    public AudioClip comboEndSound;

    public AudioClip maskTimeOutSound;

    public AudioSource audioS;
    public AudioSource heartbeatAudioS;
    public AudioSource maskTimerRAudioS;
    public AudioSource maskTimerGAudioS;
    public AudioSource maskTimerBAudioS;

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

    public void PlayDeathSoundYesRecord()
    {
        audioS.Stop();
        playingHeartbeat = false;
        audioS.PlayOneShot(deathSoundYesRecord);
    }    
    
    public void PlayDeathSoundNoRecord()
    {
        audioS.Stop();
        playingHeartbeat = false;
        audioS.PlayOneShot(deathSoundNoRecord);
    }

    public void PlayLowLifeAlertSound()
    {
        playingHeartbeat = true;
        audioS.PlayOneShot(lowLifeAlertSound);
    }

    public void PlayGetPointSound(int combo)
    {
        int audioIndex = Mathf.Min(combo - 1, pointSounds.Length - 1);
        audioS.PlayOneShot(pointSounds[audioIndex]);
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

    public void PlayErrorSound()
    {
        audioS.PlayOneShot(errorSound);
    }

    public void PlayComboEndedSound()
    {
        audioS.PlayOneShot(comboEndSound);
    }

    public void PlayMaskRSound(float timePos)
    {
        maskTimerRAudioS.time = timePos;
        maskTimerRAudioS.Play();
    }

    public void StopMaskRSound()
    {
        maskTimerRAudioS.Stop();
    }

    public void PlayMaskGSound(float timePos)
    {
        maskTimerGAudioS.time = timePos;
        maskTimerGAudioS.Play();
    }

    public void StopMaskGSound()
    {
        maskTimerGAudioS.Stop();
    }

    public void PlayMaskBSound(float timePos)
    {
        maskTimerBAudioS.time = timePos;
        maskTimerBAudioS.Play();
    }

    public void StopMaskBSound()
    {
        maskTimerBAudioS.Stop();
    }

    public void PlayMaskTimeOutSound()
    {
        audioS.PlayOneShot(maskTimeOutSound);
    }
}
