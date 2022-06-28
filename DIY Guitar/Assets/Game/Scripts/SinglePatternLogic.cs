using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class SinglePatternLogic : MonoBehaviour
{

    bool travel;
    Vector3 ogPosition;
    public Transform finalTransform;
    Vector3 finalPosition;

    Vector3 targetPosition;

    public SinglePatternLogic nextInChain;

    public GameObject peel;

    // Start is called before the first frame update
    void Awake()
    {
        //Debug.Log("Zdravoo " + transform.name);
        travel = false;
        ogPosition = transform.position;
        targetPosition = transform.position;
        finalPosition = finalTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5f);
        if (transform.position == targetPosition)
        {
            travel = false;
            if (transform.position != finalPosition && nextInChain != null)
            {
                nextInChain.gameObject.SetActive(true);
                nextInChain.Travel();
                nextInChain = null;
            }
            if (transform.position == ogPosition)
            {
                GetComponentInChildren<P3dPaintableTexture>().Clear();
                gameObject.SetActive(false);
            }
        }
    }

    public void Travel()
    {
        //Debug.Log(transform.name);
        //Debug.Log(peel != null && !peel.activeSelf && transform.position == ogPosition);
        if (peel != null && !peel.activeSelf && transform.position == ogPosition)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            targetPosition = finalPosition;
            transform.position = finalPosition;
            peel.SetActive(true);
        }
        else
        {

            if (targetPosition == ogPosition)
            {
                //Debug.Log("Putujem 1");
                targetPosition = finalPosition;
            }
            else
            {
                //Debug.Log("Putujem 2");
                targetPosition = ogPosition;
            }
            travel = true;
        }
    }
}
