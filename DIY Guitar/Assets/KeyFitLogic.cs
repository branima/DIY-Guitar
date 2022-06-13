using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFitLogic : MonoBehaviour
{
    public void Travel()
    {
        Transform realObject = transform.GetChild(0);
        realObject.gameObject.SetActive(true);
        realObject.parent = transform.parent;

        TravelAToB travelScript = realObject.gameObject.AddComponent<TravelAToB>();
        travelScript.moveSpeed = 0.25f;
        travelScript.Travel(transform);
        Destroy(gameObject);
    }
}
