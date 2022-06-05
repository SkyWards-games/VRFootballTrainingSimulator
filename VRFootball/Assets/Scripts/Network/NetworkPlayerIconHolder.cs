using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayerIconHolder : MonoBehaviour
{
    public void SetPlayerIconColor(Color color)
    {
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }

    public void SetPlayerIconTextColor(Color color)
    {
        GetComponentInChildren<TextMesh>().color = color;
    }

    public void SetPlayerNumber(string number)
    {
        GetComponentInChildren<TextMesh>().text = number;
    }
}
