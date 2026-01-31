using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void AcabarJuego()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}