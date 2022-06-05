using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public enum EditMode { Empty, Edit, Teleportation, ArrowBasic, ArrowStroke, ArrowHorizontalBasic, ArrowHorizontalStroke, Line, Cone, Chip, Ring, PlayerTeleport, Mannequin, GatesMini, GatesMid, Remove }

public class MiniMapUIEventsHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public const byte PlayerTeleport = 0;
    public const byte EditObject = 1;
    public const byte ShirtColorChange = 2;
    public const byte ResetArrowsCount = 3;
    public const byte SendArrowComment = 4;

    public static MiniMapUIEventsHandler instance;

    [SerializeField]
    Camera miniMapCamera;

    public EditMode mode;
    [SerializeField]
    Transform playersListContent;
    [SerializeField]
    GameObject playerButtonPrefab;

    Vector3 pointerDownPoint;

    public int selectedPlayerId;

    [SerializeField]
    PhotonView selectedObject;

    public int selectedShirtColor;

    [SerializeField]
    MannequinButtonColorChangeHandler mannequinButton;

    void Awake()
    {
        instance = this;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventsManager.onMiniMapPointerDown?.Invoke(mode, RaycastFunctions.ClickPointOnGround(miniMapCamera));

        SavePointerDownPoint();
    }

    void SavePointerDownPoint()
    {
        pointerDownPoint = RaycastFunctions.ClickPointOnGround(miniMapCamera);
    }

    public void SetSelectedNetworkPlayer(int playerId)
    {
        selectedPlayerId = playerId;
    }

    public void SetShirtColor(int colorIndex)
    {
        selectedShirtColor = colorIndex;
        mannequinButton.SetShirtColor(colorIndex);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        EventsManager.onMiniMapPointerUp?.Invoke(mode, RaycastFunctions.ClickPointOnGround(miniMapCamera));

        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

        switch (mode)
        {
            case EditMode.Cone:
                PhotonNetwork.Instantiate(NetworkPrefabs.cone, RaycastFunctions.ClickPointOnGround(miniMapCamera), Quaternion.identity, 0);
                break;

            case EditMode.Ring:
                GameObject clickedObject = RaycastFunctions.ClickOnObjectWithLayer(miniMapCamera, LayerMask.GetMask("Ring"));
                Debug.Log(clickedObject?.name);
                PhotonView networkObject = clickedObject?.GetComponent<PhotonView>();
                if (networkObject != null)
                {
                    if (networkObject.name.Contains("Ring"))
                        PhotonNetwork.Destroy(networkObject);
                    else
                        PhotonNetwork.Instantiate(NetworkPrefabs.ring, RaycastFunctions.ClickPointOnGround(miniMapCamera), Quaternion.identity, 0);
                }
                else
                    PhotonNetwork.Instantiate(NetworkPrefabs.ring, RaycastFunctions.ClickPointOnGround(miniMapCamera), Quaternion.identity, 0);
                break;
                
            case EditMode.PlayerTeleport:
                content = new object[] { RaycastFunctions.ClickPointOnGround(miniMapCamera), selectedPlayerId }; // Array contains the target position and the IDs of the selected units
                PhotonNetwork.RaiseEvent(PlayerTeleport, content, raiseEventOptions, SendOptions.SendReliable);
                break;

            case EditMode.Edit:
                GameObject clickedObj = RaycastFunctions.ClickOnObject(miniMapCamera);
                PhotonView networkObj = clickedObj?.GetComponent<PhotonView>();
                if (networkObj == null) networkObj =  clickedObj?.GetComponentInParent<PhotonView>();

                if (networkObj != null)
                {
                    Debug.Log(networkObj + " & " + selectedObject);
                    Debug.Log(selectedObject?.GetComponent<MiniMapSelectionHandler>());

                    selectedObject?.GetComponent<MiniMapSelectionHandler>().Deselect();
                    selectedObject = networkObj;
                    networkObj.GetComponent<MiniMapSelectionHandler>().SetSelected();
                }
                else if(selectedObject != null)
                {
                    content = new object[] { RaycastFunctions.ClickPointOnGround(miniMapCamera), selectedObject?.ViewID }; // Array contains the target position and the IDs of the selected units
                    PhotonNetwork.RaiseEvent(EditObject, content, raiseEventOptions, SendOptions.SendReliable);
                }

                break;

            case EditMode.Remove:
                GameObject objToRemove = RaycastFunctions.ClickOnObject(miniMapCamera);
                PhotonView networkObjToRemove = objToRemove?.GetComponent<PhotonView>();
                if (networkObjToRemove == null) networkObjToRemove = objToRemove?.GetComponentInParent<PhotonView>();

                Debug.Log("Object has been FOUND!");

                if (networkObjToRemove != null && !networkObjToRemove.name.Contains("TrainerAvatar") && !networkObjToRemove.name.Contains("Ball"))
                {
                    Debug.Log("Trainer ? " + networkObjToRemove.name.Contains("TrainerAvatar") + " , Ball ? " + networkObjToRemove.name.Contains("Ball"));
                    PhotonNetwork.Destroy(networkObjToRemove);
                    selectedObject = null;
                }
                break;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("BeginDrag");

        EventsManager.onMiniMapBeginDrag?.Invoke(mode, RaycastFunctions.ClickPointOnGround(miniMapCamera));

        CreateRotatableObject(pointerDownPoint);
    }

    void CreateRotatableObject(Vector3 beginDragPoint)
    {
        Debug.Log("Create Rotatable Object!");

        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };

        switch (mode)
        {
            case EditMode.ArrowBasic:
                NetworkCopier arrow = PhotonNetwork.Instantiate(NetworkPrefabs.arrowBasic, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                ArrowCommentPanelController.currentArrowPhotonViewID = arrow.photonView.ViewID;
                EventsManager.onMiniMapDrag += arrow.ArrowDrag;
                EventsManager.onMiniMapEndDrag += arrow.ArrowEndDrag;
                break;

            case EditMode.ArrowStroke:
                NetworkCopier arrowStroke = PhotonNetwork.Instantiate(NetworkPrefabs.arrowStroke, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                ArrowCommentPanelController.currentArrowPhotonViewID = arrowStroke.photonView.ViewID;
                EventsManager.onMiniMapDrag += arrowStroke.ArrowDrag;
                EventsManager.onMiniMapEndDrag += arrowStroke.ArrowEndDrag;
                break;

            case EditMode.ArrowHorizontalBasic:
                NetworkCopier arrowHorizontal = PhotonNetwork.Instantiate(NetworkPrefabs.arrowHorizontalBasic, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                ArrowCommentPanelController.currentArrowPhotonViewID = arrowHorizontal.photonView.ViewID;
                EventsManager.onMiniMapDrag += arrowHorizontal.ArrowDrag;
                EventsManager.onMiniMapEndDrag += arrowHorizontal.ArrowEndDrag;
                break;

            case EditMode.ArrowHorizontalStroke:
                NetworkCopier arrowHorizontalStroke = PhotonNetwork.Instantiate(NetworkPrefabs.arrowHorizontalStroke, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                ArrowCommentPanelController.currentArrowPhotonViewID = arrowHorizontalStroke.photonView.ViewID;
                EventsManager.onMiniMapDrag += arrowHorizontalStroke.ArrowDrag;
                EventsManager.onMiniMapEndDrag += arrowHorizontalStroke.ArrowEndDrag;
                break;

            case EditMode.Line:
                NetworkCopier line = PhotonNetwork.Instantiate(NetworkPrefabs.line, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                EventsManager.onMiniMapDrag += line.ArrowDrag;
                EventsManager.onMiniMapEndDrag += line.ArrowEndDrag;
                break;

            case EditMode.Mannequin:
                NetworkCopier mannequin = PhotonNetwork.Instantiate(NetworkPrefabs.mannequin, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                mannequin.GetComponent<ShirtColorChangeHandler>().SetShirtColor(selectedShirtColor);
                EventsManager.onMiniMapDrag += mannequin.RotateNetworkObjectDrag;
                EventsManager.onMiniMapEndDrag += mannequin.RotateNetworkObjectEndDrag;

                content = new object[] { selectedShirtColor, mannequin.photonView.ViewID }; // Array contains the target position and the IDs of the selected units
                PhotonNetwork.RaiseEvent(ShirtColorChange, content, raiseEventOptions, SendOptions.SendReliable);
                break;

            case EditMode.GatesMini:
                NetworkCopier gates = PhotonNetwork.Instantiate(NetworkPrefabs.gatesMini, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                EventsManager.onMiniMapDrag += gates.RotateNetworkObjectDrag;
                EventsManager.onMiniMapEndDrag += gates.RotateNetworkObjectEndDrag;
                break;

            case EditMode.GatesMid:
                NetworkCopier gatesMid = PhotonNetwork.Instantiate(NetworkPrefabs.gatesMid, beginDragPoint, Quaternion.identity, 0).GetComponent<NetworkCopier>();
                EventsManager.onMiniMapDrag += gatesMid.RotateNetworkObjectDrag;
                EventsManager.onMiniMapEndDrag += gatesMid.RotateNetworkObjectEndDrag;
                break;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        EventsManager.onMiniMapDrag?.Invoke(mode, RaycastFunctions.ClickPointOnGround(miniMapCamera));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventsManager.onMiniMapEndDrag?.Invoke(mode, RaycastFunctions.ClickPointOnGround(miniMapCamera));
    }
}
