using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpraycanLogic : MonoBehaviour
{

    public ParticleSystem ps;

    // Update is called once per frame
    void Update()
    {
        var Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(Ray, out hit))
        {
            transform.position = new Vector3(hit.point.x, -1.05f, hit.point.z);
            //Debug.Log(hit.transform.tag);

            if (Input.GetMouseButton(0))
            {
                if (!ps.isPlaying)
                    ps.Play();
            }
            else if (ps.isPlaying)
                ps.Stop();

        }
    }

    public void ChangeColor(Material mat)
    {
        transform.GetChild(0).GetComponent<Renderer>().materials[1].color = mat.color;
    }
}
