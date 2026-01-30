using UnityEngine;

public class RGBElement : MonoBehaviour
{
    public bool r;
    public bool g;
    public bool b;

    SpriteRenderer sprRenderer;

    MaskManager maskManager;

    void Start()
    {
        sprRenderer = GetComponent<SpriteRenderer>();

        maskManager = MaskManager.instance;
    }

    void Update()
    {
        ChangeColor();
    }

    public bool KillsPlayer()
    {
        return sprRenderer.color.a != 0;
    }

    void ChangeColor()
    {
        Color32 c = new Color32(0, 0, 0, 255);
        if (r && !maskManager.rMaskOn) c.r = 255;
        if (g && !maskManager.gMaskOn) c.g = 255;
        if (b && !maskManager.bMaskOn) c.b = 255;

        if (c.r == 0 && c.g == 0 && c.b == 0) c.a = 0;
        sprRenderer.color = c;
    }

    [ContextMenu("Change color")]
    void ChangeColorEditor()
    {
        if (sprRenderer == null) sprRenderer = GetComponent<SpriteRenderer>();
        ChangeColor();
    }
}
