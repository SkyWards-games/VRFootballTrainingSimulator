using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : Interactable
{
    private void Start()
    {
        ControllerManager.onTouchClicked += Interact;
    }

    public override void Interact(bool pressed)
    {
       
    }
}
