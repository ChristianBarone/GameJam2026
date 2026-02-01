using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 0.5f;

    public TextMeshProUGUI highscoreText;

    bool loadingGame = false;

    void Start()
    {
        int highscore = PlayerPrefs.GetInt("Record", 0);
        highscoreText.text = highscore.ToString();
    }

    public void EmpezarJuego()
    {
        if (loadingGame) return;
        loadingGame = true;

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene("Game");
    }
}