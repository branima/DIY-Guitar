using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntrumentSelectLogic : MonoBehaviour
{

    GameManager gameManager;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SelectInstrument(string name)
    {
        if (name == "guitar")
        {
            gameManager.BeginGuitar();
            gameObject.SetActive(false);
        }
        else if (name == "drums")
        {
            gameObject.SetActive(false);
        }
        else if (name == "keyboard")
        {
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("There is no instrument called " + name + ".");
        }
    }
}
