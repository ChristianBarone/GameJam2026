using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public AutoScrollCamera camController;

    public int currentLevel = 0;
    public int currentPoints = 0;
    public int totalPoints = 0;
    public int pointsToLevelUp = 0;

    public int highscore = 100;

    public int life;
    float invincibilityFrames;

    float timer;
    float scorePassiveInc = 0.05f;

    public Image lifeImage3;
    public Image lifeImage2;
    public Image lifeImage1;

    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        currentLevel = 0;
        currentPoints = 0;
        totalPoints = 0;
        pointsToLevelUp = 10;

        life = 3;
        invincibilityFrames = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scorePassiveInc)
        {
            totalPoints += 10;
            RefreshScore();
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

        camController.AddScreenShake(0.5f);

        --life;
        if (life <= 0) Debug.Log("Game Over!");
    }

    public void AddPoints(int points, float pos)
    {
        totalPoints += 100 * points * (currentLevel + 1);
        Debug.Log(pos);

    }

    public void CheckLvlUp() {
        currentPoints += 1;
        if (currentPoints == pointsToLevelUp)
        {
            currentPoints = 0;
            ++currentLevel;

            if (life < 3 && life > 0) ++life;
            pointsToLevelUp += 3;
        }
    }
    
    void RefreshScore() {
        scoreText.text = totalPoints.ToString() + " (LVL " + currentLevel.ToString() + ")";
    }
}
