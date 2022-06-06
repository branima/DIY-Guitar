using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PaintIn3D;

public class DipSelection : MonoBehaviour
{

    public Texture[] dipTextures;

    public P3dPaintDecal dipBrush;
    public Material dipWaterMat;
    public MeshRenderer dipWaterMR;

    public GameObject drum;

    public void SelectDip(int idx)
    {
        Texture selectedTexture = dipTextures[idx];
        dipWaterMR.material.SetTexture("DipTexture", selectedTexture);
        dipBrush.Texture = selectedTexture;
    }

    public void ConfirmDipPattern(GameObject panel)
    {
        drum.SetActive(true);
        panel.SetActive(false);
        CameraSwitch.Instance.ChangeCamera();
    }
}