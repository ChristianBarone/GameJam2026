using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public static MaskManager instance;

    public bool rMaskOn;
    public bool gMaskOn;
    public bool bMaskOn;

    public float rTimer;
    public float gTimer;
    public float bTimer;

    const float MASK_TIME = 5.0f;

    int masksOn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.R)) { WearOrTakeOffMask(ref rMaskOn); }
        if (Input.GetKeyDown(KeyCode.G)) { WearOrTakeOffMask(ref gMaskOn); }
        if (Input.GetKeyDown(KeyCode.B)) { WearOrTakeOffMask(ref bMaskOn); }

        if (rMaskOn)
        {
            rTimer -= Time.deltaTime;
            // BAR UPDATE
            if (rTimer <= 0) TakeOffMask(ref rMaskOn);
        }
        else { rTimer = Mathf.Min(rTimer + Time.deltaTime, MASK_TIME); }

        if (gMaskOn)
        {
            gTimer -= Time.deltaTime;
            // BAR UPDATE
            if (gTimer <= 0) TakeOffMask(ref gMaskOn);
        }
        else { gTimer = Mathf.Min(gTimer + Time.deltaTime, MASK_TIME); }

        if (bMaskOn)
        {
            bTimer -= Time.deltaTime;
            // BAR UPDATE
            if (bTimer <= 0) TakeOffMask(ref bMaskOn);
        }
        else { bTimer = Mathf.Min(bTimer + Time.deltaTime, MASK_TIME); }
    }

    void TakeOffMask(ref bool maskOn)
    {
        maskOn = false;
        --masksOn;
    }

    void WearOrTakeOffMask(ref bool maskOn)
    {
        // Can always take it off
        if (maskOn) { TakeOffMask(ref maskOn); return; }

        // Cannot wear mask if 2 are already worn
        if (masksOn >= 2) return;
        maskOn = true;
        ++masksOn;
    }
}
