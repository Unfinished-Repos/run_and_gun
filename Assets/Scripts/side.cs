using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class side : MonoBehaviour {
    void Start() {
      Physics2D.IgnoreCollision(transform.parent.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void OnTriggerStay2D(Collider2D other) {
      if (this.gameObject.name == "Right") {
        transform.parent.GetComponent<player_controls>().onRight = true;
      }else {
        transform.parent.GetComponent<player_controls>().onLeft = true;
      }
    }

    void OnTriggerExit2D(Collider2D other) {
      if (this.gameObject.name == "Right") {
        transform.parent.GetComponent<player_controls>().onRight = false;
      }else {
        transform.parent.GetComponent<player_controls>().onLeft = false;
      }
    }
}
