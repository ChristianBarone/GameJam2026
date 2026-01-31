using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    public void AcabarJuego()
    {
        SceneManager.LoadScene("MainMenu");
    }
}