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
    void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit) && hit.transform == transform)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            transform.GetComponent<MeshCollider>().isTrigger = false;
            //rb.AddForce(0f, 3f, 2f, ForceMode.Impulse);
            rb.AddForce(PickBetweenTwoNumbers(Random.Range(-2.5f, -1.5f), Random.Range(1.5f, 2.5f)), Random.Range(1.5f, 2.5f), PickBetweenTwoNumbers(Random.Range(-2.5f, -1.5f), Random.Range(1.5f, 2.5f)), ForceMode.Impulse);                //flying = true;
            transform.parent = null;
        }
    }

    float PickBetweenTwoNumbers(float x, float y)
    {
        float z = Random.Range(0f, 1f);
        if (z < 0.5)
            return x;
        else
            return y;
    }
}
