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

        rTimer = 100;
        gTimer = 100;
        bTimer = 100;

        masksOn = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { WearOrTakeOffMask(ref rMaskOn); }
        if (Input.GetKeyDown(KeyCode.G)) { WearOrTakeOffMask(ref gMaskOn); }
        if (Input.GetKeyDown(KeyCode.B)) { WearOrTakeOffMask(ref bMaskOn); }

        if (rMaskOn)
        {
            rTimer--;
            // BAR UPDATE
            if (rTimer <= 0) TakeOffMask(ref rMaskOn);
        }
        else { rTimer = Mathf.Min(rTimer + 1.0f, 100.0f); }

        if (gMaskOn)
        {
            gTimer--;
            // BAR UPDATE
            if (gTimer <= 0) TakeOffMask(ref gMaskOn);
        }
        else { gTimer = Mathf.Min(gTimer + 1.0f, 100.0f); }

        if (bMaskOn)
        {
            bTimer--;
            // BAR UPDATE
            if (bTimer <= 0) TakeOffMask(ref bMaskOn);
        }
        else { bTimer = Mathf.Min(bTimer + 1.0f, 100.0f); }
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
