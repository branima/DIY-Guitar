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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectDip(int idx){
        Texture selectedTexture = dipTextures[idx];
        dipWaterMR.material.mainTexture = selectedTexture;
        dipWaterMat.mainTexture = selectedTexture;
        dipBrush.Texture = selectedTexture;
    }
}
