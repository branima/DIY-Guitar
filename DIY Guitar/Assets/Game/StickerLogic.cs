using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PaintIn3D;

public class StickerLogic : MonoBehaviour
{

    public P3dPaintDecal brush;

    Transform oldButton;

    public void SetSticker(Image sticker)
    {
        //Debug.Log(sticker.mainTexture.name);
        brush.Texture = sticker.mainTexture;
        if (!brush.gameObject.activeSelf)
            brush.gameObject.SetActive(true);
    }

    public void HighlightButton(Transform button)
    {
        if (oldButton != null)
        {
            oldButton.GetChild(1).gameObject.SetActive(false);
        }
        oldButton = button;
        oldButton.GetChild(1).gameObject.SetActive(true);
    }
}
