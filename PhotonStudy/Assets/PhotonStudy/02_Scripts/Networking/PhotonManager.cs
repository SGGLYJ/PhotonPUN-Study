using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    #region Singleton
    private static PhotonManager instance;
    public static PhotonManager Instance => instance;
    #endregion

    #region Serialized Fields

    [Tooltip("Users are separated from each other by client's Game Version.")]
    public string gameVersion = "1";

    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField] private byte maxPlayersPerRoom = 4;
    [SerializeField] private Text stateText;
    #endregion

    #region Private Fields
    private string nickName;
    private string roomName;
    #endregion

    #region MonoBehaviour Callbacks
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            Debug.LogWarning("NetworkingManager is already exist.");
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        stateText.text = PhotonNetwork.NetworkClientState.ToString();
    }

    private void OnDestroy()
    {
        instance = null;
    }
    #endregion

    #region MonoBehaviourPunCallbacks CallBacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("PhotonManager: OnConnectedToMaster");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogFormat("PhotonManager: OnDisconnected, DisconnectCause: {0}", cause);
    }

    public override void OnJoinedLobby()
    {
        Debug.LogFormat("PhotonManager: OnJoinedLobby, LobbyName: {0}", PhotonNetwork.CurrentLobby.Name);
    }

    public override void OnLeftLobby()
    {
        Debug.Log("PhotonManager: OnLeftLobby");
    }

    public override void OnCreatedRoom()
    {
        Debug.LogFormat("PhotonManager: OnCreateRoom, RoomCount: {0}", PhotonNetwork.CountOfRooms);
    }

    public override void OnJoinedRoom()
    {
        Debug.LogFormat("PhotonManager: OnJoinRoom, RoomName: {0}", PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogFormat("PhotonManager: OnJoinRoomFailed message: {0}", message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("PhotonManager: OnPlayerEnteredRoom NickName: {0}", newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.LogFormat("PhotonManager: OnPlayerLeftRoom NickName: {0}", otherPlayer.NickName);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.LogFormat("PhotonManager: OnMasterClientSwitched NickName: {0}", newMasterClient.NickName);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("PhotonManager: OnLeftRoom");
    }
    #endregion

    #region Public Methods
    public void SetNickName(string _nickName) 
    {
        nickName = _nickName;
    }

    public void SetRoomName(string _roomName) 
    {
        roomName = _roomName;
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.LocalPlayer.NickName = nickName;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void Disconnect()
    {
        PhotonNetwork.Disconnect();
    }

    public void JoinLobby()
    {
        PhotonNetwork.JoinLobby();
    }

    public void LeaveLobby()
    {
        PhotonNetwork.LeaveLobby();
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    #endregion
}