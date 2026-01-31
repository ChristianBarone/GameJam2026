using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void EmpezarJuego()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("RGBTestScene");
    }
}