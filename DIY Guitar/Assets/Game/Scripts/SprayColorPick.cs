using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class SprayColorPick : MonoBehaviour
{

    public P3dPaintDecal brush;
    public P3dPaintSphere brushSphere;
    public Material[] colors;
    public SpraycanLogic spraycanLogic;

    public Material sprayParticleColor;

    Transform oldButton;

    // Start is called before the first frame update
    void Start()
    {
        string name = "Col2";
        Material targetMat = null;
        foreach (Material mat in colors)
        {
            if (mat.name == name)
            {
                targetMat = mat;
                break;
            }
        }
        if (brush != null)
            brush.Color = targetMat.color;
        else
            brushSphere.Color = targetMat.color;
        spraycanLogic.ChangeColor(targetMat);
    }

    public void SetColor(Transform button)
    {
        if (oldButton != null)
        {
            oldButton.GetChild(0).gameObject.SetActive(false);
        }
        oldButton = button;
        oldButton.GetChild(0).gameObject.SetActive(true);

        string name = button.name;
        Material targetMat = null;
        foreach (Material mat in colors)
        {
            if (mat.name == name)
            {
                targetMat = mat;
                break;
            }
        }
        if (brush != null)
            brush.Color = targetMat.color;
        else
            brushSphere.Color = targetMat.color;

        Color newColor = new Color(targetMat.color.r, targetMat.color.g, targetMat.color.b, 0.35f);
        sprayParticleColor.color = newColor;
        spraycanLogic.ChangeColor(targetMat);
    }
}
