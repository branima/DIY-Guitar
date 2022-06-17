using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelection : MonoBehaviour
{

    int selectedIdx;

    // Start is called before the first frame update
    void Start()
    {
        selectedIdx = -1;
    }

    public void Select(int newIdx)
    {
        if (selectedIdx != -1)
            transform.GetChild(selectedIdx).GetChild(1).gameObject.SetActive(false);
        selectedIdx = newIdx;
        transform.GetChild(selectedIdx).GetChild(1).gameObject.SetActive(true);

    }
}
