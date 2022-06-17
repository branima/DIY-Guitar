using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForOrder : MonoBehaviour
{

    public Transform targetPosition;
    bool travel;

    GameManager gameManager;

    public Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        travel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, Time.deltaTime * 2.5f);
        transform.LookAt(targetPosition);
        if (transform.position == targetPosition.position)
        {
            if (animator != null)
                animator.SetTrigger("stopTrigger");
            travel = false;
            transform.rotation = targetPosition.rotation;
            gameManager.EnableTapToPlayMessage();
        }
    }

    public void Travel()
    {
        travel = true;
        if (animator != null)
            animator.SetTrigger("moveTrigger");
    }
}
