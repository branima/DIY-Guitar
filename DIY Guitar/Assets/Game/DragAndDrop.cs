using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{   
    
    public GameObject guitar;

    private Vector3 mOffset;
    private float mZCord;

    bool directionDown;
    

    void Awake(){
        guitar = gameObject;
        directionDown = true;
    }

    void OnMouseDown()
    {
        mZCord = Camera.main.WorldToScreenPoint(guitar.transform.position).z;
        mOffset = guitar.transform.position - GetMouseWorldPos();
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
        Vector3 currPos = guitar.transform.position;
        float x = currPos.x;
        float y = move.y;
        float z = currPos.z;
        //Debug.Log(currPos.y - move.y);
        if((directionDown && currPos.y - move.y > 0) || (!directionDown && currPos.y - move.y < 0))
            guitar.transform.position = new Vector3(x, y, z);
    }

    public void SwitchDirection(){
        directionDown = !directionDown;
    }

    public bool isDirectionDown(){
        return directionDown;
    }
}
