using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPartsLogic : MonoBehaviour
{

    bool travel;

    public Transform awayTransform;
    public Transform ogTransform;
    Vector3 targetPosition;

    GameObject mop, brush, panel;

    void Awake()
    {
        travel = false;
    }

    void Update()
    {
        if (!travel)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 3.5f);
        if (transform.position == targetPosition)
        {
            travel = false;
            if (mop != null && brush != null && panel != null)
            {
                mop.SetActive(true);
                brush.SetActive(true);
                panel.SetActive(true);
            }
        }
    }

    public void Travel(GameObject mop, GameObject brush, GameObject panel)
    {
        this.mop = mop;
        this.brush = brush;
        this.panel = panel;

        Invoke("DelayedTravel", 0.1f);

        /*
        if (transform.position == ogTransform.position)
            targetPosition = awayTransform.position;
        else
            targetPosition = ogTransform.position;

        travel = true;
        */
    }

    void DelayedTravel()
    {
        if (transform.position == ogTransform.position)
            targetPosition = awayTransform.position;
        else
            targetPosition = ogTransform.position;

        travel = true;
    }
}
