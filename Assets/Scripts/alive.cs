using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alive : MonoBehaviour
{
    public GameObject killed_effect;

    void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.tag == "Deadly") {
        if ((this.gameObject.GetComponent("player_controls") as player_controls) != null) {
          this.gameObject.GetComponent<player_controls>().Death();
        }else {
          Destroy(this.gameObject);
        }
      }
    }

    void OnDestroy() {
      if(gameObject.scene.isLoaded) { //Was Deleted{
        GameObject temp = Instantiate(killed_effect, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        Destroy (temp, .5f);
      }
    }
}
