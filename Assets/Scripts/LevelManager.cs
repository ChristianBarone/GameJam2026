using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int currentLevel = 0;
    public int currentPoints = 0;
    public int totalPoints = 0;
    public int pointsToLevelUp = 0;

    public int highscore = 100;

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
    }

    void Update()
    {
        scoreText.text = totalPoints.ToString() + " (LVL " + currentLevel.ToString() + ")";
        highscoreText.text = highscore.ToString();
    }

    public void AddPoint()
    {
        ++currentPoints;
        ++totalPoints;
        if (currentPoints == pointsToLevelUp)
        {
            currentPoints = 0;
            ++currentLevel;

            pointsToLevelUp += 10;
        }
    }
}
