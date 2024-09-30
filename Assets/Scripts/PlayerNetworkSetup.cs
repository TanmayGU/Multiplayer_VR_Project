using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;



public class PlayerNetworkSetup : MonoBehaviourPunCallbacks
{

    public GameObject LocalXRRigGameobject;

    public GameObject AvatarHead;
    public GameObject AvatarBody;
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            ///the player is local
            LocalXRRigGameobject.SetActive(true);
            SetLayerRecursively(AvatarHead,6);
            SetLayerRecursively(AvatarBody,7);

            TeleportationArea[] teleportationAreas = GameObject.FindObjectsOfType<TeleportationArea>();
            if(teleportationAreas.Length > 0)
            {
                Debug.Log("Found"+ teleportationAreas.Length + "teleportation area.");
                foreach (var item in teleportationAreas)
                {
                    item.teleportationProvider = LocalXRRigGameobject.GetComponent<TeleportationProvider>();
                }
            }
        }

        else 
        {
            //the player is remote player
            LocalXRRigGameobject.SetActive(false);

            SetLayerRecursively(AvatarHead, 0);
            SetLayerRecursively(AvatarBody, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetLayerRecursively(GameObject go,int layerNumber)
    {
        if (go == null) return;
        foreach(Transform trans in go.GetComponentInChildren<Transform>(true)) 
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
