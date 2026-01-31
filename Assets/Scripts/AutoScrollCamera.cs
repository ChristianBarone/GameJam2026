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
            float speed = scrollSpeed + LevelManager.instance.currentLevel;
            transform.Translate(Vector3.up * speed * Time.deltaTime);

        }
    }

}
