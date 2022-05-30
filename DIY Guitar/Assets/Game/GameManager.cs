using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int numberOfLevels = 2;

    public bool isRestauration;

    public Transform customers;
    GameObject currCustomer;

    public Transform playingCustomers;
    GameObject currPlayingCustomer;

    public GameObject tapToPlayMessage;

    public GuitarSpawner guitarSpawner;

    ///GUITARS FROM SCRATCH SYSTEM
    public GameObject shapeSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (isRestauration)
            NextCustomer();
        else
        {
            CameraSwitch.Instance.ChangeCamera();
            CameraSwitch.Instance.ChangeCamera();

            shapeSelectionPanel.SetActive(true);
        }

        ///AD-ONLY PART
        /*
        GameObject guitar = GetCurrentCustomer().transform.GetChild(0).gameObject;
        guitar.transform.parent.gameObject.SetActive(false);
        guitarSpawner.SpawnGuitar(guitar);
        */
    }

    public void NextCustomer()
    {
        if (currPlayingCustomer != null)
            currPlayingCustomer.SetActive(false);

        if (customers.childCount > 0)
        {
            currCustomer = customers.GetChild(0).gameObject;
            currCustomer.transform.parent = null;
            currCustomer.SetActive(true);
            currCustomer.GetComponent<MoveForOrder>().Travel();
        }
    }

    public Transform NextPlayingCustomer()
    {
        currPlayingCustomer = playingCustomers.GetChild(0).gameObject;
        currPlayingCustomer.transform.parent = null;
        return currPlayingCustomer.transform;
    }

    public void EnableTapToPlayMessage()
    {
        tapToPlayMessage.SetActive(true);
    }

    public GameObject GetCurrentCustomer()
    {
        return currCustomer;
    }

    public void NextLevel()
    {
        LoadLevel((SceneManager.GetActiveScene().buildIndex + 1) % numberOfLevels);
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
