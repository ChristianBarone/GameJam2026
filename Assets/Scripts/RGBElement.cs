using UnityEngine;

public class RGBElement : MonoBehaviour
{
    public bool r;
    public bool g;
    public bool b;

    public SpriteRenderer[] sprRendererers;
    public SpriteRenderer sprOutline;

    MaskManager maskManager;

    bool interactedWithPlayer;

    float scale;

    void Start()
    {
        maskManager = MaskManager.instance;
        interactedWithPlayer = false;

        scale = transform.localScale.x;
    }

    void Update()
    {
        ChangeColor();

        if (interactedWithPlayer)
        {
            scale -= Time.deltaTime;
            if (scale < 0) scale = 0;
            transform.localScale = Vector3.one * scale;
        }
    }

    public bool KillsPlayer()
    {
        return sprRendererers[0].color.a != 0;
    }

    public bool HasInteractedWithPlayer()
    {
        return interactedWithPlayer;
    }

    public void InteractWithPlayer()
    {
        interactedWithPlayer = true;
    }

    public int PointsToGive()
    {
        int points = 0;

        if (r) ++points;
        if (g) ++points;
        if (b) ++points;

        return points;
    }

    void ChangeColor()
    {
        Color32 c = new Color32(0, 0, 0, 255);
        if (r && !maskManager.rMaskOn) c.r = 255;
        if (g && !maskManager.gMaskOn) c.g = 255;
        if (b && !maskManager.bMaskOn) c.b = 255;

        if (c.r == 0 && c.g == 0 && c.b == 0) c.a = 0;

        foreach (SpriteRenderer sprRenderer in sprRendererers)
        {
            sprRenderer.color = c;
        }
    }

    public void SetColor(bool _r, bool _g, bool _b)
    {
        r = _r;
        g = _g;
        b = _b;

        Color32 c = new Color32(0, 0, 0, 255);
        if (r) c.r = 200;
        if (g) c.g = 200;
        if (b) c.b = 200;

        sprOutline.color = c;
    }
}
