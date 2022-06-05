using ExitGames.Client.Photon;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrackedObject { Body, Head, Arrow, Mannequin, Line, Gates }

public class NetworkCopier : Photon.Pun.MonoBehaviourPun
{
    public const byte PlayerTeleport = 0;
    public const byte EditObject = 1;
    public const byte ShirtColorChange = 2;
    public const byte ResetArrowsCount = 3;
    public const byte SendArrowComment = 4;

    [SerializeField]
    TrackedObject trackedObj;
    
    private void Start()
    {
        if (trackedObj == TrackedObject.Body)
        {
            // Deprecated in v0.2.5 - no need to use players icons to select them, because they are selectable through the tap in 3D MiniMap space.
            //if (!photonView.IsMine)
            //{
            //    GetComponentInChildren<NetworkPlayerIconHolder>().SetPlayerNumber(photonView.ControllerActorNr.ToString());
            //    MiniMapUIEventsHandler.instance.CreateNetworkPlayerIcon(this, photonView.ControllerActorNr.ToString());
            //}
        }

        if (trackedObj == TrackedObject.Arrow)
        {
            GetComponentInChildren<NetworkArrowIconHolder>().SetArrowNumber();
        }
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    private void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if (eventCode == PlayerTeleport)
        {
            object[] data = (object[])photonEvent.CustomData;
            Vector3 targetPosition = (Vector3)data[0];
            int targetNetworkPlayerId = (int)data[1];

            if (photonView.ControllerActorNr == targetNetworkPlayerId)
                transform.position = targetPosition;
        }

        if (eventCode == EditObject)
        {
            object[] data = (object[])photonEvent.CustomData;
            Vector3 targetPosition = (Vector3)data[0];
            int selectedObjectID = (int)data[1];

            if (photonView.ViewID == selectedObjectID)
                transform.position = targetPosition;
        }

        if (eventCode == ShirtColorChange)
        {
            object[] data = (object[])photonEvent.CustomData;
            Color targetColor = new Color();
            int selectedObjectID = (int)data[1];

            Debug.Log("Player " + selectedObjectID + " shirt color change event received to " + targetColor);

            if (photonView.ViewID == selectedObjectID)
            {
                GetComponent<ShirtColorChangeHandler>().SetShirtColor((int)data[0]);
            }
        }

        if (eventCode == ResetArrowsCount)
        {
            NetworkArrowIconHolder.arrowsCount = 0;
        }

        if (eventCode == SendArrowComment)
        {
            object[] data = (object[])photonEvent.CustomData;
            string comment = (string)data[0];
            int selectedObjectID = (int)data[1];

            Debug.Log("Player " + selectedObjectID + " shirt color change event received to " + comment);

            if (photonView.ViewID == selectedObjectID)
            {
                GetComponentInChildren<ArrowCommentHolder>().SetComment((string)data[0], Mathf.Abs(transform.rotation.y));
            }
        }
    }

    

    void Update()
    {
        if(photonView.IsMine)
        {
            if(trackedObj == TrackedObject.Body)
            {
                transform.position = PlayerAvatarHolder.instance.body.position;
                
                PlayerAvatarHolder.instance.target.position = new Vector3(PlayerAvatarHolder.instance.target.position.x, 2.13f, PlayerAvatarHolder.instance.target.position.z);
                PlayerAvatarHolder.instance.relative.rotation = Quaternion.LookRotation(PlayerAvatarHolder.instance.target.position, Vector3.up);

                if (PlayerAvatarHolder.instance.head.localRotation.y > 0.1 || PlayerAvatarHolder.instance.head.localRotation.y < -0.1)
                    transform.rotation = Quaternion.Lerp(transform.rotation, PlayerAvatarHolder.instance.relative.rotation, 0.1f);
            }

            if (trackedObj == TrackedObject.Head)
            {
                transform.rotation = PlayerAvatarHolder.instance.head.rotation;
            }
        }
    }

    public void ArrowDrag(EditMode mode, Vector3 tipPoint)
    {
        transform.LookAt(tipPoint);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Vector3.Distance(transform.position, tipPoint) * 0.1f);
    }

    public void RotateNetworkObjectDrag(EditMode mode, Vector3 direction)
    {
        transform.LookAt(direction);
    }

    public void ArrowEndDrag(EditMode mode, Vector3 tipPoint)
    {
        EventsManager.onMiniMapDrag -= ArrowDrag;
        EventsManager.onMiniMapEndDrag -= ArrowEndDrag;
        ArrowCommentPanelController.instance.EnableCommentPanel();
    }
    
    public void RotateNetworkObjectEndDrag(EditMode mode, Vector3 direction)
    {
        GetComponent<ShirtColorChangeHandler>()?.DisableDirectionArrow();
        GetComponent<DirectionArrowDisableHandler>()?.DisableDirectionArrow();
        EventsManager.onMiniMapDrag -= RotateNetworkObjectDrag;
        EventsManager.onMiniMapEndDrag -= RotateNetworkObjectEndDrag;
    }

    private void OnDestroy()
    {
        EventsManager.onMiniMapDrag -= ArrowDrag;
        EventsManager.onMiniMapEndDrag -= ArrowEndDrag;
    }
}
