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
    bool canStopComboWhenDespawned;

    float scale;

    Camera cam;

    void Start()
    {
        cam = Camera.main;

        maskManager = MaskManager.instance;
        interactedWithPlayer = false;

        canStopComboWhenDespawned = true;

        scale = transform.localScale.x;
    }

    void Update()
    {
        float bottomLimit = cam.ViewportToWorldPoint(new Vector3(0, -0.1f, 0)).y;
        if (transform.position.y < bottomLimit)
        {
            Destroy(gameObject);

            if (canStopComboWhenDespawned)
            {
                if (!(r && g && b)) LevelManager.instance.EndCombo();
            }
        }

        Color32 c = new Color32(0, 0, 0, 255);
        Color32 cOutline = sprOutline.color;
        cOutline.a = 0;

        if (r && !maskManager.rMaskOn) c.r = 255;
        if (g && !maskManager.gMaskOn) c.g = 255;
        if (b && !maskManager.bMaskOn) c.b = 255;

        if (c.r == 0 && c.g == 0 && c.b == 0) { c.a = 0; cOutline.a = 255; }

        foreach (SpriteRenderer sprRenderer in sprRendererers)
        {
            sprRenderer.color = c;
        }

        sprOutline.color = cOutline;

        if (interactedWithPlayer)
        {
            scale -= Time.deltaTime * 2;
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
        canStopComboWhenDespawned = false;

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

    public void SetColor(bool _r, bool _g, bool _b)
    {
        r = _r;
        g = _g;
        b = _b;

        Color32 c = new Color32(0, 0, 0, 0);
        if (r) c.r = 150;
        if (g) c.g = 150;
        if (b) c.b = 150;

        if (r && g && b)
        {
            c = new Color32(255, 255, 255, 0);
        }

        sprOutline.color = c;
    }
}
