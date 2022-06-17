using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumsLogic : MonoBehaviour
{

    GameManager gameManager;

    public GameObject paintableDrum;
    GameObject paintableDrumClone;
    public GameObject paintableCinela;
    GameObject paintableCinelaClone;

    public Transform paintablePosition;

    public GameObject dipSelectionPanel;
    public GameObject rimColorSelectionPanel;
    public GameObject stickerPanel;

    public GameObject stickerBrush;

    public GameObject drumSet;
    GameObject drumSetClone;
    public Transform drummingTransform;
    bool fitting;
    bool showcaseReady;
    public ParticleSystem shineParticle;

    Material[] cinelaMats;
    Material drumBaseMat;
    Material drumRimMat;

    void OnEnable()
    {
        if (drumSetClone != null)
            Destroy(drumSetClone);
        drumSetClone = Instantiate(drumSet, drumSet.transform.position, drumSet.transform.rotation, drumSet.transform.parent);
        gameManager = FindObjectOfType<GameManager>();
        if (paintableDrumClone != null)
            Destroy(paintableDrumClone);
        paintableDrumClone = Instantiate(paintableDrum, paintableDrum.transform.position, paintableDrum.transform.rotation, paintableDrum.transform.parent);
        if (paintableCinelaClone != null)
            Destroy(paintableCinelaClone);
        paintableCinelaClone = Instantiate(paintableCinela, paintableCinela.transform.position, paintableCinela.transform.rotation, paintableCinela.transform.parent);
        dipSelectionPanel.SetActive(true);
        fitting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fitting)
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            DrumPartFitLogic fitScript = hit.transform.GetComponent<DrumPartFitLogic>();
            if (fitScript != null)
                fitScript.Travel(hit.transform.tag);
        }

        showcaseReady = true;
        foreach (Transform item in drumSetClone.transform)
        {
            if (item.tag != "Untagged")
                showcaseReady = false;
        }

        if (showcaseReady)
        {
            GlobalProgressBarLogic.Instance.gameObject.SetActive(false);
            shineParticle.Play();
            Invoke("Showcase", 1.5f);
            fitting = false;
        }
    }

    public void Showcase()
    {
        CameraSwitch.Instance.ChangeCamera();
        GameObject playableDrummingSet = Instantiate(drumSetClone, drummingTransform.position, drummingTransform.rotation);
        Transform playingCustomer = gameManager.NextDrumsPlayingCustomer();
        playingCustomer.gameObject.SetActive(true);
        StartCoroutine(NextCustomer(playingCustomer.gameObject, playableDrummingSet, 4f));
    }

    public void CinelaPhase()
    {
        GlobalProgressBarLogic.Instance.ShowNextStep();
        drumBaseMat = paintableDrumClone.GetComponent<MeshRenderer>().material;
        drumRimMat = paintableDrumClone.transform.GetChild(2).GetComponentInChildren<MeshRenderer>().material;

        rimColorSelectionPanel.SetActive(false);
        stickerPanel.SetActive(true);
        stickerBrush.SetActive(true);
        Vector3 outVector = paintablePosition.position - Vector3.up * 5f;
        paintableDrumClone.GetComponent<TravelAToB>().Travel(outVector);
        paintableCinelaClone.SetActive(true);
        //paintableCinelaClone.GetComponent<TravelAToB>().Travel(paintablePosition, new Vector3(-8f, 0f, -3f));
        paintableCinelaClone.transform.rotation = Quaternion.Euler(new Vector3(-8f, 0f, -3f));
        paintableCinelaClone.GetComponent<TravelAToB>().Travel(paintablePosition.position);
    }

    public void DrumFitting()
    {
        cinelaMats = paintableCinelaClone.GetComponentInChildren<MeshRenderer>().materials;

        drumSetClone.SetActive(true);
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

    public GameObject GetPaintableDrum()
    {
        return paintableDrumClone;
    }

    public GameObject GetPaintableCinela()
    {
        return paintableCinelaClone;
    }

    private IEnumerator NextCustomer(GameObject playingCustomer, GameObject drums, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        gameManager.ShowcasePanel("drums", drums);
        CameraSwitch.Instance.ChangeCamera();
        //gameManager.GetCurrentCustomer().SetActive(false);
        //gameManager.NextCustomer();

        Destroy(playingCustomer);
        //Destroy(paintableDrumClone);
        //Destroy(paintableCinelaClone);
        //Destroy(drums);
        Destroy(drumSetClone);
    }
}
