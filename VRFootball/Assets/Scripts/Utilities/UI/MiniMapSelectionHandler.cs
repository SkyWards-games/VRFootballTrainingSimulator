using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapSelectionHandler : Photon.Pun.MonoBehaviourPun
{
    public const byte PlayerTeleport = 0;
    public const byte EditObject = 1;

    [SerializeField]
    GameObject visualizeSelectionObject;

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    public void SetSelected()
    {
        visualizeSelectionObject.SetActive(true);
    }

    public void Deselect()
    {
        visualizeSelectionObject.SetActive(false);
    }

    private void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        
        if (eventCode == EditObject)
        {
            object[] data = (object[])photonEvent.CustomData;
            Vector3 targetPosition = (Vector3)data[0];
            int selectedObjectID = (int)data[1];

            if (photonView.ViewID == selectedObjectID)
                transform.position = targetPosition;
        }
    }
}
