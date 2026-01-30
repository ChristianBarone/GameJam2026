using UnityEngine;

public class RGBElement : MonoBehaviour
{
    public bool r;
    public bool g;
    public bool b;

    private SpriteRenderer sprRenderer;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        Color32 c = new Color32(0, 0, 0, 255);
        if (r) c.r = 255;
        if (g) c.g = 255;
        if (b) c.b = 255;

        sprRenderer.color = c;
    }

    [ContextMenu("Change color")]
    void ChangeColorEditor()
    {
        if (sprRenderer == null) sprRenderer = GetComponent<SpriteRenderer>();
        ChangeColor();
    }
}
