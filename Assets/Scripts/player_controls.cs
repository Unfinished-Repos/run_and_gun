using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_controls : MonoBehaviour {
    public float speed = 1f;
    public float sprintBoost = 1.5f;
    public int jumpsLeft = 0;
    public Sprite[] moves;
    public GameObject gun; // TODO: Right & Left side implementation

    void Start() {

    }

    void Update() {
      // Gun controls
      Vector3 mousePos = Input.mousePosition;
      mousePos.z = Camera.main.nearClipPlane;
      Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
      gun.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(worldPosition.y - transform.position.y, worldPosition.x - transform.position.x) * Mathf.Rad2Deg));
      gun.GetComponent<SpriteRenderer>().flipY = false;
      if (gun.transform.localEulerAngles.z > 90 && gun.transform.localEulerAngles.z < 270) {
        gun.GetComponent<SpriteRenderer>().flipY = true;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

      if (Input.GetMouseButtonDown(0)) {
        gun.GetComponent<pistol>().fire();
      }
    }

    void OnCollisionEnter2D(Collision2D other) {
      jumpsLeft = 1;
    }
}
