using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAndDip : MonoBehaviour
{

    public float descendSpeed;
    bool travel;

    MeshRenderer dipMeshRenderer;
    Material dipMat;
    bool warp;

    Animator animator;

    public Transform paintingTransform;
    public GameObject rimColorSelectionPanel;

    void Awake()
    {
        travel = true;
        warp = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (travel && Input.GetMouseButton(0))
        {
            transform.position = transform.position - Vector3.up * Time.deltaTime * descendSpeed;
            if (warp)
            {
                float currAmmount = dipMat.GetFloat("WarpAmount");
                dipMat.SetFloat("WarpAmount", currAmmount + Time.deltaTime * descendSpeed);
            }
        }
    }

    public void StartWarping(MeshRenderer dipMeshRenderer)
    {
        this.dipMeshRenderer = dipMeshRenderer;
        dipMat = dipMeshRenderer.material;
        warp = true;
    }

    public void DipOut()
    {
        CameraSwitch.Instance.ChangeCamera();
        travel = false;
        animator.enabled = true;
        Invoke("NextPhase", 3f);
    }

    void NextPhase()
    {        
        animator.enabled = false;
        CameraSwitch.Instance.ChangeCamera();
        GlobalProgressBarLogic.Instance.ShowNextStep();
    }

    void OnBecameInvisible()
    {
        //Debug.Log("Ben");
        transform.position = paintingTransform.position;
        transform.rotation = paintingTransform.rotation;
        rimColorSelectionPanel.SetActive(true);
        Destroy(this);
    }

}
