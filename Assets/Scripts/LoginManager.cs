using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class LoginManager : MonoBehaviourPunCallbacks
{

    public TMP_InputField PlayerName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectAnonymously()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public void ConnectToPhotonServer()
    {
        if (PlayerName != null)
        {
            PhotonNetwork.NickName = PlayerName.text;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnected()
    {
        Debug.Log("OnConnected is called and server is available!!!!");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master server with player name" +PhotonNetwork.NickName);
        PhotonNetwork.LoadLevel("HomeScene");
    }

}
