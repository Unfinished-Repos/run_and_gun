using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : MonoBehaviour {
    public GameObject blast_effect;

    public void fire() {
      // TODO: Fire using gameobject not raycast
      //
      // Effect animation
      float angle = transform.localEulerAngles.z;
      GameObject temp = Instantiate(blast_effect, new Vector2(this.transform.position.x + 0.251f*Mathf.Cos(angle * Mathf.Deg2Rad), this.transform.position.y + 0.251f*Mathf.Sin(angle * Mathf.Deg2Rad)), transform.rotation);
      temp.transform.eulerAngles = new Vector3(-angle, 90, 0);
      Destroy (temp, .2f);
    }
}
