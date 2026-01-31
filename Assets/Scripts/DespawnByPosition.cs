using UnityEngine;

public class DespawnByPosition : MonoBehaviour
{
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        float bottomLimit = cam.ViewportToWorldPoint(new Vector3(0, -0.1f, 0)).y;
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);
        }

    }
}
