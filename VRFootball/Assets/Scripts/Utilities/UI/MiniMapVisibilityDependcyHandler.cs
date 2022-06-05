using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapVisibilityDependcyHandler : MonoBehaviour
{
    void Start()
    {
        EventsManager.onCameraMinimize += SetActive;
    }
    
    public virtual void SetActive()
    {
        Debug.Log(name + " visibility status changed.");
    }

    private void OnDestroy()
    {
        EventsManager.onCameraMinimize -= SetActive;
    }
}
