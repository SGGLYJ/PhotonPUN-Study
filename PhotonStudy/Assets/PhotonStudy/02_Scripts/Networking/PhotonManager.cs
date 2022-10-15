using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

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
    #endregion

    #region Private Fields
    private string nickName;
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

    private void OnDestroy()
    {
        instance = null;
    }
    #endregion

    #region MonoBehaviourPunCallbacks CallBacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("PhotonManager: OnConnectedToMaster, JoinLobby Called");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("PhotonManager: OnJoinedLobby, JoinRandomRoom Called");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("PhotonManager: OnCreatedRoom");

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("PhotonManager: OnJoinedRoom");

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("PhotonManager: OnJoinRoomFailed, CreateRoom Called");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PhotonManager: OnJoinRandomFailed, CreateRoom Called");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("PhotonManager: OnPlayerEnteredRoom");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("PhotonManager: OnPlayerLeftRoom");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("PhotonManager: OnMasterClientSwitched");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("PhotonManager: OnLeftRoom");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("PhotonManager: OnLeftLobby");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PhotonManager: OnDisconnected, DisconnectCause: {0}", cause);
    }
    #endregion

    #region Public Methods
    public void SetNickName(string _nickName) 
    {
        nickName = _nickName;
    }

    /// <summary>
    /// if not yet connected, Connect this application instance to Photon Cloud Network
    /// </summary>
    public void ConnectToPhotonCloudNetwork()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.NickName = nickName;
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            Debug.Log("Already connected to server");
        }
    }
    #endregion
}