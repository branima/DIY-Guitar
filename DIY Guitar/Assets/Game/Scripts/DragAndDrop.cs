using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    
    GameObject draggedObject;

    private Vector3 mOffset;
    private float mZCord;

    bool directionDown;

    MeshRenderer dipMeshRenderer;
    Material dipMat;
    bool warp;

    void Awake(){
        draggedObject = gameObject;
        directionDown = true;
        warp = false;
    }

    void OnMouseDown()
    {
        mZCord = Camera.main.WorldToScreenPoint(draggedObject.transform.position).z;
        mOffset = draggedObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCord;
        
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 move = GetMouseWorldPos() + mOffset;
        Vector3 currPos = draggedObject.transform.position;
        float x = currPos.x;
        float y = move.y;
        float z = currPos.z;
        //Debug.Log(currPos.y - move.y);
        if((directionDown && currPos.y - move.y > 0) || (!directionDown && currPos.y - move.y < 0)){
            draggedObject.transform.position = new Vector3(x, y, z);
            if(warp){
                float distance = Mathf.Abs(currPos.y - move.y);
                float currAmmount = dipMat.GetFloat("WarpAmount");
                dipMat.SetFloat("WarpAmount", currAmmount + distance);
            }
        }
    }

    public void StartWarping(MeshRenderer dipMeshRenderer){ 
        this.dipMeshRenderer = dipMeshRenderer;
        dipMat = dipMeshRenderer.material;
        warp = true;
    }

    public void SwitchDirection(){
        directionDown = !directionDown;
    }
}
