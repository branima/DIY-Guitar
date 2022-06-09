using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumsLogic : MonoBehaviour
{

    GameManager gameManager;

    public GameObject paintableDrum;
    public GameObject paintableCinela;

    public Transform paintablePosition;

    public GameObject dipSelectionPanel;
    public GameObject rimColorSelectionPanel;
    public GameObject stickerPanel;

    public GameObject stickerBrush;

    public GameObject drumSet;
    public Transform drummingTransform;
    bool fitting;
    bool showcaseReady;

    Material[] cinelaMats;
    Material drumBaseMat;
    Material drumRimMat;

    void OnEnable()
    {
        dipSelectionPanel.SetActive(true);
        fitting = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fitting)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out hit))
        {
            DrumPartFitLogic fitScript = hit.transform.GetComponent<DrumPartFitLogic>();
            if (fitScript != null)
                fitScript.Travel(hit.transform.tag);
        }

        showcaseReady = true;
        foreach (Transform item in drumSet.transform)
        {
            if (item.tag != "Untagged")
                showcaseReady = false;
        }

        if (showcaseReady)
        {
            Invoke("Showcase", 0.75f);
            fitting = false;

        }
    }

    public void Showcase()
    {
        GlobalProgressBarLogic.Instance.gameObject.SetActive(false);
        CameraSwitch.Instance.ChangeCamera();
        GameObject playableDrummingSet = Instantiate(drumSet, drummingTransform.position, drummingTransform.rotation);
        Transform playingCustomer = gameManager.NextDrumsPlayingCustomer();
        playingCustomer.gameObject.SetActive(true);
        StartCoroutine(NextCustomer(playingCustomer.gameObject, playableDrummingSet, 5f));
    }

    public void CinelaPhase()
    {
        GlobalProgressBarLogic.Instance.ShowNextStep();
        drumBaseMat = paintableDrum.GetComponent<MeshRenderer>().material;
        drumRimMat = paintableDrum.transform.GetChild(2).GetComponentInChildren<MeshRenderer>().material;

        rimColorSelectionPanel.SetActive(false);
        stickerPanel.SetActive(true);
        stickerBrush.SetActive(true);
        Vector3 outVector = paintablePosition.position - Vector3.up * 5f;
        paintableDrum.GetComponent<TravelAToB>().Travel(outVector);
        paintableCinela.GetComponent<TravelAToB>().Travel(paintablePosition, new Vector3(-8f, 0f, -3f));
    }

    public void DrumFitting()
    {
        cinelaMats = paintableCinela.GetComponentInChildren<MeshRenderer>().materials;

        drumSet.SetActive(true);
        CameraSwitch.Instance.ChangeCamera();
        GlobalProgressBarLogic.Instance.ShowNextStep();
        stickerPanel.SetActive(false);
        stickerBrush.SetActive(false);
        fitting = true;
    }

    public Material[] GetCinelaMats()
    {
        return cinelaMats;
    }

    public Material GetDrumBaseMat()
    {
        return drumBaseMat;
    }

    public Material GetDrumRimMat()
    {
        return drumRimMat;
    }

    private IEnumerator NextCustomer(GameObject playingCustomer, GameObject drums, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        CameraSwitch.Instance.ChangeCamera();
        gameManager.GetCurrentCustomer().SetActive(false);
        gameManager.NextCustomer();
        
        playingCustomer.SetActive(false);
        drums.SetActive(false);
    }
}
