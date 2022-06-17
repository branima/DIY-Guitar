using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelAToB : MonoBehaviour
{

    public float moveSpeed;

    Transform startPoint;
    Transform endPoint;
    Vector3 endVectorPosition;
    Vector3 endVectorRotation;
    bool travel;
    float lerpTime;

    void Awake()
    {
        travel = false;
        lerpTime = 0f;
        endVectorPosition = Vector3.zero;
        endVectorRotation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        lerpTime += Time.deltaTime * moveSpeed;

        transform.position = Vector3.Lerp(transform.position, endVectorPosition, lerpTime);
        transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, endVectorRotation, lerpTime));


        if (Vector3.Distance(transform.position, endVectorPosition) < 0.1f && Vector3.Distance(transform.rotation.eulerAngles, endVectorRotation) < 0.1f)
        {
            transform.position = endVectorPosition;
            transform.rotation = Quaternion.Euler(endVectorRotation);
            travel = false;
            Animator animator = transform.GetComponent<Animator>();
            if (transform.name.Contains("Cinela") && animator != null)
                animator.enabled = true;
        }
    }

    void TravelUniversal()
    {
        lerpTime = 0f;
        travel = true;
    }

    public void Travel(Transform endPoint)
    {
        endVectorPosition = endPoint.position;
        endVectorRotation = endPoint.rotation.eulerAngles;
        TravelUniversal();
    }

    public void Travel(Vector3 endPoint)
    {
        endVectorPosition = endPoint;
        endVectorRotation = transform.rotation.eulerAngles;
        TravelUniversal();
    }

    public void Travel(Transform endPoint, Vector3 targetVectorRotation)
    {
        endVectorPosition = endPoint.position;
        endVectorRotation = targetVectorRotation;
        TravelUniversal();
    }

    public void Travel(Transform startPoint, Transform endPoint)
    {
        transform.position = startPoint.position;
        endVectorPosition = endPoint.position;
        endVectorRotation = endPoint.rotation.eulerAngles;
        TravelUniversal();
    }

    public void Travel(Transform startPoint, Transform endPoint, Vector3 targetVectorRotation)
    {
        transform.position = startPoint.position;
        endVectorPosition = endPoint.position;
        endVectorRotation = targetVectorRotation;
        TravelUniversal();
    }
}
