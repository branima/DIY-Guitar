using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    int numberOfLevels = 2;

    public Transform customers;
    GameObject currCustomer;

    public Transform guitarPlayingCustomers;
    GameObject currGuitarPlayingCustomer;

    public Transform drumsPlayingCustomers;
    GameObject currDrumsPlayingCustomer;

    public Transform pianoPlayingCustomers;
    GameObject currPianoPlayingCustomer;

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
        if ((guitarPlayingCustomers.childCount > drumsPlayingCustomers.childCount) || (guitarPlayingCustomers.childCount > pianoPlayingCustomers.childCount))
            Destroy(guitarPlayingCustomers.GetChild(0).gameObject);
        if ((drumsPlayingCustomers.childCount > guitarPlayingCustomers.childCount) || (drumsPlayingCustomers.childCount > pianoPlayingCustomers.childCount))
            Destroy(drumsPlayingCustomers.GetChild(0).gameObject);
        if ((pianoPlayingCustomers.childCount > drumsPlayingCustomers.childCount) || (pianoPlayingCustomers.childCount > guitarPlayingCustomers.childCount))
            Destroy(pianoPlayingCustomers.GetChild(0).gameObject);

        if (customers.childCount > 0)
        {
            currCustomer = customers.GetChild(0).gameObject;
            currCustomer.transform.parent = null;
            currCustomer.SetActive(true);
            currCustomer.GetComponent<MoveForOrder>().Travel();
        }
    }

    public Transform NextGuitarPlayingCustomer()
    {
        currGuitarPlayingCustomer = guitarPlayingCustomers.GetChild(0).gameObject;
        currGuitarPlayingCustomer.transform.parent = null;
        return currGuitarPlayingCustomer.transform;
    }

    public Transform NextDrumsPlayingCustomer()
    {
        currDrumsPlayingCustomer = drumsPlayingCustomers.GetChild(0).gameObject;
        currDrumsPlayingCustomer.transform.parent = null;
        return currDrumsPlayingCustomer.transform;
    }

    public Transform NextPianoPlayingCustomer()
    {
        currPianoPlayingCustomer = pianoPlayingCustomers.GetChild(0).gameObject;
        currPianoPlayingCustomer.transform.parent = null;
        return currPianoPlayingCustomer.transform;
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
}
