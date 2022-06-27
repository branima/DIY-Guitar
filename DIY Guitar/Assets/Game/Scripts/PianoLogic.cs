using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class PianoLogic : MonoBehaviour
{

    GameManager gameManager;

    public GameObject paintablePiano;
    GameObject paintablePianoClone;
    public Transform paintingPosition;

    public GameObject paintingBrush;
    public P3dPaintSphere brushScript;
    public GameObject spraycan;
    public GameObject paintingPanel;

    public GameObject stickerBrush;
    public GameObject stickerPanel;

    public GameObject paintableKeyL;
    GameObject paintableKeyLClone;
    public Transform keyPositionL;

    public GameObject paintableKeyT;
    GameObject paintableKeyTClone;
    public Transform keyPositionT;

    public GameObject paintableKeyJ;
    GameObject paintableKeyJClone;
    public Transform keyPositionJ;

    bool readyForPainting;
    bool fitting;
    bool showcaseReady;

    public GameObject keyPaintingPanel;

    Material keyMatL;
    Material keyMatT;
    Material keyMatJ;

    public GameObject pianoShowcasePosition;

    public ParticleSystem shineParticles;

    public Transform pianoPlayingPosition;

    // Start is called before the first frame update
    void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        readyForPainting = false;
        fitting = false;

        if (paintablePianoClone != null)
            Destroy(paintablePianoClone);
        paintablePianoClone = Instantiate(paintablePiano, paintablePiano.transform.position, paintablePiano.transform.rotation, paintablePiano.transform.parent);
        paintablePianoClone.SetActive(true);

        if (paintableKeyLClone != null)
            Destroy(paintableKeyLClone);
        paintableKeyLClone = Instantiate(paintableKeyL, paintableKeyL.transform.position, paintableKeyL.transform.rotation, paintableKeyL.transform.parent);

        if (paintableKeyTClone != null)
            Destroy(paintableKeyTClone);
        paintableKeyTClone = Instantiate(paintableKeyT, paintableKeyT.transform.position, paintableKeyT.transform.rotation, paintableKeyT.transform.parent);

        if (paintableKeyJClone != null)
            Destroy(paintableKeyJClone);
        paintableKeyJClone = Instantiate(paintableKeyJ, paintableKeyJ.transform.position, paintableKeyJ.transform.rotation, paintableKeyJ.transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (paintablePianoClone.transform.childCount == 2 && !readyForPainting)
        {
            paintablePianoClone.GetComponent<MeshCollider>().enabled = true;
            TravelAToB travelScript = paintablePianoClone.AddComponent<TravelAToB>();
            travelScript.moveSpeed = 0.25f;
            travelScript.Travel(paintingPosition);
            CameraSwitch.Instance.ChangeCamera();
            readyForPainting = true;

            shineParticles.Play();
            Invoke("EnablePainting", 1f);
        }

        if (!fitting)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            KeyFitLogic fitScript = hit.transform.GetComponent<KeyFitLogic>();
            if (fitScript != null)
                fitScript.Travel();
        }

        showcaseReady = true;
        Transform keys = paintablePianoClone.transform.GetChild(1);
        foreach (Transform item in keys)
        {
            if (item.tag != "Untagged")
                showcaseReady = false;
        }

        if (showcaseReady)
        {
            shineParticles.Play();
            Invoke("Showcase", 1.5f);
            fitting = false;
        }
    }

    public void Showcase()
    {
        //Destroy(gameManager.GetCurrentCustomer());
        pianoShowcasePosition.SetActive(true);
        GlobalProgressBarLogic.Instance.gameObject.SetActive(false);
        CameraSwitch.Instance.ChangeCamera();
        Transform playingCustomer = gameManager.NextPlayingCustomer();
        playingCustomer.position = pianoPlayingPosition.position;
        playingCustomer.rotation = pianoPlayingPosition.rotation;
        playingCustomer.gameObject.SetActive(true);

        paintablePianoClone.transform.position = pianoShowcasePosition.transform.GetChild(0).position;
        paintablePianoClone.transform.rotation = pianoShowcasePosition.transform.GetChild(0).rotation;
        paintablePianoClone.transform.localScale = pianoShowcasePosition.transform.GetChild(0).localScale;

        StartCoroutine(NextCustomer(playingCustomer.gameObject, 4f));
    }

    public void EnablePainting()
    {
        brushScript.Radius = 0.35f;
        GlobalProgressBarLogic.Instance.ShowNextStep();
        paintingPanel.SetActive(true);
        paintingBrush.SetActive(true);
        spraycan.SetActive(true);
    }

    public void EnableStickering()
    {
        GlobalProgressBarLogic.Instance.ShowNextStep();
        CameraSwitch.Instance.ChangeCamera();

        paintingPanel.SetActive(false);
        paintingBrush.SetActive(false);
        spraycan.SetActive(false);

        stickerPanel.SetActive(true);
        stickerBrush.SetActive(true);
    }

    public void KeyPainting()
    {
        brushScript.Radius = 0.25f;
        GlobalProgressBarLogic.Instance.ShowNextStep();
        CameraSwitch.Instance.ChangeCamera();

        TravelAToB travelScriptPiano = paintablePianoClone.GetComponent<TravelAToB>();
        if (travelScriptPiano == null)
            travelScriptPiano = paintablePianoClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKeyL = paintableKeyLClone.GetComponent<TravelAToB>();
        if (travelScriptKeyL == null)
            travelScriptKeyL = paintableKeyLClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKeyT = paintableKeyTClone.GetComponent<TravelAToB>();
        if (travelScriptKeyT == null)
            travelScriptKeyT = paintableKeyTClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKeyJ = paintableKeyJClone.GetComponent<TravelAToB>();
        if (travelScriptKeyJ == null)
            travelScriptKeyJ = paintableKeyJClone.AddComponent<TravelAToB>();

        stickerPanel.SetActive(false);
        stickerBrush.SetActive(false);

        paintableKeyLClone.SetActive(true);
        paintableKeyTClone.SetActive(true);
        paintableKeyJClone.SetActive(true);
        keyPaintingPanel.SetActive(true);
        paintingBrush.SetActive(true);
        spraycan.SetActive(true);

        travelScriptPiano.Travel(paintablePianoClone.transform.position + Vector3.left * 3f);
        travelScriptKeyL.moveSpeed = 0.2f;
        travelScriptKeyL.Travel(keyPositionL);
        travelScriptKeyT.moveSpeed = 0.2f;
        travelScriptKeyT.Travel(keyPositionT);
        travelScriptKeyJ.moveSpeed = 0.2f;
        travelScriptKeyJ.Travel(keyPositionJ);
    }

    public void KeyFitting()
    {
        GlobalProgressBarLogic.Instance.ShowNextStep();
        fitting = true;

        keyMatL = paintableKeyLClone.GetComponent<MeshRenderer>().material;
        keyMatL.renderQueue = 3100;
        keyMatT = paintableKeyTClone.GetComponent<MeshRenderer>().material;
        keyMatT.renderQueue = 3100;
        keyMatJ = paintableKeyJClone.GetComponent<MeshRenderer>().material;
        keyMatJ.renderQueue = 3100;

        CameraSwitch.Instance.ChangeCamera();

        keyPaintingPanel.SetActive(false);
        paintingBrush.SetActive(false);
        spraycan.SetActive(false);

        paintablePianoClone.transform.GetChild(1).gameObject.SetActive(true);

        Transform keys = paintablePianoClone.transform.GetChild(1);
        foreach (Transform item in keys)
        {
            Transform key = item.GetChild(0);
            if (!key.name.Contains("black"))
            {
                if (key.name.Contains("L"))
                    key.GetComponent<MeshRenderer>().material = keyMatL;
                else if (key.name.Contains("T"))
                    key.GetComponent<MeshRenderer>().material = keyMatT;
                else
                    key.GetComponent<MeshRenderer>().material = keyMatJ;
            }
        }

        TravelAToB travelScriptPiano = paintablePianoClone.GetComponent<TravelAToB>();
        if (travelScriptPiano == null)
            travelScriptPiano = paintablePianoClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKeyL = paintableKeyLClone.GetComponent<TravelAToB>();
        if (travelScriptKeyL == null)
            travelScriptKeyL = paintableKeyLClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKeyT = paintableKeyTClone.GetComponent<TravelAToB>();
        if (travelScriptKeyT == null)
            travelScriptKeyT = paintableKeyTClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKeyJ = paintableKeyJClone.GetComponent<TravelAToB>();
        if (travelScriptKeyJ == null)
            travelScriptKeyJ = paintableKeyJClone.AddComponent<TravelAToB>();

        travelScriptPiano.Travel(paintingPosition, paintablePianoClone.transform.rotation.eulerAngles - new Vector3(0f, 90f, 0f));
        travelScriptKeyL.moveSpeed = 2f;
        travelScriptKeyT.moveSpeed = 2f;
        travelScriptKeyJ.moveSpeed = 2f;
        travelScriptKeyL.Travel(paintableKeyLClone.transform.position + Vector3.left * 3f + Vector3.down * 3f);
        travelScriptKeyT.Travel(paintableKeyTClone.transform.position + Vector3.down * 3f);
        travelScriptKeyJ.Travel(paintableKeyJClone.transform.position - Vector3.left * 3f + Vector3.down * 3f);
    }


    private IEnumerator NextCustomer(GameObject playingCustomer, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        gameManager.ShowcasePanel("piano", paintablePianoClone);
        CameraSwitch.Instance.ChangeCamera();
        //gameManager.NextCustomer();

        Destroy(playingCustomer);
        //Destroy(paintableKeyClone);
        //Destroy(paintablePianoClone);
        pianoShowcasePosition.SetActive(false);
    }

}
