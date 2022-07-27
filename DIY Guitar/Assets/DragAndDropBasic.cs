using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropBasic : MonoBehaviour
{

    public Animator handsAnim;
    bool handsIn;

    GameObject draggedObject;

    private Vector3 mOffset;
    private float mZCord;

    float baseHeight = -2.22f;
    float baseRotation = 65.81f;

    public GameObject[] partsToEnable;
    bool guitarsSwitched;

    void Awake()
    {
        draggedObject = gameObject;
        handsIn = false;
        guitarsSwitched = false;
    }

    void Update()
    {
        if (!guitarsSwitched && transform.position.y < -2.85f)
        {
            guitarsSwitched = true;
            foreach (GameObject item in partsToEnable)
                item.SetActive(true);
            transform.GetChild(transform.childCount - 1).gameObject.SetActive(false);
        }
    }


    void OnMouseDrag()
    {
        Vector3 move = GetMouseWorldPos() + mOffset;
        Vector3 currPos = draggedObject.transform.position;
        float x = currPos.x;
        float y = move.y;
        float z = currPos.z;
        draggedObject.transform.position = new Vector3(x, y, z);

        /*
        if (currPos.y - move.y > 0)
            transform.Rotate(0, 0, -60 * Time.deltaTime);
        else if (currPos.y - move.y < 0)
            transform.Rotate(0, 0, 60 * Time.deltaTime);
        */
        //Debug.Log(transform.rotation.x + ", " + transform.rotation.y + ", " + transform.rotation.z);

        /*
        if (currPos.y - move.y > 0)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, baseRotation - Mathf.Abs(baseHeight - transform.position.y) * 17.5f);
        else if (currPos.y - move.y < 0)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, baseRotation + Mathf.Abs(baseHeight - transform.position.y) * 17.5f);
        */

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, baseRotation - (baseHeight - transform.position.y) * 17.5f);
    }

    void OnMouseDown()
    {
        if (!handsIn)
        {
            handsAnim.SetTrigger("fadeIn");
            handsIn = true;
        }
        mZCord = Camera.main.WorldToScreenPoint(draggedObject.transform.position).z;
        mOffset = draggedObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseUp()
    {
        gameObject.AddComponent<Rigidbody>();
        handsAnim.SetTrigger("fadeOut");
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
