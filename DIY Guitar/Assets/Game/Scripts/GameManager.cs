using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public CustomerSpawn spawnScript;

    int numberOfLevels = 2;

    public Transform customers;
    GameObject currCustomer;
    GameObject currPlayingCustomer;

    public Transform guitarPlayingCustomers;
    public Transform drumsPlayingCustomers;
    public Transform pianoPlayingCustomers;

    public GameObject tapToPlayMessage;

    public GameObject guitarShapeSelectionPanel;

    public GameObject guitarSegment;
    public GameObject drumsSegment;
    public GameObject pianoSegment;

    public Transform cameraPositions;
    int cameraPositionsIdx;

    public GameObject guitarRenderCamera;
    public GameObject drumsRenderCamera;
    public GameObject pianoRenderCamera;

    public GameObject showcasePanel;
    GameObject showcasedObject;

    // Start is called before the first frame update
    void Start()
    {
        cameraPositionsIdx = 0;
        NextCustomer();
    }


    public void NextCustomer()
    {
        showcasePanel.SetActive(false);
        if (showcasedObject != null)
        {
            showcasedObject.SetActive(false);
            Destroy(showcasedObject);
        }

        guitarSegment.SetActive(false);
        drumsSegment.SetActive(false);
        pianoSegment.SetActive(false);

        currCustomer = spawnScript.SpawnNext();
        currCustomer.GetComponent<MoveForOrder>().Travel();

        guitarRenderCamera.SetActive(false);
        drumsRenderCamera.SetActive(false);
        pianoRenderCamera.SetActive(false);
    }

    public Transform NextPlayingCustomer()
    {
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

    void EnableCameraPositionsGroup(int idx)
    {
        cameraPositions.GetChild(cameraPositionsIdx).gameObject.SetActive(false);
        cameraPositionsIdx = idx;
        cameraPositions.GetChild(cameraPositionsIdx).gameObject.SetActive(true);
    }

    public void BeginGuitar()
    {
        EnableCameraPositionsGroup(0);
        guitarSegment.SetActive(true);
        CameraSwitch.Instance.ChangeCamera();
        guitarShapeSelectionPanel.SetActive(true);
    }

    public void BeginDrums()
    {
        EnableCameraPositionsGroup(1);
        drumsSegment.SetActive(true);
        CameraSwitch.Instance.ChangeCamera();
    }

    public void BeginKeyboard()
    {
        EnableCameraPositionsGroup(2);
        pianoSegment.SetActive(true);
        CameraSwitch.Instance.ChangeCamera();
    }

    public void ShowcasePanel(string instrumentName, GameObject showcasedObject)
    {
        if (showcasedObject != null)
            this.showcasedObject = showcasedObject;

        if (instrumentName == "guitar")
            guitarRenderCamera.SetActive(true);
        else if (instrumentName == "drums")
            drumsRenderCamera.SetActive(true);
        else if (instrumentName == "piano")
            pianoRenderCamera.SetActive(true);

        showcasePanel.SetActive(true);
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

    public void DestroyCurrentCustomer(string instrumentName)
    {
        if (instrumentName == "guitar")
            currPlayingCustomer = currCustomer.transform.GetChild(currCustomer.transform.childCount - 1).GetChild(0).gameObject;
        else if (instrumentName == "drums")
            currPlayingCustomer = currCustomer.transform.GetChild(currCustomer.transform.childCount - 1).GetChild(1).gameObject;
        else if (instrumentName == "keyboard")
            currPlayingCustomer = currCustomer.transform.GetChild(currCustomer.transform.childCount - 1).GetChild(2).gameObject;

        currPlayingCustomer.transform.parent = null;
        Destroy(GetCurrentCustomer());
    }
}
