using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDecorColoring : MonoBehaviour
{

    public Material[] colors;
    public DrumsLogic drumsLogic;

    public Transform paintables;

    void OnEnable()
    {
        if (drumsLogic != null)
            paintables = drumsLogic.GetPaintableDrum().transform.GetChild(2);
    }

    public void SetColor(string name)
    {
        Material targetMat = null;
        foreach (Material mat in colors)
        {
            if (mat.name == name)
            {
                targetMat = mat;
                break;
            }
        }

        foreach (Transform item in paintables)
        {
            //item.GetComponent<Renderer>().material = targetMat;
            Material[] mats = item.GetComponent<Renderer>().materials;
            for (int i = 0; i < mats.Length; i++)
                mats[i] = targetMat;
            item.GetComponent<Renderer>().materials = mats;
        }
    }
}
