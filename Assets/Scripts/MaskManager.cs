using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public static MaskManager instance;

    public bool rMaskOn;
    public bool gMaskOn;
    public bool bMaskOn;

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

        masksOn = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { WearOrTakeOffMask(ref rMaskOn); }
        if (Input.GetKeyDown(KeyCode.G)) { WearOrTakeOffMask(ref gMaskOn); }
        if (Input.GetKeyDown(KeyCode.B)) { WearOrTakeOffMask(ref bMaskOn); }
    }

    void WearOrTakeOffMask(ref bool maskOn)
    {
        // Can always take it off
        if (maskOn) { maskOn = false; --masksOn; return; }

        // Cannot wear mask if 2 are already worn
        if (masksOn >= 2) return;
        maskOn = true;
        ++masksOn;
    }
}
