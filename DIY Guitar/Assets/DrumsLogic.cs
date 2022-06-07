using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumsLogic : MonoBehaviour
{


    public GameObject paintableDrum;
    public GameObject paintableCinela;

    public Transform paintablePosition;

    public GameObject dipSelectionPanel;
    public GameObject rimColorSelectionPanel;
    public GameObject stickerPanel;

    void OnEnable()
    {
        dipSelectionPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CinelaPhase()
    {
        rimColorSelectionPanel.SetActive(false);
        stickerPanel.SetActive(true);
        Vector3 outVector = paintablePosition.position - Vector3.up * 5f;
        paintableDrum.GetComponent<TravelAToB>().Travel(outVector);
        paintableCinela.GetComponent<TravelAToB>().Travel(paintablePosition);
    }
}
