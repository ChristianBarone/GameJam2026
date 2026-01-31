using UnityEngine;
using UnityEngine.UI;

public class MaskManager : MonoBehaviour
{
    public static MaskManager instance;

    public bool rMaskOn;
    public bool gMaskOn;
    public bool bMaskOn;    
    
    public bool rMaskCharging;
    public bool gMaskCharging;
    public bool bMaskCharging;

    public float rTimer;
    public float gTimer;
    public float bTimer;

    public Slider rSlider;
    public Slider gSlider;
    public Slider bSlider;

    const float MASK_TIME = 5.0f;

    private Camera cam;

    int masksOn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;

        rMaskOn = false;
        gMaskOn = false;
        bMaskOn = false;

        rTimer = MASK_TIME;
        gTimer = MASK_TIME;
        bTimer = MASK_TIME;

        masksOn = 0;
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R)) { WearOrTakeOffMask(ref rMaskOn); }
        if (Input.GetKeyDown(KeyCode.G)) { WearOrTakeOffMask(ref gMaskOn); }
        if (Input.GetKeyDown(KeyCode.B)) { WearOrTakeOffMask(ref bMaskOn); }
        */

        if (Input.GetKey(KeyCode.R) && !rMaskCharging) WearMask(ref rMaskOn); else TakeOffMask(ref rMaskOn);
        if (Input.GetKey(KeyCode.G) && !gMaskCharging) WearMask(ref gMaskOn); else TakeOffMask(ref gMaskOn);
        if (Input.GetKey(KeyCode.B) && !bMaskCharging) WearMask(ref bMaskOn); else TakeOffMask(ref bMaskOn);

        Color32 bgColor = new Color32(0,0,0,255);

        if (rMaskOn)
        {
            bgColor.r = 255;
            rTimer -= Time.deltaTime;
            if (rTimer <= 0) { TakeOffMask(ref rMaskOn); rMaskCharging = true; }
        }
        else 
        { 
            rTimer = Mathf.Min(rTimer + Time.deltaTime, MASK_TIME);
            if (rTimer == MASK_TIME) rMaskCharging = false;
        }

        if (gMaskOn)
        {
            bgColor.g = 255;
            gTimer -= Time.deltaTime;
            if (gTimer <= 0) { TakeOffMask(ref gMaskOn); gMaskCharging = true; }
        }
        else 
        { 
            gTimer = Mathf.Min(gTimer + Time.deltaTime, MASK_TIME);
            if (gTimer == MASK_TIME) gMaskCharging = false;
        }

        if (bMaskOn)
        {
            bgColor.b = 255;
            bTimer -= Time.deltaTime;
            if (bTimer <= 0) { TakeOffMask(ref bMaskOn); bMaskCharging = true; }
        }
        else 
        { 
            bTimer = Mathf.Min(bTimer + Time.deltaTime, MASK_TIME);
            if (bTimer == MASK_TIME) bMaskCharging = false;
        }

        rSlider.value = (rTimer / MASK_TIME);
        gSlider.value = (gTimer / MASK_TIME);
        bSlider.value = (bTimer / MASK_TIME);

        cam.backgroundColor = Color.Lerp(cam.backgroundColor, bgColor, 25.0f * Time.deltaTime);
    }

    void TakeOffMask(ref bool maskOn)
    {
        if (!maskOn) return;

        maskOn = false;
        --masksOn;
    }

    void WearMask(ref bool maskOn)
    {
        if (maskOn) return;

        if (masksOn >= 2) return;
        maskOn = true;
        ++masksOn;
    }

    void WearOrTakeOffMask(ref bool maskOn)
    {
        // Can always take it off
        if (maskOn) { TakeOffMask(ref maskOn); return; }

        // Cannot wear mask if 2 are already worn
        WearMask(ref maskOn);
    }
}
