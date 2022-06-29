using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeelLogic : MonoBehaviour
{

    public GameObject realPattern;

    public void EnableRealPattern()
    {
        realPattern.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PeelOutLogic(){
        gameObject.SetActive(false);
    }
}
