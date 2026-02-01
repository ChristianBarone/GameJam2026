using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public AutoScrollCamera camController;
    public Transform playerTransform;

    public GameObject gameOverPanel;
    public TextMeshProUGUI EndScreenScore;
    public TextMeshProUGUI EndScreenHighscore;

    public int currentLevel = 0;
    public int currentPointsBeforeNextLevel = 0;
    public int totalPoints = 0;
    public int pointsToLevelUp = 0;

    public int highscore = 250000;

    public int life;
    float invincibilityFrames;

    int combo;

    float timer;
    float scorePassiveInc = 0.2f;

    public Image lifeImage3;
    public Image lifeImage2;
    public Image lifeImage1;

    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI scoreText;

    public GameObject scoreTextEffect;

    public Animator hurtPanelAnim;
    public Animator gameplayPanelAnim;
    public Animator transitionAnim;

    Camera cam;
    AudioManager audioManager;

    bool didGameOver;

    bool loadingScene;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);

        cam = Camera.main;
        audioManager = AudioManager.instance;

        currentLevel = 0;
        currentPointsBeforeNextLevel = 0;
        totalPoints = 0;
        pointsToLevelUp = 2500;

        highscore = PlayerPrefs.GetInt("Record", 250000);

        combo = 0;

        didGameOver = false;
        loadingScene = false;

        life = 3;
        invincibilityFrames = 0;
    }

    void Update()
    {
        RefreshScore();
        timer += Time.deltaTime;

        if (didGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R)) RestartGame();
            if (Input.GetKeyDown(KeyCode.M)) ReturnToMainMenu();
        }

        if (timer >= scorePassiveInc)
        {
            totalPoints += 1;
            timer = 0;
        }        
        highscoreText.text = highscore.ToString();

        if (invincibilityFrames < 0) invincibilityFrames -= Time.deltaTime;
        else invincibilityFrames = 0;

        lifeImage1.color = (life >= 1) ? Color.magenta : Color.gray;
        lifeImage2.color = (life >= 2) ? Color.magenta : Color.gray;
        lifeImage3.color = (life >= 3) ? Color.magenta : Color.gray;
    }

    public void TakeDamage()
    {
        if (invincibilityFrames > 0) return;

        invincibilityFrames = 3;

        EndCombo();

        --life;
        if (life <= 0)
        {
            didGameOver = true;

            Time.timeScale = 0f;

            camController.AddScreenShake(5.1f);

            hurtPanelAnim.SetTrigger("Dead");
            gameplayPanelAnim.SetTrigger("Dead");

            gameOverPanel.SetActive(true);
            gameOverPanel.GetComponent<Animator>().SetTrigger("Effect");
            gameOverPanel.GetComponent<Animator>().SetBool("Record", totalPoints > highscore);

            if (totalPoints > highscore)
            {
                audioManager.PlayDeathSoundYesRecord();
                PlayerPrefs.SetInt("Record", totalPoints);
            }
            else audioManager.PlayDeathSoundNoRecord();

            EndScreenScore.text = totalPoints.ToString();
            EndScreenHighscore.text = highscore.ToString();
        }
        else
        {
            hurtPanelAnim.SetTrigger("Hurt");

            camController.AddScreenShake(0.5f);

            audioManager.PlayHurtSound();
            if (life == 1) audioManager.PlayLowLifeAlertSound();
        }
    }

    public void AddPoints(int points, Vector2 pos, bool penaltyForDoubleMask)
    {
        float y = cam.WorldToViewportPoint(pos).y;
        int verticalMultiplier = Mathf.RoundToInt(y + 1);
        verticalMultiplier = Mathf.Clamp(verticalMultiplier, 1, 2);

        string pointsColor = "white";
        if (verticalMultiplier == 2) pointsColor = "green";

        ++combo;
        if (combo > 5) combo = 5;

        int addedPointsBeforeCombo = 100 * points * verticalMultiplier * (currentLevel + 1);

        if (penaltyForDoubleMask) {
            addedPointsBeforeCombo /= 2;
            pointsColor = "red";
        }

        int addedPoints = addedPointsBeforeCombo * combo;

        totalPoints += addedPoints;
        currentPointsBeforeNextLevel += addedPoints;

        audioManager.PlayGetPointSound(combo);

        bool lvlUp = CheckLvlUp();
        if (lvlUp) { audioManager.PlayLevelUpSound(); CreateScoreTextEffect("+<color=" + pointsColor + ">" + addedPointsBeforeCombo.ToString() + "</color><color=yellow> x" + combo.ToString() + "</color>\n<color=red>\u2665</color> <color=blue>LVL UP</color> <color=red>\u2665</color>"); }
        else CreateScoreTextEffect("+<color=" + pointsColor + ">" + addedPointsBeforeCombo.ToString() + " </color><color=yellow>x" + combo.ToString() + "</color>");
    }

    void CreateScoreTextEffect(string t)
    {
        GameObject GO = Instantiate(scoreTextEffect, camController.transform);
        GO.transform.position = playerTransform.position + Vector3.right * 0.5f;

        TextMeshPro text = GO.GetComponentInChildren<TextMeshPro>();
        text.text = t;

        Destroy(GO, 2);
    }

    bool CheckLvlUp() 
    {
        if (currentPointsBeforeNextLevel >= pointsToLevelUp)
        {
            currentPointsBeforeNextLevel = 0;
            ++currentLevel;

            if (life < 3 && life > 0) ++life;
            pointsToLevelUp += 5500;

            return true;
        }

        return false;
    }
    
    void RefreshScore() 
    {
        scoreText.text = totalPoints.ToString() + " (LVL " + currentLevel.ToString() + ")";
    }

    public void EndCombo()
    {
        if (combo > 0)
        {
            audioManager.PlayComboEndedSound();
            CreateScoreTextEffect("<color=red>Combo ended</color>");
        }
        combo = 0;
    }

    public bool GetIsRGBValid(bool r, bool g, bool b)
    {
        if (!r && !g && !b) return false;
        if (currentLevel >= 10) return true;

        if (r && !g && !b) return true; // RED => Always
        else if (!r && g && !b) return currentLevel >= 1; // GREEN => Level 1
        else if (!r && !g && b) return currentLevel >= 2; // BLUE => Level 2

        else if (r && g && !b) return currentLevel >= 5; // YELLOW => Level 5
        else if (!r && g && b) return currentLevel >= 6; // CYAN => Level 6
        else if (r && g && !b) return currentLevel >= 7; // MAGENTA => Level 7

        else if (r && g && b) return currentLevel >= 10; // WHITE => Level 10

        return false;
    }

    public void RestartGame()
    {
        if (loadingScene) return;
        loadingScene = true;

        StartCoroutine(LoadScene(SceneManager.GetActiveScene().name));
    }

    public void ReturnToMainMenu()
    {
        if (loadingScene) return;
        loadingScene = true;

        StartCoroutine(LoadScene("MainMenu"));
    }

    IEnumerator LoadScene(string level)
    {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(4.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(level);
    }
}
