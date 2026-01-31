using UnityEngine;

public class AutoScrollCamera : MonoBehaviour
{
    public float scrollSpeed = 2f;

    public Transform camChild;

    float camShakeTime = 0;

    void Start()
    {
        camShakeTime = 0;
    }

    void Update()
    {
        if (LevelManager.instance != null)
        {
            float speed = scrollSpeed + 0.4f * LevelManager.instance.currentLevel;
            if (speed > 10.0f) speed = 10.0f;
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (camShakeTime > 0)
        {
            float shakeAmount = Mathf.Min(camShakeTime, 1.0f);
            camChild.eulerAngles = new Vector3(0, 0, Random.Range(-5.0f, 5.0f) * shakeAmount);

            camShakeTime -= Time.deltaTime;
        }
    }

    public void AddScreenShake(float time)
    {
        camShakeTime = Mathf.Max(camShakeTime, time);
    }
}
