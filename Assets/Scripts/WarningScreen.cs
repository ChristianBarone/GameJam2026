using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WarningScreen : MonoBehaviour
{
    public Animator anim;

    public AudioSource audioS;
    public AudioClip transitionSound;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(.5f);

        yield return new WaitUntil(() => Input.anyKeyDown);

        audioS.PlayOneShot(transitionSound);
        anim.SetTrigger("Start");
        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        
    }
}
