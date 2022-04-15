using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;
using TMPro;

public class AcceptCustomer : MonoBehaviour
{

    public GuitarSpawner guitarSpawner;

    int customerNum;
    public TextMeshProUGUI levelText;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            levelText.gameObject.SetActive(false);
            CameraSwitch.Instance.ChangeCamera();
            //GameObject guitar = FindObjectOfType<GameManager>().GetCurrentCustomer().transform.GetChild(0).gameObject;
            GameObject guitar = FindObjectOfType<GameManager>().GetCurrentCustomer().GetComponentInChildren<P3dPaintable>().gameObject;
            guitarSpawner.SpawnGuitar(guitar);

            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
}
