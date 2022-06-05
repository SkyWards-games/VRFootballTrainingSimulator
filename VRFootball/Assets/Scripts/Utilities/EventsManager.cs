using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{

    #region UI.Events

    public delegate void OnCameraMinimize();
    public static OnCameraMinimize onCameraMinimize;

    public delegate void OnUIButtonPressed();
    public static OnUIButtonPressed onUIButtonPressed;

    public delegate void OnMiniMapPointerDown(EditMode mode, Vector3 beginDragPoint);
    public static OnMiniMapPointerDown onMiniMapPointerDown;

    public delegate void OnMiniMapBeginDrag(EditMode mode, Vector3 beginDragPoint);
    public static OnMiniMapBeginDrag onMiniMapBeginDrag;

    public delegate void OnMiniMapDrag(EditMode mode, Vector3 endDragPoint);
    public static OnMiniMapDrag onMiniMapDrag;

    public delegate void OnMiniMapEndDrag(EditMode mode, Vector3 endDragPoint);
    public static OnMiniMapEndDrag onMiniMapEndDrag;
    
    public delegate void OnMiniMapPointerUp(EditMode mode, Vector3 pointerUpPoint);
    public static OnMiniMapPointerUp onMiniMapPointerUp;

    public delegate void OnMenuButtonsVisibilityChange();
    public static OnMenuButtonsVisibilityChange onMenuButtonsVisibilityChange;

    public delegate void OnArrowCommentConfirmed(EditMode mode, string comment);
    public static OnArrowCommentConfirmed onArrowCommentConfirmed;

    public void OnCameraMinimizeInvoke()
    {
        onCameraMinimize?.Invoke();
    }

    #endregion
}
