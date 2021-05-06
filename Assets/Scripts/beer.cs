using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beer : MonoBehaviour {
    public GameObject shattered_effect;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnDestroy() {
      if(gameObject.scene.isLoaded) { //Was Deleted{
        GameObject temp = Instantiate(shattered_effect, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        Destroy (temp, .5f);
      }
    }

    void OnCollisionEnter2D(Collision2D other) {
      float speed = GetComponent<Rigidbody2D>().velocity.magnitude;
      if (speed > 3f*Mathf.Pow(10, -7)) {
        Destroy(this.gameObject);
      }
    }
}
