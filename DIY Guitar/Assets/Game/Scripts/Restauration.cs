using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using UnityEngine.UI;
using TMPro;

public class Restauration : MonoBehaviour
{

    GameManager gameManager;

    public bool videoAdMode;

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

    int phase; /// 1 - Spraying, 2 - Pattern, 3 - Stickers, 4 - Final Decorations, 5 - Showcase

    bool patternDeployed;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GetComponent<GameManager>();
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
    }

    public void NextPhase()
    {
        //Debug.Log(activePattern);
        if (phase == 1 && activePattern != null && activePattern.nextInChain != null)
        {
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
            if (activePattern != null)
                activePattern.Travel();
            activePattern = null;
            patternDeployed = true;
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
                finalDecorPanel.SetActive(false);
                CameraSwitch.Instance.ChangeCamera();

                GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                ga.ResetBrushesAndProps();

                playingCustomerRig = gameManager.NextGuitarPlayingCustomer().GetChild(0).gameObject;
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

                StartCoroutine(NextCustomer(playingCustomerRig.transform.parent.gameObject, 4f));
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
