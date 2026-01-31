using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int currentLevel = 0;
    public int currentPoints = 0;
    public int pointsToLevelUp = 0;

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
        pointsToLevelUp = 10;
    }

    void Update()
    {
        
    }

    public void AddPoint()
    {
        ++currentPoints;
        if (currentPoints == pointsToLevelUp)
        {
            currentPoints = 0;
            ++currentLevel;

            pointsToLevelUp += 10;
        }
    }
}
