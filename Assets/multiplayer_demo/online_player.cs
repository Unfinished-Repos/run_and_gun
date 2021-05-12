using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class online_player : MonoBehaviour {
    GameObject clientCamera;

    void Start() {
      if (GetComponent<NetworkIdentity>() == null || GetComponent<NetworkIdentity>().isLocalPlayer) {
        this.gameObject.GetComponent<player_controls>().enabled = true;
        clientCamera = GameObject.Find("Main Camera");
        clientCamera.AddComponent<camera_follow>();
        clientCamera.GetComponent<camera_follow>().player = this.gameObject;
      }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
