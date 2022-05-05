using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class GuitarSpawner : MonoBehaviour
{

    Transform guitar;

    Vector3 paintingPosition;
    bool travel;

    public GameObject phaseText;

    public P3dPaintSphere brush;

    public Restauration restauration;

    public PatternLogic pl;

    // Start is called before the first frame update
    void Awake()
    {
        travel = false;
        //brush.Color = new Color(173f / 255f, 173f / 255f, 173f / 255f, 1f);
        brush.Color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (!travel)
            return;

        guitar.position = Vector3.MoveTowards(guitar.position, paintingPosition, Time.deltaTime * 5f);
        if (guitar.position == paintingPosition)
        {
            travel = false;
            phaseText.SetActive(true);
            restauration.StartCleaning(guitar.gameObject);
        }
    }

    public void SpawnGuitar(GameObject guitar)
    {
        GuitarAttributes ga = guitar.GetComponent<GuitarAttributes>();

        guitar.transform.parent = null;
        guitar.transform.position = transform.position;
        //guitar.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
        guitar.transform.rotation = Quaternion.Euler(ga.cleaningRotation);
        //guitar.transform.localScale = guitar.transform.localScale * 2.5f;
        guitar.transform.localScale = guitar.transform.localScale * ga.cleaningSize;
        this.guitar = guitar.transform;
        paintingPosition = ga.cleaningPosition;
        travel = true;

        pl.EnablePatternGroup(ga.guitarType);

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
