using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom && PV.IsMine)
        {
            transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * Time.deltaTime);


            if (Input.GetKeyDown(KeyCode.T)) 
            {
                PV.RPC("MoveUpRPC", RpcTarget.All);
            }
        }

        
    }

    [PunRPC]
    public void MoveUpRPC() 
    {
        transform.Translate(Vector3.up);
    }
}
