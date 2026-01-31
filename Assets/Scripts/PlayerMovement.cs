using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    float minX, maxX, minY, maxY;

    Vector3 movement;

    Rigidbody2D rb2D;
    void Start()
    {
        Camera cam = Camera.main;
        rb2D = GetComponent<Rigidbody2D>();

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float padding = 0.5f;

        minX = bottomLeft.x + padding;
        maxX = topRight.x - padding;
        minY = bottomLeft.y + padding;
        maxY = topRight.y - padding;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement = new Vector3(h, v, 0) * speed;

        /*
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
        */
    }

    void FixedUpdate()
    {
        rb2D.velocity = movement * Time.fixedDeltaTime;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        RGBElement e = col.GetComponent<RGBElement>();
        if (e == null) return;

        if (e.KillsPlayer()) Debug.Log("You are dead!");
    }
}
