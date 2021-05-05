using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour {
    public GameObject player;
    
    void Start() {

    }

    void Update() {
      float height = Camera.main.orthographicSize * 2.0f;
      float width = height * Screen.width / Screen.height;
      if (player.transform.position.y > -5) {
          transform.position = new Vector3(Mathf.Floor((player.transform.position.x + width / 2) / width) * width, Mathf.Floor((player.transform.position.y + height / 2) / height) * height, transform.position.z);
        if (player.transform.position.x < 0) {
          transform.position = new Vector3(Mathf.Floor((player.transform.position.x + width / 2) / width) * width, Mathf.Floor((player.transform.position.y + height / 2) / height) * height, transform.position.z);
        }
      }else {
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
      }
    }
}
