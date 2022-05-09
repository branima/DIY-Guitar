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

    public GameObject dipInstructionsDown;
    public Restauration restauration;

    public void SelectDip(int idx)
    {
        Texture selectedTexture = dipTextures[idx];
        //dipWaterMR.material.mainTexture = selectedTexture;
        //dipWaterMat.mainTexture = selectedTexture;
        dipWaterMR.material.SetTexture("DipTexture", selectedTexture);
        //dipWaterMat.mainTexture = selectedTexture;
        dipBrush.Texture = selectedTexture;
    }

    public void ConfirmDipPattern(GameObject scrollview)
    {
        Invoke("ShowInstructions", 1f);
        //dipInstructionsDown.SetActive(true);
        scrollview.SetActive(false);
        CameraSwitch.Instance.ChangeCamera();
        restauration.DipTransition();
    }

    private void ShowInstructions()
    {
        dipInstructionsDown.SetActive(true);
    }
}
