using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoLogic : MonoBehaviour
{

    GameManager gameManager;

    public GameObject paintablePiano;
    GameObject paintablePianoClone;
    public Transform paintingPosition;

    public GameObject paintingBrush;
    public GameObject spraycan;
    public GameObject paintingPanel;

    public GameObject stickerBrush;
    public GameObject stickerPanel;

    public GameObject paintableKey;
    GameObject paintableKeyClone;
    public Transform keyPosition;

    bool readyForPainting;
    bool fitting;
    bool showcaseReady;

    public GameObject keyPaintingPanel;

    Material keyMat;

    public GameObject pianoShowcasePosition;

    // Start is called before the first frame update
    void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        readyForPainting = false;
        fitting = false;
        paintablePianoClone = Instantiate(paintablePiano, paintablePiano.transform.position, paintablePiano.transform.rotation, paintablePiano.transform.parent);
        paintablePianoClone.SetActive(true);
        paintableKeyClone = Instantiate(paintableKey, paintableKey.transform.position, paintableKey.transform.rotation, paintableKey.transform.parent);
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
            Invoke("Showcase", 1.5f);
            fitting = false;
        }
    }

    public void Showcase()
    {
        Destroy(gameManager.GetCurrentCustomer());
        pianoShowcasePosition.SetActive(true);
        GlobalProgressBarLogic.Instance.gameObject.SetActive(false);
        CameraSwitch.Instance.ChangeCamera();
        Transform playingCustomer = gameManager.NextPianoPlayingCustomer();
        playingCustomer.gameObject.SetActive(true);

        paintablePianoClone.transform.position = pianoShowcasePosition.transform.GetChild(0).position;
        paintablePianoClone.transform.rotation = pianoShowcasePosition.transform.GetChild(0).rotation;
        paintablePianoClone.transform.localScale = pianoShowcasePosition.transform.GetChild(0).localScale;

        StartCoroutine(NextCustomer(playingCustomer.gameObject, 3.5f));
    }

    public void EnablePainting()
    {
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
        GlobalProgressBarLogic.Instance.ShowNextStep();
        CameraSwitch.Instance.ChangeCamera();

        TravelAToB travelScriptPiano = paintablePianoClone.GetComponent<TravelAToB>();
        if (travelScriptPiano == null)
            travelScriptPiano = paintablePianoClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKey = paintableKeyClone.GetComponent<TravelAToB>();
        if (travelScriptKey == null)
            travelScriptKey = paintableKeyClone.AddComponent<TravelAToB>();

        stickerPanel.SetActive(false);
        stickerBrush.SetActive(false);

        paintableKeyClone.SetActive(true);
        keyPaintingPanel.SetActive(true);
        paintingBrush.SetActive(true);
        spraycan.SetActive(true);

        travelScriptPiano.Travel(paintablePianoClone.transform.position + Vector3.left * 3f);
        travelScriptKey.moveSpeed = 0.2f;
        travelScriptKey.Travel(keyPosition);
    }

    public void KeyFitting()
    {
        GlobalProgressBarLogic.Instance.ShowNextStep();
        fitting = true;
        keyMat = paintableKeyClone.GetComponent<MeshRenderer>().material;
        keyMat.renderQueue = 3100;

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
                key.GetComponent<MeshRenderer>().material = keyMat;
        }

        TravelAToB travelScriptPiano = paintablePianoClone.GetComponent<TravelAToB>();
        if (travelScriptPiano == null)
            travelScriptPiano = paintablePianoClone.AddComponent<TravelAToB>();

        TravelAToB travelScriptKey = paintableKeyClone.GetComponent<TravelAToB>();
        if (travelScriptKey == null)
            travelScriptKey = paintableKeyClone.AddComponent<TravelAToB>();

        travelScriptPiano.Travel(paintingPosition, paintablePianoClone.transform.rotation.eulerAngles - new Vector3(0f, 90f, 0f));
        travelScriptKey.moveSpeed = 2f;
        travelScriptKey.Travel(paintableKeyClone.transform.position + Vector3.left * 3f);
    }


    private IEnumerator NextCustomer(GameObject playingCustomer, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        gameManager.ShowcasePanel("piano", paintablePianoClone);
        CameraSwitch.Instance.ChangeCamera();
        //gameManager.NextCustomer();

        Destroy(playingCustomer);
        Destroy(paintableKeyClone);
        //Destroy(paintablePianoClone);
        pianoShowcasePosition.SetActive(false);
    }

}
