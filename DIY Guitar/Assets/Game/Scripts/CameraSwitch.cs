﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class CameraSwitch : MonoBehaviour
{

    Transform cam;

    int currActive;
    Transform currActiveTransform;

    bool reposition;
    float repoTime;

    public static CameraSwitch Instance;
    private void Awake()
    {
        Instance = this;
        cam = Camera.main.transform;
        currActive = 0;
        currActiveTransform = transform.GetChild(currActive);
        reposition = false;
        repoTime = 0f;
    }

    void OnEnable()
    {
        Instance = this;
    }

    void Update()
    {
        if (reposition)
        {
            //UnityEngine.Debug.Log("Zdravo " + (cam.position != currActiveTransform.position));
            repoTime += Time.deltaTime * 0.2f;
            if (cam.position != currActiveTransform.position)
                cam.position = Vector3.Lerp(cam.position, currActiveTransform.position, repoTime);

            if (cam.rotation != currActiveTransform.rotation)
                cam.rotation = Quaternion.Lerp(cam.rotation, currActiveTransform.rotation, repoTime);

            if (cam.position == currActiveTransform.position && cam.rotation == currActiveTransform.rotation)
                reposition = false;
        }
    }

    public void ChangeCamera()
    {
        //UnityEngine.Debug.Log(transform.name + ", " + currActive);
        //string callingFuncName = new StackFrame(1).GetMethod().Name;
        //UnityEngine.Debug.Log(callingFuncName);
        currActive++;
        if (currActive == transform.childCount)
            currActive = 0;
        //UnityEngine.Debug.Log(currActive);
        currActiveTransform = transform.GetChild(currActive);
        reposition = true;
        repoTime = 0f;
    }
}
