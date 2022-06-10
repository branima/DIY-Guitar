using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoLogic : MonoBehaviour
{

    public GameObject paintablePiano;
    public Transform paintingPosition;

    public GameObject paintingBrush;
    public GameObject spraycan;
    public GameObject paintingPanel;

    public GameObject stickerBrush;
    public GameObject stickerPanel;

    public GameObject paintableKey;

    int phaseNum;

    // Start is called before the first frame update
    void Start()
    {
        phaseNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (paintablePiano.transform.childCount == 1 && phaseNum == 1)
        {
            paintablePiano.GetComponent<MeshCollider>().enabled = true;
            TravelAToB travelScript = paintablePiano.AddComponent<TravelAToB>();
            travelScript.moveSpeed = 0.25f;
            travelScript.Travel(paintingPosition);
            CameraSwitch.Instance.ChangeCamera();
            phaseNum++;

            Invoke("EnablePainting", 1f);
        }
    }

    public void EnablePainting()
    {
        paintingPanel.SetActive(true);
        paintingBrush.SetActive(true);
        spraycan.SetActive(true);
    }

    public void EnableStickering()
    {
        CameraSwitch.Instance.ChangeCamera();

        paintingPanel.SetActive(false);
        paintingBrush.SetActive(false);
        spraycan.SetActive(false);

        stickerPanel.SetActive(true);
        stickerBrush.SetActive(true);
    }

    public void KeyPainting()
    {
        CameraSwitch.Instance.ChangeCamera();

        stickerPanel.SetActive(false);
        stickerBrush.SetActive(false);

    }
}
