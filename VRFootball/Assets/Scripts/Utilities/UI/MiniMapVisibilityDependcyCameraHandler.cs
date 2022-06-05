using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapVisibilityDependcyCameraHandler : MiniMapVisibilityDependcyHandler
{
    Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    public override void SetActive()
    {
        base.SetActive();

        camera.enabled = !camera.enabled;
    }
}
