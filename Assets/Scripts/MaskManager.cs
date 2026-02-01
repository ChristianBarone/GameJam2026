using UnityEngine;
using UnityEngine.UI;

public class MaskManager : MonoBehaviour
{
    public static MaskManager instance;

    public SpriteRenderer grid;

    public bool rMaskOn;
    public bool gMaskOn;
    public bool bMaskOn;

    public SpriteRenderer playerMaskSprite001;
    public SpriteRenderer playerMaskSprite002;
    public Sprite maskRSprite;
    public Sprite maskGSprite;
    public Sprite maskBSprite;
    
    public bool rMaskCharging;
    public bool gMaskCharging;
    public bool bMaskCharging;

    public float rTimer;
    public float gTimer;
    public float bTimer;

    public Slider rSlider;
    public Image rSliderBar;
    public Slider gSlider;
    public Image gSliderBar;
    public Slider bSlider;
    public Image bSliderBar;

    const float MASK_TIME = 3.0f;

    Camera cam;
    AudioManager audioManager;

    int masksOn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        audioManager = AudioManager.instance;

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
        if (Input.GetKeyDown(KeyCode.Alpha1)) { WearOrTakeOffMask(ref rMaskOn); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { WearOrTakeOffMask(ref gMaskOn); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { WearOrTakeOffMask(ref bMaskOn); }
        */

        if (Input.GetKey(KeyCode.Alpha1) && !rMaskCharging) WearMask(ref rMaskOn); else TakeOffMask(ref rMaskOn);
        if (Input.GetKey(KeyCode.Alpha2) && !gMaskCharging) WearMask(ref gMaskOn); else TakeOffMask(ref gMaskOn);
        if (Input.GetKey(KeyCode.Alpha3) && !bMaskCharging) WearMask(ref bMaskOn); else TakeOffMask(ref bMaskOn);

        Color32 bgColor = new Color32(29, 29, 29, 255);

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

        if (masksOn == 0) { playerMaskSprite001.enabled = false; playerMaskSprite002.enabled = false; }
        else if (masksOn == 1)
        {
            playerMaskSprite001.enabled = true;
            playerMaskSprite002.enabled = false;

            playerMaskSprite001.transform.localPosition = new Vector3(0f, .2f, 0);
            playerMaskSprite001.transform.localEulerAngles = new Vector3(0, 0, 0.0f);

            playerMaskSprite001.sprite = (rMaskOn) ? maskRSprite : ((gMaskOn) ? maskGSprite : maskBSprite);
        }
        else
        {
            playerMaskSprite001.enabled = true;
            playerMaskSprite002.enabled = true;

            playerMaskSprite001.transform.localPosition = new Vector3(-0.3f, .2f, 0);
            playerMaskSprite001.transform.localEulerAngles = new Vector3(0, 0, 20.0f);

            if (!rMaskOn) { playerMaskSprite001.sprite = maskGSprite; playerMaskSprite002.sprite = maskBSprite; }
            else if (!gMaskOn) { playerMaskSprite001.sprite = maskRSprite; playerMaskSprite002.sprite = maskBSprite; }
            else if (!bMaskOn) { playerMaskSprite001.sprite = maskRSprite; playerMaskSprite002.sprite = maskGSprite; }
        }

        rSlider.value = (rTimer / MASK_TIME);
        gSlider.value = (gTimer / MASK_TIME);
        bSlider.value = (bTimer / MASK_TIME);

        rSliderBar.color = (!rMaskCharging) ? new Color32(255,0,0,255) : new Color32(70, 0, 0, 255);
        gSliderBar.color = (!gMaskCharging) ? new Color32(0,255, 0,255) : new Color32(0, 70, 0, 255);
        bSliderBar.color = (!bMaskCharging) ? new Color32(0,0,255,255) : new Color32(0, 0, 70, 255);

        //cam.backgroundColor = Color.Lerp(cam.backgroundColor, bgColor, 25.0f * Time.deltaTime);
        grid.color = Color.Lerp(grid.color, bgColor, 25.0f * Time.deltaTime);
    }

    void TakeOffMask(ref bool maskOn)
    {
        if (!maskOn) return;

        maskOn = false;
        --masksOn;

        audioManager.PlayMaskPutOffSound();
    }

    void WearMask(ref bool maskOn)
    {
        if (maskOn) return;

        if (masksOn >= 2)
        {
            audioManager.PlayErrorSound();
            return;
        }
        maskOn = true;
        ++masksOn;

        audioManager.PlayMaskPutOnSound();
    }

    void WearOrTakeOffMask(ref bool maskOn)
    {
        // Can always take it off
        if (maskOn) { TakeOffMask(ref maskOn); return; }

        // Cannot wear mask if 2 are already worn
        WearMask(ref maskOn);
    }
}
