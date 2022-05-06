using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DipLogic : MonoBehaviour
{

    public Restauration restauration;

    public DragAndDrop dragAndDrop;

    public GameObject dipBrush;

    [SerializeField]
    private Texture dipTexture;

    public ParticleSystem starParticles;

    // Start is called before the first frame update
    void Start()
    {
        dipTexture = GetComponent<MeshRenderer>().material.mainTexture;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name + ", " + other.tag);
        if (other.name == "InDetector")
        {
            //other.GetComponentInParent<MeshRenderer>().material.mainTexture = dipTexture;
            dragAndDrop.SwitchDirection();
            other.transform.parent.GetChild(other.transform.GetSiblingIndex() + 1).gameObject.SetActive(true);
            other.gameObject.SetActive(false);
            dipBrush.SetActive(false);
        }
        else if (other.name == "OutDetector")
        {
            Destroy(dragAndDrop);
            starParticles.Play();
            Invoke("StickeringPreparation", 1f);
        }
    }

    void StickeringPreparation()
    {
        CameraSwitch.Instance.ChangeCamera();
        restauration.DipReturn();
    }
}
