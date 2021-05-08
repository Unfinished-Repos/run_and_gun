using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_controls : MonoBehaviour {
    public float speed = 1f;
    public float sprintBoost = 1.5f;
    public int jumpsLeft = 0;
    public Sprite[] moves;
    public GameObject weaponR; // TODO: Right & Left side implementation
    public List<GameObject> pickupable_obj = new List<GameObject>();
    void Start() {

    }

    void Update() {
      // Gun controls
      if (weaponR != null) {
        weaponR.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.15f);
        follow_mouse(weaponR);
        if (weaponR.GetComponent<pistol>() != null) {
          gun_controls(weaponR);
        }else {
          beer_controls(weaponR);
        }
      }else {
        if (Input.GetKeyDown("space") && pickupable_obj.Count > 0) {
          weaponR = pickupable_obj[Random.Range(0, pickupable_obj.Count-1)];
          pickupable_obj.Remove(weaponR);
          weaponR.GetComponent<PolygonCollider2D>().isTrigger = true;
          weaponR.transform.parent = this.transform;
        }
      }
      // Player movement
      GetComponent<SpriteRenderer>().sprite = moves[0]; // default
      GetComponent<SpriteRenderer>().flipX = false;
      if (Input.GetKey(KeyCode.D)) {
        if (!Input.GetKey(KeyCode.LeftShift)) {
          transform.Translate(Vector2.right * (float)Time.deltaTime * speed, Space.Self);
          GetComponent<SpriteRenderer>().sprite = moves[1];
        }else { // sprinting
          transform.Translate(Vector2.right * (float)Time.deltaTime * speed * sprintBoost, Space.Self);
          GetComponent<SpriteRenderer>().sprite = moves[2];
        }
      }
      if (Input.GetKey(KeyCode.A)) {
        GetComponent<SpriteRenderer>().flipX = true;
        if (!Input.GetKey(KeyCode.LeftShift)) {
          transform.Translate(Vector2.left * (float)Time.deltaTime * speed, Space.Self);
          GetComponent<SpriteRenderer>().sprite = moves[1];
        }else { // sprinting
          transform.Translate(Vector2.left * (float)Time.deltaTime * speed * sprintBoost, Space.Self);
          GetComponent<SpriteRenderer>().sprite = moves[2];
        }
      }
      if (Input.GetKey(KeyCode.W) && jumpsLeft > 0) {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 2.0f) * 4.0f, ForceMode2D.Impulse);
        jumpsLeft--;
      }
      if (transform.position.y < -10)
        Death();
    }

    void OnCollisionEnter2D(Collision2D other) {
      jumpsLeft = 1;
      //Debug.Log(other.gameObject.tag);
    }

    public void Death () {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void follow_mouse(GameObject holdingObj) {
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = Camera.main.nearClipPlane;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      holdingObj.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x) * Mathf.Rad2Deg));
      float angleDeg = holdingObj.transform.localEulerAngles.z;
      holdingObj.GetComponent<SpriteRenderer>().flipY = false;
      if (angleDeg > 90 && angleDeg < 270)
        holdingObj.GetComponent<SpriteRenderer>().flipY = true;
    }

    void gun_controls(GameObject gun) {
      // Mouse follow

      // Control
      if (Input.GetMouseButtonDown(0))
        gun.GetComponent<pistol>().fire();
      if (Input.GetMouseButtonDown(1))
        gun.GetComponent<pistol>().thrown();
    }

    void beer_controls(GameObject beer) {
      if (Input.GetMouseButtonDown(1))
        beer.GetComponent<beer>().thrown();
    }
}
