using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumPartFitLogic : MonoBehaviour
{

    DrumsLogic drumsLogic;

    void Awake()
    {
        drumsLogic = FindObjectOfType<DrumsLogic>();
    }

    public void Travel(string objectType)
    {
        Transform realObject = transform.GetChild(0);
        realObject.gameObject.SetActive(true);
        realObject.parent = transform.parent;

        if (objectType == "cinela")
        {
            realObject.GetComponentInChildren<MeshRenderer>().materials = drumsLogic.GetCinelaMats();
        }
        else if (objectType == "drum")
        {
            realObject.GetComponent<MeshRenderer>().material = drumsLogic.GetDrumBaseMat();

            Material rimMat = drumsLogic.GetDrumRimMat();
            Transform rim = realObject.GetChild(0);
            foreach (Transform item in rim)
            {
                Material[] mats = item.GetComponent<MeshRenderer>().materials;
                for (int i = 0; i < mats.Length; i++)
                    mats[i] = rimMat;
                item.GetComponent<MeshRenderer>().materials = mats;
            }
        }

        TravelAToB travelScript = realObject.gameObject.AddComponent<TravelAToB>();
        travelScript.moveSpeed = 0.35f;
        travelScript.Travel(transform);
        Destroy(gameObject);
    }
}
