using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePosition : MonoBehaviour
{
    /*
    public Image pointer;
    public bool isPressed;

    public Camera cam;

    private void Start()
    {
        pointer.gameObject.SetActive(false);
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isPressed = false;
        }
        if (isPressed)
        {
            pointer.gameObject.SetActive(true);

            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 100.0f; //distance of the plane from the camera
            pointer.gameObject.transform.position = cam.ScreenToWorldPoint(screenPoint);

            //pointer.gameObject.transform.position = Input.mousePosition;
        }
        else if (!isPressed)
        {
            pointer.gameObject.SetActive(false);
        }
    }
    */
    public Image pointer;
    public Image pointerPressed;
    public bool isPressed;

    bool show;

    public Camera cam;

    void Start()
    {
        show = false;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Debug.Log("zdraavoo");
            show = !show;
        }

        if (Input.GetMouseButtonDown(0))
            isPressed = true;

        if (Input.GetMouseButtonUp(0))
            isPressed = false;

        if (isPressed && show)
        {
            pointer.gameObject.SetActive(false);
            pointerPressed.gameObject.SetActive(true);
        }
        else if (!isPressed && show)
        {
            pointer.gameObject.SetActive(true);
            pointerPressed.gameObject.SetActive(false);
        }
        else
        {
            pointer.gameObject.SetActive(false);
            pointerPressed.gameObject.SetActive(false);
        }

        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 100.0f; //distance of the plane from the camera
        pointer.gameObject.transform.position = cam.ScreenToWorldPoint(screenPoint);
        pointerPressed.gameObject.transform.position = cam.ScreenToWorldPoint(screenPoint);
    }
}