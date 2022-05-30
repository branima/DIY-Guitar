using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class GuitarSpawner : MonoBehaviour
{

    GameManager gameManager;

    Transform guitar;

    Vector3 paintingPosition;
    bool travel;

    public P3dPaintSphere brush;

    public Restauration restauration;

    public PatternLogic pl;

    public float repositionSpeed;

    public GameObject[] guitars;
    int currIdx;
    public GameObject shapeSelectPanel;

    // Start is called before the first frame update
    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        travel = false;
        //brush.Color = new Color(173f / 255f, 173f / 255f, 173f / 255f, 1f);
        brush.Color = Color.black;

        foreach (GameObject guitar in guitars)
        {
            GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();
            guitar.transform.position = ga.GetCleaningPosition();
            guitar.transform.rotation = Quaternion.Euler(ga.cleaningRotation);
            guitar.transform.localScale = guitar.transform.localScale * ga.cleaningSize;
        }
        currIdx = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        guitar.position = Vector3.MoveTowards(guitar.position, paintingPosition, Time.deltaTime * repositionSpeed);
        if (guitar.position == paintingPosition)
        {
            travel = false;
            //phaseText.SetActive(true);
            restauration.StartCleaning(guitar.gameObject, gameManager.isRestauration);
        }
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

    void Reposition(GameObject guitar, GuitarAttributes ga)
    {
        guitar.transform.parent = null;
        guitar.transform.position = transform.position;
        //guitar.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        guitar.transform.rotation = Quaternion.Euler(ga.cleaningRotation);
        //guitar.transform.localScale = guitar.transform.localScale * 2.5f;
        guitar.transform.localScale = guitar.transform.localScale * ga.cleaningSize;
    }

    public void SpawnGuitar(GameObject guitar)
    {
        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();

        if (gameManager.isRestauration)
            Reposition(guitar, ga);

        this.guitar = guitar.transform;
        paintingPosition = ga.cleaningPosition;
        pl.EnablePatternGroup(ga.guitarType);
        ga.ScaleFillBar();
        ga.ScaleBrushesAndProps();
        travel = true;

        ///AD-ONLY UPDATES
        /*
        guitar.transform.parent = null;
        guitar.transform.position = transform.position;
        guitar.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        guitar.transform.localScale = guitar.transform.localScale * 2.5f;
        this.guitar = guitar.transform;

        this.guitar.position = paintingPosition.position;
        restauration.StartCleaning(guitar.gameObject);
        */
    }
}
