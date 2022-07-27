using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;
using TMPro;

public class Restauration : MonoBehaviour
{

    GameManager gameManager;
    Camera cam;

    public bool videoAdMode;

    public bool controlledSmash;
    public GameObject breakableGuitar;
    public Transform guitarSmashPosition;

    public bool customerSmash;
    public GameObject smashingCustomer;
    public Transform holdingPosition;
    public GameObject amp;

    public bool firstPersonSmash;
    public Transform fpsCameraPos;

    public Transform cameraPositions;

    public GameObject paintingBrush;
    GameObject guitar;

    public GameObject guitarShapeSelectionPanel;

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

    public Transform guitarPlayingPosition;

    int phase; /// 1 - Spraying, 2 - Pattern, 3 - Stickers, 4 - Final Decorations, 5 - Showcase

    bool patternDeployed;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
        cam = Camera.main;
        phase = 0;
        activePattern = null;
        patternDeployed = false;
    }

    public void StartPainting(GameObject guitar)
    {
        guitarShapeSelectionPanel.SetActive(false);
        phase = 1;
        this.guitar = guitar;
        finalParts = guitar.GetComponentInChildren<FinalPartsLogic>();

        finalParts.Travel(null, null, null);

        sprayPanel.SetActive(true);
        paintingBrush.SetActive(true);
        sprayCan.SetActive(true);
        GlobalProgressBarLogic.Instance.ShowNextStep();

        foreach (GameObject pattern in patterns)
        {
            pattern.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            pattern.SetActive(true);
            pattern.SetActive(false);
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
        patternDeployed = true;
        //Debug.Log("Ime transforma sa skriptom: ");
        //Debug.Log(spl.transform.name);
    }

    public void NextPhase()
    {
        if (phase == 1 && activePattern != null && activePattern.nextInChain != null)
        {
            //Debug.Log(phase + ", " + activePattern.transform.name + ", " + activePattern.nextInChain.transform.name);
            activePattern.Travel();
            activePattern = activePattern.nextInChain;
            return;
        }

        phase++;
        if (phase != 5)
            GlobalProgressBarLogic.Instance.ShowNextStep();
        if (patternDeployed && phase == 2)
        {
            paintingBrush.SetActive(false);
            sprayPanel.SetActive(false);
            sprayCan.SetActive(false);
            if (activePattern != null)
                activePattern.Travel();
            activePattern = null;
            phase++;
        }
        if (phase == 2)
        {
            paintingBrush.SetActive(false);
            sprayPanel.SetActive(false);
            sprayCan.SetActive(false);
            patternPanel.SetActive(true);
        }
        else if (phase == 3)
        {
            patternPanel.SetActive(false);
            stickerPanel.SetActive(true);

            foreach (GameObject pattern in patterns)
            {
                pattern.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
                pattern.SetActive(true);
            }

        }
        else if (phase == 4)
        {
            stickerPanel.SetActive(false);
            stickerBrush.SetActive(false);
            CameraSwitch.Instance.ChangeCamera();
            FinalDecorColoring fdc = finalDecorPanel.GetComponentInChildren<FinalDecorColoring>();
            fdc.paintables = finalParts.transform.GetChild(0);
            finalDecorPanel.SetActive(true);
            finalParts.Travel(null, null, null);
        }
        else if (phase == 5)
        {
            GlobalProgressBarLogic.Instance.gameObject.SetActive(false);
            if (!videoAdMode)
            {
                if (controlledSmash)
                {
                    finalDecorPanel.SetActive(false);
                    CameraSwitch.Instance.ChangeCamera();

                    GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                    ga.ResetBrushesAndProps();

                    breakableGuitar.SetActive(true);
                    Transform guitarPos = guitarSmashPosition;
                    guitar.transform.parent = guitarPos.parent;
                    guitar.transform.position = guitarPos.position;
                    guitar.transform.rotation = guitarPos.rotation;
                    guitar.transform.localScale = guitar.transform.localScale / ga.cleaningSize;


                }
                else if (customerSmash)
                {
                    finalDecorPanel.SetActive(false);
                    CameraSwitch.Instance.ChangeCamera();

                    GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                    ga.ResetBrushesAndProps();

                    amp.SetActive(true);
                    smashingCustomer.SetActive(true);
                    Transform guitarPos = guitarSmashPosition;
                    guitar.transform.parent = guitarPos.parent;
                    guitar.transform.position = guitarPos.position;
                    guitar.transform.rotation = guitarPos.rotation;
                    guitar.transform.localScale = guitar.transform.localScale / ga.cleaningSize;
                }
                else if (firstPersonSmash)
                {
                    finalDecorPanel.SetActive(false);
                    Debug.Log("tu sam");
                    cam.transform.parent = fpsCameraPos.parent;
                    cam.transform.position = fpsCameraPos.position;
                    cam.transform.rotation = fpsCameraPos.rotation;
                    Debug.Log("jedem govna");

                    GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                    ga.ResetBrushesAndProps();

                    amp.SetActive(true);
                    smashingCustomer.SetActive(true);
                    Transform guitarPos = guitarSmashPosition;
                    guitar.transform.parent = guitarPos.parent;
                    guitar.transform.position = guitarPos.position;
                    guitar.transform.rotation = guitarPos.rotation;
                    guitar.transform.localScale = guitar.transform.localScale / ga.cleaningSize;
                }
                else
                {
                    finalDecorPanel.SetActive(false);
                    CameraSwitch.Instance.ChangeCamera();


                    GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                    ga.ResetBrushesAndProps();

                    playingCustomerRig = gameManager.NextPlayingCustomer().GetChild(0).gameObject;
                    playingCustomerRig.transform.parent.position = guitarPlayingPosition.position;
                    playingCustomerRig.transform.parent.gameObject.SetActive(true);
                    //Debug.Log(playingCustomerRig.name);
                    Transform guitarPos = playingCustomerRig.transform.GetChild(0);
                    Transform customer = playingCustomerRig.transform;
                    guitar.transform.position = guitarPos.position;
                    guitar.transform.rotation = guitarPos.rotation;
                    guitar.transform.localScale = guitar.transform.localScale / ga.cleaningSize;
                    guitar.transform.parent = customer;
                    //GameObject customerOg = gameManager.GetCurrentCustomer();
                    //customerOg.SetActive(false);

                    //StartCoroutine(NextCustomer(playingCustomerRig.transform.parent.gameObject, 4f));
                }
            }
            else
            {
                finalDecorPanel.SetActive(false);
                showcasePanel.SetActive(true);
            }
        }
    }

    private IEnumerator NextCustomer(GameObject playingCustomer, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        guitar.transform.parent = null;

        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();

        guitar.transform.position = ga.GetCleaningPosition();
        guitar.transform.rotation = Quaternion.Euler(ga.GetCleaningRotation());
        guitar.transform.localScale = guitar.transform.localScale * ga.cleaningSize;

        Destroy(playingCustomer);
        foreach (GameObject pattern in patterns)
        {
            pattern.GetComponentInChildren<MeshRenderer>().enabled = true;
            pattern.SetActive(false);
        }

        gameManager.ShowcasePanel("guitar", guitar);
        CameraSwitch.Instance.ChangeCamera();

        phase = 0;
        //gameManager.NextCustomer();
        patternDeployed = false;
    }
}
