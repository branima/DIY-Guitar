using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class GuitarSpawner : MonoBehaviour
{

    GameManager gameManager;

    Transform guitar;

    public Restauration restauration;

    public PatternLogic patternLogic;

    public GameObject[] guitars;
    int currIdx;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();

        foreach (GameObject guitar in guitars)
        {
            GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
            guitar.transform.position = ga.GetCleaningPosition();
            guitar.transform.rotation = Quaternion.Euler(ga.cleaningRotation);
            guitar.transform.localScale = guitar.transform.localScale * ga.cleaningSize;
        }
        currIdx = 0;
    }    

    public void SelectGuitar(int idx)
    {
        guitars[currIdx].SetActive(false);
        currIdx = idx;
        guitars[currIdx].SetActive(true);
    }

    public void ConfirmSelection()
    {
        SpawnGuitar(guitars[currIdx]);
    }

    public void SpawnGuitar(GameObject guitar)
    {
        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
        this.guitar = guitar.transform;
        patternLogic.EnablePatternGroup(ga.guitarType);
        ga.ScaleBrushesAndProps();
        restauration.StartPainting(guitar.gameObject);
    }
}
