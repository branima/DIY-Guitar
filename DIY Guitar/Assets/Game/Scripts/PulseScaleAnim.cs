using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScaleAnim : MonoBehaviour
{

    public float speed;
    public float scaleFactor;

    Vector3 startVector;
    Vector3 endVector;

    float lerpTime;

    void Awake()
    {
        startVector = new Vector3(transform.localScale.x * scaleFactor, transform.localScale.y * scaleFactor, transform.localScale.z * scaleFactor);
        endVector = new Vector3(transform.localScale.x / scaleFactor, transform.localScale.y / scaleFactor, transform.localScale.z / scaleFactor);
        lerpTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        lerpTime += Time.deltaTime * speed;
        transform.localScale = Vector3.MoveTowards(transform.localScale, endVector, Time.deltaTime * speed);
        if (Vector3.Distance(transform.localScale, endVector) < 0.01f)
        {
            lerpTime = 0f;
            Vector3 temp = endVector;
            endVector = startVector;
            startVector = temp;
        }
    }
}
