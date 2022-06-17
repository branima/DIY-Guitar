using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using TMPro;

public class AcceptCustomer : MonoBehaviour
{

    public GameObject globalProgressBarPanel;
    public GameObject instrumentSelectionPanel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            globalProgressBarPanel.SetActive(true);
            instrumentSelectionPanel.SetActive(true);
            gameObject.SetActive(false);           
        }
    }
}
