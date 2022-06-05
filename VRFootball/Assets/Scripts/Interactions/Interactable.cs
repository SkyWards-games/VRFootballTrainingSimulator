using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractbleType { Interactable, Draggable, Teleportable }

public class Interactable : MonoBehaviour
{
    public InteractbleType type;
    public Collider col;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    
    public virtual void Interact(bool started)
    {
        DebugLog.Print(" | Interact with " + name);
    }
}
