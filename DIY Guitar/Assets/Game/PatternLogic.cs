using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternLogic : MonoBehaviour
{

    public Restauration restauration;

    Transform oldButton;

    public void PatternProceed(GameObject pattern)
    {
        ///Pustimo pattern na guitar
        pattern.SetActive(true);
        SinglePatternLogic spl = pattern.GetComponent<SinglePatternLogic>();
        spl.Travel();
        restauration.BackToSpraying(spl);
    }

    public void HighlightButton(Transform button)
    {
        if (oldButton != null)
        {
            oldButton.GetChild(1).gameObject.SetActive(false);
        }
        oldButton = button;
        if (oldButton.childCount > 1)
            oldButton.GetChild(1).gameObject.SetActive(true);
    }

    public void EnablePatternGroup(string name)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(true);
        foreach (Transform child in transform)
        {
            if (child.tag != name)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
