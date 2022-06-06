using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DipLogic : MonoBehaviour
{

    public DragAndDrop dragAndDrop;

    public GameObject dipBrush;

    [SerializeField]
    private Texture dipTexture;

    bool dipWarping;

    // Start is called before the first frame update
    void Start()
    {
        //dipTexture = GetComponent<MeshRenderer>().material.mainTexture;
        dipTexture = GetComponent<MeshRenderer>().material.GetTexture("DipTexture");
        dipWarping = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + ", " + other.tag);
        if (other.name == "SinkLimit")
        {
            //other.GetComponentInParent<MeshRenderer>().material.mainTexture = dipTexture;
            dragAndDrop.SwitchDirection();
            other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
            other.gameObject.SetActive(false);
            dipBrush.SetActive(false);
        }
        else if (other.name == "LiftLimit")
        {
            if (dipWarping)
            {
                Destroy(dragAndDrop);
            }
            else{
                dragAndDrop.StartWarping(GetComponent<MeshRenderer>());
                dipWarping = true;
            }
        }
    }
}