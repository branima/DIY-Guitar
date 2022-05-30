using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;
using TMPro;

public class Restauration : MonoBehaviour
{

    public bool videoAdMode;

    public Transform cameraPositions;

    public GameObject cleaningBrush;
    public GameObject paintingBrush;
    GameObject guitar;
    GameManager gameManager;

    public GameObject cleanPanel;
    public GameObject mop;
    public Image cleaningProgress;
    public ParticleSystem starParticles;

    public DipLogic dipLogic;
    public GameObject dipBox;
    public GameObject dipPanel;


    public GameObject sprayPanel;
    public GameObject sprayCan;

    public GameObject patternPanel;

    public GameObject stickerPanel;
    public GameObject stickerBrush;

    public GameObject finalDecorPanel;
    FinalPartsLogic finalParts;

    SinglePatternLogic activePattern;
    public GameObject[] patterns;

    public GameObject showcasePanel;
    GameObject playingCustomerRig;
    //public Transform guitarShowcasePosition;

    int customerNum;
    public TextMeshProUGUI levelText;

    int phase; /// 1 - Cleaning, 2 - Spraying, 3 - Pattern, 4 - Stickers, 5 - Final Decorations, 6 - Showcase

    int paintingMode; /// 0 - Spraying, 1 - Dipping

    // Start is called before the first frame update
    void Awake()
    {
        customerNum = 1;
        levelText.text = "LEVEL 1";
        phase = 0;
        gameManager = GetComponent<GameManager>();
        activePattern = null;
        paintingMode = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(phase);
        if (phase == 1)
        {
            //Debug.Log(cleaningProgress.fillAmount);
            if (cleaningProgress.fillAmount >= 1f)
            {
                cleaningBrush.SetActive(false);
                mop.SetActive(false);
                if (paintingMode == 0)
                {
                    Invoke("SprayRestTime", 1f);
                    if (gameManager.isRestauration)
                        CameraSwitch.Instance.ChangeCamera();
                }
                else if (paintingMode == 1)
                {
                    StartCoroutine(DipSelectionTransition(1f));
                }
                cleanPanel.SetActive(false);
                starParticles.Play();
                phase = 2;

                foreach (GameObject pattern in patterns)
                {
                    pattern.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                    pattern.SetActive(true);
                    pattern.SetActive(false);
                }
            }
        }
    }

    private IEnumerator DipSelectionTransition(float time)
    {
        yield return new WaitForSeconds(time);
        CameraSwitch.Instance.ChangeCamera();

        dipPanel.SetActive(true);
        dipBox.SetActive(true);
    }

    public void DipTransition()
    {
        /*
        dipPanel.SetActive(true);
        dipBox.SetActive(true);
        */
        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();

        guitar.transform.position = ga.GetDippingPosition();
        guitar.transform.rotation = Quaternion.Euler(ga.GetDippingRotation());
        guitar.transform.localScale = guitar.transform.localScale * ga.GetDippingSize();
        DragAndDrop dad = guitar.AddComponent<DragAndDrop>();
        dipLogic.dragAndDrop = dad;
    }

    public void DipReturn()
    {
        dipPanel.SetActive(false);
        dipBox.SetActive(false);
        phase = 3;
        NextPhase();

        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();

        guitar.transform.position = ga.GetCleaningPosition();
        guitar.transform.rotation = Quaternion.Euler(ga.GetCleaningRotation());
        guitar.transform.localScale = guitar.transform.localScale / ga.GetDippingSize();
        DragAndDrop dad = guitar.AddComponent<DragAndDrop>();
        dipLogic.dragAndDrop = dad;
    }

    void SprayRestTime()
    {
        sprayPanel.SetActive(true);
        cleaningBrush.GetComponent<P3dHitScreen>().ClearHitCache();
        paintingBrush.SetActive(true);
        sprayCan.SetActive(true);
    }

    public void StartCleaning(GameObject guitar, bool isDirty)
    {
        phase = 1;
        this.guitar = guitar;
        finalParts = guitar.GetComponentInChildren<FinalPartsLogic>();
        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
        paintingMode = ga.paintingMode;
        if (isDirty)
        {
            cleaningBrush.SetActive(true);
            mop.SetActive(true);
            finalParts.Travel(mop, cleaningBrush, cleanPanel);
        }
        else
        {
            finalParts.Travel(null, null, null);
            cleaningProgress.fillAmount = 1f;
        }

    }

    public void BackToSpraying(SinglePatternLogic spl)
    {
        phase--;
        patternPanel.SetActive(false);
        sprayPanel.SetActive(true);
        paintingBrush.SetActive(true);
        sprayCan.SetActive(true);
        activePattern = spl;
    }

    public void NextPhase()
    {
        if (phase == 2 && activePattern != null && activePattern.nextInChain != null)
        {
            activePattern.Travel();
            activePattern = activePattern.nextInChain;
            return;
        }

        phase++;

        if (phase == 3)
        {
            paintingBrush.SetActive(false);
            sprayPanel.SetActive(false);
            sprayCan.SetActive(false);
            patternPanel.SetActive(true);
            if (activePattern != null)
                activePattern.Travel();
            activePattern = null;

        }
        else if (phase == 4)
        {
            patternPanel.SetActive(false);
            stickerPanel.SetActive(true);

            foreach (GameObject pattern in patterns)
            {
                pattern.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                pattern.SetActive(true);
            }

        }
        else if (phase == 5)
        {
            stickerPanel.SetActive(false);
            stickerBrush.SetActive(false);
            CameraSwitch.Instance.ChangeCamera();
            FinalDecorColoring fdc = finalDecorPanel.GetComponentInChildren<FinalDecorColoring>();
            fdc.paintables = finalParts.transform.GetChild(0);
            finalDecorPanel.SetActive(true);
            finalParts.Travel(null, null, null);

            /*
            Transform customer = gameManager.GetCurrentCustomer().transform;
            Transform guitarPos = customer.GetChild(0);
            guitar.transform.position = guitarPos.position;
            guitar.transform.rotation = guitarPos.rotation;
            guitar.transform.localScale = guitar.transform.localScale / 1.5f;
            guitar.transform.parent = customer;
            */
        }
        else if (phase == 6)
        {
            if (!videoAdMode)
            {
                finalDecorPanel.SetActive(false);
                CameraSwitch.Instance.ChangeCamera();

                GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                ga.ResetBrushesAndProps();

                playingCustomerRig = gameManager.NextPlayingCustomer().GetChild(0).gameObject;
                playingCustomerRig.transform.parent.gameObject.SetActive(true);
                Transform guitarPos = playingCustomerRig.transform.GetChild(0);
                Transform customer = playingCustomerRig.transform;
                guitar.transform.position = guitarPos.position;
                guitar.transform.rotation = guitarPos.rotation;
                guitar.transform.localScale = guitar.transform.localScale / ga.cleaningSize;
                guitar.transform.parent = customer;
                GameObject customerOg = gameManager.GetCurrentCustomer();
                customerOg.SetActive(false);

                Invoke("NextCustomer", 5f);
            }
            else
            {
                finalDecorPanel.SetActive(false);
                showcasePanel.SetActive(true);

            }
        }
    }

    void NextCustomer()
    {
        foreach (GameObject pattern in patterns)
        {
            pattern.GetComponentInChildren<MeshRenderer>().enabled = true;
            pattern.SetActive(false);
        }

        CameraSwitch.Instance.ChangeCamera();
        foreach (Transform camPosition in cameraPositions)
            camPosition.gameObject.SetActive(false);

        phase = 0;
        cleaningProgress.fillAmount = 0f;
        gameManager.NextCustomer();
        customerNum++;
        levelText.text = "LEVEL " + customerNum;
        levelText.gameObject.SetActive(true);
    }
}
