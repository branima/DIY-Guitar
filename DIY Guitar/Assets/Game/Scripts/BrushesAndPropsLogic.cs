using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class BrushesAndPropsLogic : MonoBehaviour
{

    public P3dPaintDecal[] decalBrushes;
    public P3dPaintSphere[] sphereBrushes;
    public Transform[] props;

    public void ScaleBrushesAndProps(float modifier)
    {
        /*
        foreach (P3dPaintDecal brush in decalBrushes)
            brush.Radius = brush.Radius * modifier;
        */
        foreach (P3dPaintSphere brush in sphereBrushes)
            brush.Radius = brush.Radius * modifier;

        foreach (Transform prop in props)
            prop.localScale = prop.localScale * modifier;
    }

    public void ResetBrushesAndProps(float modifier)
    {
        /*
        foreach (P3dPaintDecal brush in decalBrushes)
            brush.Radius = brush.Radius / modifier;
        */

        foreach (P3dPaintSphere brush in sphereBrushes)
            brush.Radius = brush.Radius / modifier;

        foreach (Transform prop in props)
            prop.localScale = prop.localScale / modifier;
    }
}
