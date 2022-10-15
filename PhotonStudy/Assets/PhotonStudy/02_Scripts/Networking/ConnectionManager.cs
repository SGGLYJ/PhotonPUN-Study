using UnityEngine;
using Photon.Pun;

namespace PUNStudy.Networking
{
    public class ConnectionManager : Singleton<ConnectionManager>
    {
        [Tooltip("Users are separated from each other by client's Game Version.")]
        public string gameVersion = "1";

        #region MonoBehaviour CallBacks
        protected override void Awake()
        {
            base.Awake();
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            ConnectToPhotonCloudNetwork();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// if not yet connected, Connect this application instance to Photon Cloud Network
        /// </summary>
        public void ConnectToPhotonCloudNetwork()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
            else 
            {
                Debug.Log("ConnectToPhotonClound: Already connected!");
            }
        }
        #endregion
    }
}