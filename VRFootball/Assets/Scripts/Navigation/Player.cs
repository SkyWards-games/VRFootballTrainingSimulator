using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

    public static void SetPosition(Vector3 pos)
    {
        instance.transform.position = pos;
    }
}
