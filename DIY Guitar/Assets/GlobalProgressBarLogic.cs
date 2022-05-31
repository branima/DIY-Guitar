using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalProgressBarLogic : MonoBehaviour
{

    List<Transform> buttons;
    int activeIdx;

    void Awake()
    {
        Reset();
    }

    void OnEnable()
    {
        Reset();
    }

    public void ShowNextStep()
    {
        buttons[activeIdx].GetChild(0).gameObject.SetActive(false);
        buttons[activeIdx++].GetChild(1).gameObject.SetActive(true);
        buttons[activeIdx].GetChild(0).gameObject.SetActive(true);
    }

    public void Reset()
    {

        buttons = new List<Transform>();
        foreach (Transform t in transform)
        {
            t.GetChild(0).gameObject.SetActive(false);
            t.GetChild(1).gameObject.SetActive(false);
            buttons.Add(t);
        }

        activeIdx = 0;
        buttons[activeIdx].GetChild(0).gameObject.SetActive(true);
    }
}
