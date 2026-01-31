using UnityEngine;

public class AutoScrollCamera : MonoBehaviour
{
    public float scrollSpeed = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (LevelManager.instance != null)
        {
            float speed = scrollSpeed + 0.4f * LevelManager.instance.currentLevel;
            if (speed > 10.0f) speed = 10.0f;
            transform.Translate(Vector3.up * speed * Time.deltaTime);

        }
    }

}
