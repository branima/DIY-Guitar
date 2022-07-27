using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceShatter : MonoBehaviour
{

    float forceModifier = 1.5f;

    Rigidbody rb;
    Collider col;  

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Trigger");
        if (other.transform.name == "Floor")
        {
            //Debug.Log("Trigger2");
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(PickBetweenTwoNumbers(Random.Range(-2.5f, -1.5f), Random.Range(1.5f, 2.5f)) * forceModifier, Random.Range(1.5f, 2.5f) * forceModifier, PickBetweenTwoNumbers(Random.Range(-2.5f, -1.5f), Random.Range(1.5f, 2.5f)) * forceModifier, ForceMode.Impulse);                //flying = true;
            transform.parent = null;
            //col.isTrigger = false;
        }
    }

    float PickBetweenTwoNumbers(float x, float y)
    {
        if (Random.Range(0f, 1f) < 0.5f)
            return x;
        return y;
    }
}
