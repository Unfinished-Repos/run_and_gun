using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow : MonoBehaviour {
    public GameObject player;
    public enum styles {Blocked, Follow}
    public styles currentStyle;

    void FixedUpdate() {
      if (currentStyle == styles.Blocked) {
        transform.position = findBlocked();
      }else if (currentStyle == styles.Follow) {
        transform.position = findFollow();
      }
    }

    Vector3 findBlocked() {
      float height = Camera.main.orthographicSize * 2.0f;
      float width = height * Screen.width / Screen.height;
      if (player.transform.position.y > -5) {
        if (player.transform.position.x < 0) {
          return new Vector3(Mathf.Floor((player.transform.position.x + width / 2) / width) * width, Mathf.Floor((player.transform.position.y + height / 2) / height) * height, transform.position.z);
        }
        return new Vector3(Mathf.Floor((player.transform.position.x + width / 2) / width) * width, Mathf.Floor((player.transform.position.y + height / 2) / height) * height, transform.position.z);
      }
      return new Vector3(0,0,0);
    }

    Vector3 findFollow() {
      float height = Camera.main.orthographicSize * 2.0f;
      float width = height * Screen.width / Screen.height;
      if (player.transform.position.y > -5) {
        return new Vector3(player.transform.position.x, Mathf.Floor((player.transform.position.y + height / 2) / height) * height, transform.position.z);
      }
      return new Vector3(0,0,0);
    }
}
