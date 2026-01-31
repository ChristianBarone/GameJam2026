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
            new Vector3(Random.Range(0.3f, 0.6f), 1.1f, 0)
        );

        spawnPos.z = 0;

        int randomIndex = Random.Range(0, 4);
        float rotationZ = randomIndex * 90f;

        GameObject obj = Instantiate(
            prefabs[Random.Range(0, prefabs.Length)],
            spawnPos,
            Quaternion.Euler(0, 0, rotationZ)
        );


        RGBElement rgb = obj.GetComponent<RGBElement>();
        if (rgb != null)
        {
            bool r = false;
            bool g = false;
            bool b = false;

            do
            {
                r = Random.value < 0.5f;
                g = Random.value < 0.5f;
                b = Random.value < 0.5f;
            } while (!r && !b && !g);

            rgb.SetColor(r, g, b);
        }
        
        float scale = 1.0f;
        obj.transform.localScale = new Vector3(scale, scale, scale);
    }

    void recalcSpawnrate() {
        spawnRate = Random.Range(2.0f, 4.0f);
    }
}
