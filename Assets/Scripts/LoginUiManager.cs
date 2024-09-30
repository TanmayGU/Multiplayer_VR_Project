using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginUiManager : MonoBehaviour
{
    public GameObject ConnectWithOptionsPanel;
    public GameObject ConnectWithNamePanel;

    // Start is called before the first frame update
    void Start()
    {
        ConnectWithOptionsPanel.SetActive(true);
        ConnectWithNamePanel.SetActive(false);
    }

   
}
