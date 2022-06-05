using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ViewportParameters
{
    public float x;
    public float y;
    public float w;
    public float h;

    public ViewportParameters(float x, float y, float w, float h)
    {
        this.x = x;
        this.y = y;
        this.w = w;
        this.h = h;
    }
}

public class MiniMapVisibilityDependcyCameraViewportHandler : MiniMapVisibilityDependcyHandler
{
    Camera camera;

    [SerializeField]
    ViewportParameters defaultViewport = new ViewportParameters(0,0,1,1);
    [SerializeField]
    ViewportParameters minimizedViewport = new ViewportParameters(0, 0.68f, 1, 0.32f);

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    public override void SetActive()
    {
        base.SetActive();

        if(camera.rect.y == 0) // Default Viewport that needs to be changed to Minimized Values
        {
            camera.rect = new Rect(minimizedViewport.x, minimizedViewport.y, minimizedViewport.w, minimizedViewport.h);
        }
        else // Minimized Viewport that needs to be changed to Default Values
        {
            camera.rect = new Rect(defaultViewport.x, defaultViewport.y, defaultViewport.w, defaultViewport.h);
        }
        
    }
}
