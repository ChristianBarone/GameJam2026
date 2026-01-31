using UnityEngine;

public class AutoScrollCamera : MonoBehaviour
{
    public float scrollSpeed = 2f;

    public Transform camChild;

    float camShakeTime = 0;

    float deadPlayerTime = 0;

    Camera cam;

    void Start()
    {
        cam = Camera.main;

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
            float baseRotation = 0;
            if (Time.timeScale == 0)
            {
                deadPlayerTime += Time.unscaledDeltaTime;
                float t = deadPlayerTime / 5;
                if (t > 1) t = 1;

                baseRotation = Mathf.Lerp(0, 50, t);
                cam.orthographicSize = Mathf.Lerp(5, 4, t);
            }

            float shakeAmount = Mathf.Min(camShakeTime, 1.0f);
            camChild.eulerAngles = new Vector3(0, 0, baseRotation + Random.Range(-5.0f, 5.0f) * shakeAmount);

            camShakeTime -= Time.unscaledDeltaTime;
        }
    }

    public void AddScreenShake(float time)
    {
        camShakeTime = Mathf.Max(camShakeTime, time);
    }
}
