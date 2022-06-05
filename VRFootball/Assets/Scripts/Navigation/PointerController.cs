using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour
{
    public Transform player;
    [SerializeField]
    Color[] pointerColors;
    [SerializeField]
    GameObject laser;
    [SerializeField]
    GameObject point;
    [SerializeField]
    Material laserMat;

    Vector3 origin = new Vector3(0, -1, 0);

    public static RaycastHit hit;
    

    bool active;
    
    void Start()
    {
        ControllerManager.onTouchClicked += ActivatePointer;
    }

    void ActivatePointer(bool clicked)
    {
        laser.SetActive(clicked);
        point.SetActive(clicked);

        active = clicked;

        if (!clicked)
        {
            Teleport();
        }
    }

    void Update()
    {
        if(active)
        {
            hit = new RaycastHit();

            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                point.transform.position = hit.point;
            }
        }
        
    }

    void Teleport()
    {
        player.position = hit.point;
    }
}
