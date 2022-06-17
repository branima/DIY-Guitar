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

    public DrumsLogic drumsLogic;
    GameObject drum;

    void OnEnable()
    {
        SelectDip(0);
        drum = drumsLogic.GetPaintableDrum();
    }

    public void SelectDip(int idx)
    {
        Texture selectedTexture = dipTextures[idx];
        dipWaterMR.material.SetTexture("DipTexture", selectedTexture);
        dipBrush.Texture = selectedTexture;
    }

    public void ConfirmDipPattern(GameObject panel)
    {
        GlobalProgressBarLogic.Instance.ShowNextStep();
        drum.SetActive(true);
        panel.SetActive(false);
        CameraSwitch.Instance.ChangeCamera();
    }
}