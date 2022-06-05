using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Interactable
{
    bool dragging;

    private void Start()
    {
        ControllerManager.onTriggerPressed += Interact;
    }

    public override void Interact(bool drag)
    {
       
    }
    
    private void Update()
    {
       
    }
}
