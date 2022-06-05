using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkArrowIconHolder : MonoBehaviour
{
    public static int arrowsCount;
    Transform target;

    public void SetArrowIconColor(Color color)
    {
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public void SetArrowIconTextColor(Color color)
    {
        GetComponentInChildren<TextMesh>().color = color;
    }

    public void SetArrowNumber()
    {
        arrowsCount++;
        GetComponentInChildren<TextMesh>().text = arrowsCount.ToString();
        target = PlayerAvatarHolder.instance.head;
    }

    private void Update()
    {
        //transform.LookAt(target);
        transform.LookAt(new Vector3(1000f, transform.position.y, transform.position.z));
    }
}
