using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapModeSetHandler : MonoBehaviour
{
    public const byte PlayerTeleport = 0;
    public const byte EditObject = 1;
    public const byte ShirtColorChange = 2;
    public const byte ResetArrowsCount = 3;

    public void SetEmptyMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Empty;
    }

    public void SetEditMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Edit;
    }

    public void SetTeleportMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Teleportation;
    }

    public void SetBasicArrowMode()
    {
        CheckDoubleClick(EditMode.ArrowBasic);
        MiniMapUIEventsHandler.instance.mode = EditMode.ArrowBasic;
    }

    public void SetStrokeArrowMode()
    {
        CheckDoubleClick(EditMode.ArrowStroke);
        MiniMapUIEventsHandler.instance.mode = EditMode.ArrowStroke;
    }

    public void SetHorizontalBasicArrowMode()
    {
        CheckDoubleClick(EditMode.ArrowHorizontalBasic);
        MiniMapUIEventsHandler.instance.mode = EditMode.ArrowHorizontalBasic;
    }

    public void SetHorizontalStrokeArrowMode()
    {
        CheckDoubleClick(EditMode.ArrowHorizontalStroke);
        MiniMapUIEventsHandler.instance.mode = EditMode.ArrowHorizontalStroke;
    }

    public void SetLineMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Line;
    }

    public void SetConeMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Cone;
    }

    public void SetChipMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Chip;
    }

    public void SetRingMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Ring;
    }

    public void SetPlayerTeleportMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.PlayerTeleport;
    }

    public void SetMannequinMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Mannequin;
    }

    public void SetGatesMiniMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.GatesMini;
    }

    public void SetGatesMidMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.GatesMid;
    }

    public void SetRemoveMode()
    {
        MiniMapUIEventsHandler.instance.mode = EditMode.Remove;
    }

    void CheckDoubleClick(EditMode currentMode)
    {
        if (MiniMapUIEventsHandler.instance.mode == currentMode)
        {
            object[] content = new object[] { };
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            PhotonNetwork.RaiseEvent(ResetArrowsCount, content, raiseEventOptions, SendOptions.SendReliable);
        }
    }
}
