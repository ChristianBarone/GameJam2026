using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public Animator transition;

    public float transitiontime = 1.0f;

    public void EmpezarJuego()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("RGBTestScene");
    }
}