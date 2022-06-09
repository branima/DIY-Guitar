using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterOnTouch : MonoBehaviour
{

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(1f, 1f, 1f, ForceMode.Impulse);
        }
    }
}
