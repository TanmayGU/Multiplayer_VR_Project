using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class NetworkGrabbing : MonoBehaviourPunCallbacks, IPunOwnershipCallbacks
{
    PhotonView m_PhotonView;

    Rigidbody rb;

    bool isBeingHeld = false;

    private void Awake()
    {
        m_PhotonView = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            ///object is grabbed
            rb.isKinematic = true;
            gameObject.layer = 11;
        }
        else
        {
            rb.isKinematic= false;
            gameObject.layer = 9;
        }
    }

    private void TransferOwnership()
    {
        m_PhotonView.RequestOwnership();
    }


    public void OnSelectEntered()
    {
        Debug.Log("Object Grabbed!!!");
        m_PhotonView.RPC("StartNetworkGrabbing", RpcTarget.AllBuffered);


        if (m_PhotonView.Owner == PhotonNetwork.LocalPlayer)
        {
            Debug.Log("Don't Need Ownership, object is already mine");
        }
        else
        {
            TransferOwnership();
        }
        
    }

    public void OnSelectExited()
    {
        Debug.Log("Object Released!!!");
        m_PhotonView.RPC("StopNetworkGrabbing",RpcTarget.AllBuffered);
    }

    public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
    {
        //throw new System.NotImplementedException();

        if (targetView != m_PhotonView)
        {
            return;

        }
        Debug.Log("Ownership is requested for : "+targetView.name+ " from " + requestingPlayer.NickName);
        m_PhotonView.TransferOwnership(requestingPlayer);
    }

    public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
    {
        //throw new System.NotImplementedException();
        Debug.Log("On Ownership tranformed to "+ targetView.name+ " from "+ previousOwner.NickName);
    }

    public void OnOwnershipTransferFailed(PhotonView targetView, Player senderOfFailedRequest)
    {
        //throw new System.NotImplementedException();
    }

    [PunRPC]
    public void StartNetworkGrabbing()
    {
        isBeingHeld = true;
    }

    [PunRPC]
    public void StopNetworkGrabbing()
    {
        isBeingHeld = false;
    }
}
