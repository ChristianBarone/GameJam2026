using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    Vector3 movement;

    Rigidbody2D rb2D;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement = new Vector3(h, v, 0) * speed;
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
