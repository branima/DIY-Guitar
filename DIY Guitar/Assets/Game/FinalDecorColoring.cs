using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDecorColoring : MonoBehaviour
{

    public Material[] colors;
    public Transform paintables;

    //public Renderer bridge;
    //public Renderer buttons;

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
            item.GetComponent<Renderer>().material = targetMat;
        }

        //bridge.material = targetMat;
        //buttons.material = targetMat;
    }
}
