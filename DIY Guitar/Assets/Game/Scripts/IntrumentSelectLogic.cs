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
            Invoke("DestroyCustomer", 0.5f);
        }
        else if (name == "drums")
        {
            gameManager.BeginDrums();
            gameObject.SetActive(false);
            Invoke("DestroyCustomer", 0.5f);
        }
        else if (name == "keyboard")
        {
            gameManager.BeginKeyboard();
            gameObject.SetActive(false);
            Invoke("DestroyCustomer", 0.5f);
        }
        else
        {
            Debug.Log("There is no instrument called " + name + ".");
        }
    }

    void DestroyCustomer()
    {
        gameManager.DestroyCurrentCustomer();
    }
}
