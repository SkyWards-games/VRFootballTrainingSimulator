using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatarHolder : MonoBehaviour
{
    public Transform head;
    public Transform body;
    public Transform target;
    public Transform relative;

    public static PlayerAvatarHolder instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        EventsManager.onMiniMapPointerUp += Teleport;
    }

    void Teleport(EditMode mode, Vector3 teleportTo)
    {
        if(mode == EditMode.Teleportation)
            transform.position = teleportTo;
    }
}