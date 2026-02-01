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

    public int masksOn;

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

        if (Input.GetKey(KeyCode.Alpha1) && !rMaskCharging) WearMask(ref rMaskOn); else TakeOffMask(ref rMaskOn);
        if (Input.GetKey(KeyCode.Alpha2) && !gMaskCharging) WearMask(ref gMaskOn); else TakeOffMask(ref gMaskOn);
        if (Input.GetKey(KeyCode.Alpha3) && !bMaskCharging) WearMask(ref bMaskOn); else TakeOffMask(ref bMaskOn);
        */

        if (Input.GetKeyDown(KeyCode.Alpha1)) WearMask(ref rMaskOn, ref rMaskCharging, 0, rTimer);
        if (Input.GetKeyUp(KeyCode.Alpha1)) TakeOffMask(ref rMaskOn, 0);  
        
        if (Input.GetKeyDown(KeyCode.Alpha2)) WearMask(ref gMaskOn, ref gMaskCharging, 1, gTimer);
        if (Input.GetKeyUp(KeyCode.Alpha2)) TakeOffMask(ref gMaskOn, 1);  
        
        if (Input.GetKeyDown(KeyCode.Alpha3)) WearMask(ref bMaskOn, ref bMaskCharging, 2, bTimer);
        if (Input.GetKeyUp(KeyCode.Alpha3)) TakeOffMask(ref bMaskOn, 2);

        Color32 bgColor = new Color32(29, 29, 29, 255);

        if (rMaskOn)
        {
            bgColor.r = 255;
            rTimer -= Time.deltaTime;
            if (rTimer <= 0) { TakeOffMask(ref rMaskOn, 0); rMaskCharging = true; }
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
            if (gTimer <= 0) { TakeOffMask(ref gMaskOn, 1); gMaskCharging = true; }
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
            if (bTimer <= 0) { TakeOffMask(ref bMaskOn, 2); bMaskCharging = true; }
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

    void TakeOffMask(ref bool maskOn, int maskIndex)
    {
        if (!maskOn) return;

        maskOn = false;
        --masksOn;

        audioManager.PlayMaskPutOffSound();

        if (maskIndex == 0) audioManager.StopMaskRSound();
        if (maskIndex == 1) audioManager.StopMaskGSound();
        if (maskIndex == 2) audioManager.StopMaskBSound();
    }

    void WearMask(ref bool maskOn, ref bool maskCharging, int maskIndex, float timer)
    {
        if (maskOn) return;

        if (maskCharging)
        {
            audioManager.PlayErrorSound();
            return;
        }

        if (masksOn >= 2)
        {
            audioManager.PlayErrorSound();
            return;
        }
        maskOn = true;
        ++masksOn;

        audioManager.PlayMaskPutOnSound();

        if (maskIndex == 0) audioManager.PlayMaskRSound(3.0f - timer);
        if (maskIndex == 1) audioManager.PlayMaskGSound(3.0f - timer);
        if (maskIndex == 2) audioManager.PlayMaskBSound(3.0f - timer);
    }
}
