using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DipLogic : MonoBehaviour
{

    //public DragAndDrop dragAndDrop;
    public DrumsLogic drumsLogic;
    HoldAndDip holdAndDip;

    public GameObject dipBrush;

    [SerializeField]
    private Texture dipTexture;

    bool dipWarping;

    // Start is called before the first frame update
    void OnEnable()
    {
        //dipTexture = GetComponent<MeshRenderer>().material.mainTexture;
        dipTexture = GetComponent<MeshRenderer>().material.GetTexture("DipTexture");
        dipWarping = false;
        holdAndDip = drumsLogic.GetPaintableDrum().GetComponent<HoldAndDip>();
        dipBrush.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + ", " + other.tag);
        if (other.name == "SinkLimit")
        {
            holdAndDip.DipOut();
            other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
            other.gameObject.SetActive(false);
            dipBrush.SetActive(false);
        }
        else if (other.name == "LiftLimit")
        {

            holdAndDip.StartWarping(GetComponent<MeshRenderer>());
            dipWarping = true;
        }
    }
}