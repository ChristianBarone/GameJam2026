using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    public float transitiontime = 1.0f;

    public void EmpezarJuego()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitiontime);

        SceneManager.LoadScene("RGBTestScene");
    }
}