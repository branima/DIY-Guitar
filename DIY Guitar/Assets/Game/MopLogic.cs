using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MopLogic : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetMouseButton(0))
        {
            var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(Ray, out hit) && hit.transform.tag == "guitar")
                transform.position = hit.point + Vector3.up * 0.1f;
        }
        */
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if (Physics.Raycast(Ray, out hit) && hit.transform.tag == "guitar")
        if (Physics.Raycast(Ray, out hit))
            //transform.position = hit.point + Vector3.up * 0.25f;
            transform.position = new Vector3(hit.point.x, -1.45f, hit.point.z);
    }
}
