using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_controls : MonoBehaviour {
    public float speed = 5f;
    public float sprintBoost = 1.5f;
    public int jumpsLeft = 0;
    public Sprite[] moves;
    public GameObject weaponR; // TODO: Right & Left side implementation
    public List<GameObject> pickupable_obj = new List<GameObject>();
    public bool onRight, onLeft;

    void Update() {
      // Gun controls
      if (weaponR != null) {
        weaponR.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.15f);
        follow_mouse(weaponR);
        if (weaponR.GetComponent<pistol>() != null)
          gun_controls(weaponR);
        if (Input.GetMouseButtonDown(1))
          weaponR.GetComponent<throwable>().thrown();
      }else {
        if (Input.GetKeyDown("space") && pickupable_obj.Count > 0) {
          weaponR = pickupable_obj[Random.Range(0, pickupable_obj.Count-1)];
          pickupable_obj.Remove(weaponR);
          weaponR.GetComponent<PolygonCollider2D>().isTrigger = true;
          weaponR.transform.parent = this.transform;
        }
      }

      // kill condition
      if (transform.position.y < -10)
        Death();
    }

    void FixedUpdate() {
      // Player movement
      GetComponent<SpriteRenderer>().sprite = moves[0]; // default
      GetComponent<SpriteRenderer>().flipX = false;
      Vector2 movement = Vector2.zero; //Vector2.zero;
      //bool grounded = onGround();
      if (Input.GetKey(KeyCode.D)) {
        // check if something to right (&& vel.x == 0?)
        if (!Input.GetKey(KeyCode.LeftShift)) {
          movement.x += (Vector2.right * speed).x;
          //Debug.Log(GetComponent<Rigidbody2D>().velocity.x);
          //transform.Translate(Vector2.right * (float)Time.deltaTime * speed, Space.Self);
          GetComponent<SpriteRenderer>().sprite = moves[1];
        }else { // sprinting
          movement.x += (Vector2.right * speed * sprintBoost).x;
          GetComponent<SpriteRenderer>().sprite = moves[2];
        }
      }
      if (Input.GetKey(KeyCode.A)) {
        GetComponent<SpriteRenderer>().flipX = true;
        if (!Input.GetKey(KeyCode.LeftShift)) {
          movement.x += (Vector2.left * speed).x;
          //transform.Translate(Vector2.left * (float)Time.deltaTime * speed, Space.Self);
          GetComponent<SpriteRenderer>().sprite = moves[1];
        }else { // sprinting
          movement.x += (Vector2.left * speed * sprintBoost).x;
          GetComponent<SpriteRenderer>().sprite = moves[2];
        }
      }

      bool grounded = onGround();
      if (movement.x > 0 && onRight && !grounded) {
        movement.x = 0;
      }
      if (movement.x < 0 && onLeft && !grounded) {
        movement.x = 0;
      }

      if (Input.GetKey(KeyCode.W) && jumpsLeft > 0) {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 2.0f) * 4.0f, ForceMode2D.Impulse);
        jumpsLeft--;
        //movement.y = (transform.up*Time.deltaTime*speed*25f).y;
      }
      movement.y = GetComponent<Rigidbody2D>().velocity.y;
      GetComponent<Rigidbody2D>().velocity = movement;
      //movement = movement + (Vector2)(transform.position);
      //GetComponent<Rigidbody2D>().MovePosition(movement);
    }

    void OnCollisionEnter2D(Collision2D other) {
      if (onGround())
        jumpsLeft = 1;
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
      if (Input.GetMouseButtonDown(0))
        gun.GetComponent<pistol>().fire();
    }

    bool onGround() { // TODO: Optimize
      float currentx = transform.position.x - transform.localScale.x / 2f;
      RaycastHit2D hit;
      do {
        Vector2 under = new Vector2(currentx, transform.position.y - transform.localScale.y / 2f - 0.01f);
        hit = Physics2D.Raycast(under, -Vector2.up, .01f);
        currentx += transform.localScale.x / 10f; // 10 raycasts
      }while (currentx <= transform.position.x + transform.localScale.x / 2f && hit.collider == null);
      return hit.collider != null;
    }
}
