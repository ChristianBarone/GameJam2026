using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WarningScreen : MonoBehaviour
{
    public Animator anim;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(3);

        yield return new WaitUntil(() => Input.anyKeyDown);

        anim.SetTrigger("Start");
        yield return new WaitForSeconds(4.5f);

        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {
        
    }
}
