using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnRate = 3.0f;

    float timer;
    Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRate)
        {
            Spawn();
            recalcSpawnrate();
            timer = 0;
        }
    }

    void Spawn()
    {
        Vector3 spawnPos = cam.ViewportToWorldPoint(
            new Vector3(1.1f, Random.Range(0.1f, 0.9f), 0)
        );

        spawnPos.z = 0;

        int randomIndex = Random.Range(0, 4);
        float rotationZ = randomIndex * 90f;

        GameObject obj = Instantiate(
            prefabs[Random.Range(0, prefabs.Length)],
            spawnPos,
            Quaternion.Euler(0, 0, rotationZ)
        );


        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            Color c;
            do
            {
                c = new Color(
                    Random.Range(0, 2),
                    Random.Range(0, 2),
                    Random.Range(0, 2)
                );
            }
            while (c == Color.black);

            sr.color = c;
        }
    }

    void recalcSpawnrate() {
        spawnRate = Random.Range(2.0f, 4.0f);
    }
}
