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
        transform.Translate(Vector3.right * scrollSpeed * Time.deltaTime);
    }
}
