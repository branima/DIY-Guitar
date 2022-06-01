using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int numberOfLevels = 2;

    public Transform customers;
    GameObject currCustomer;

    public Transform playingCustomers;
    GameObject currPlayingCustomer;

    public GameObject tapToPlayMessage;

    public GameObject guitarShapeSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        NextCustomer();
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

    public void BeginGuitar()
    {
        CameraSwitch.Instance.ChangeCamera();
        guitarShapeSelectionPanel.SetActive(true);
    }

    ///TECHNICAL PART
    public void NextLevel()
    {
        LoadLevel((SceneManager.GetActiveScene().buildIndex + 1) % numberOfLevels);
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
