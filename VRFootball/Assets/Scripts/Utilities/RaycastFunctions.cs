using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastFunctions
{
    public static Vector3 ClickPoint(Camera camera)
    {
        return camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
    }

    public static Vector3 ClickPointOnGround(Camera camera)
    {
        Vector3 clickPoint = ClickPoint(camera);
        return new Vector3(clickPoint.x, 0f, clickPoint.z);
    }

    public static GameObject ClickOnObject(Camera camera)
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ClickPoint(camera), Vector3.down, out hit, 100f))
        {
            return hit.transform.gameObject;
        }

        return null;
    }

    public static GameObject ClickOnObjectWithLayer(Camera camera, LayerMask layer)
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ClickPoint(camera), Vector3.down, out hit, 100f, layer))
        {
            return hit.transform.gameObject;
        }

        return null;
    }
}
