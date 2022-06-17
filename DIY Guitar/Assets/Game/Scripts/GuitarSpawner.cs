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
    GameObject[] guitarClones;
    int currIdx;

    public GameObject nextButton;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        guitarClones = new GameObject[guitars.Length];
        ResetClones();
        currIdx = 0;
    }

    void OnEnable()
    {
        //ResetClones();
    }

    public void SelectGuitar(int idx)
    {
        guitarClones[currIdx].SetActive(false);
        currIdx = idx;
        guitarClones[currIdx].SetActive(true);
        if (!nextButton.activeSelf)
            nextButton.SetActive(true);
    }

    public void ConfirmSelection()
    {
        SpawnGuitar(guitarClones[currIdx]);

        GameObject guitar = guitars[currIdx];
        guitarClones[currIdx] = Instantiate(guitar, guitar.transform.position, guitar.transform.rotation, guitar.transform.parent);
    }

    public void SpawnGuitar(GameObject guitar)
    {
        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
        this.guitar = guitar.transform;
        patternLogic.EnablePatternGroup(ga.guitarType);
        ga.ScaleBrushesAndProps();
        restauration.StartPainting(guitar.gameObject);
    }

    public void ResetClones()
    {
        for (int i = 0; i < guitars.Length; i++)
        {
            if (guitarClones[i] == null)
            {
                GameObject guitar = guitars[i];
                GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
                guitar.transform.position = ga.GetCleaningPosition();
                guitar.transform.rotation = Quaternion.Euler(ga.cleaningRotation);
                guitar.transform.localScale = guitar.transform.localScale * ga.cleaningSize;
                guitarClones[i] = Instantiate(guitar, guitar.transform.position, guitar.transform.rotation, guitar.transform.parent);
            }
        }
    }
}
