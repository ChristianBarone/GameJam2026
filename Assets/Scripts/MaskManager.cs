using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public bool rMaskOn;
    public bool gMaskOn;
    public bool bMaskOn;

    void Start()
    {
        rMaskOn = false;
        gMaskOn = false;
        bMaskOn = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) rMaskOn = !rMaskOn;
        if (Input.GetKeyDown(KeyCode.G)) gMaskOn = !gMaskOn;
        if (Input.GetKeyDown(KeyCode.B)) bMaskOn = !bMaskOn;
    }
}
