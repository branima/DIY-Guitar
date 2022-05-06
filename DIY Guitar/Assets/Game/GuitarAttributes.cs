using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D.Examples;
using PaintIn3D;

public class GuitarAttributes : MonoBehaviour
{

    public Vector3 cleaningPosition;
    public Vector3 cleaningRotation;
    public float cleaningSize;

    public Vector3 dippingPosition;
    public Vector3 dippingRotation;
    public float dippingSize;

    public float fillBarScaler;
    public P3dChannelCounterFill fillScript;

    public BrushesAndPropsLogic brushesAndProps;

    public float propsAndBrushesModifier;

    public string guitarType;

    void Start()
    {
        fillScript.fillBarScaler = fillBarScaler;
        brushesAndProps.ScaleBrushesAndProps(propsAndBrushesModifier);
    }

    public void ResetBrushesAndProps()
    {
        brushesAndProps.ResetBrushesAndProps(propsAndBrushesModifier);
    }

    public Vector3 GetCleaningPosition()
    {
        return cleaningPosition;
    }

    public Vector3 GetCleaningRotation()
    {
        return cleaningRotation;
    }

    public float GetCleaningSize()
    {
        return cleaningSize;
    }

    public Vector3 GetDippingPosition()
    {
        return dippingPosition;
    }

    public Vector3 GetDippingRotation()
    {
        return dippingRotation;
    }

    public float GetDippingSize()
    {
        return dippingSize;
    }
}
