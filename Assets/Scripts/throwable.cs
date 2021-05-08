using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwable : MonoBehaviour {
    public Collider2D exParent;

    void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.GetComponent<player_controls>() != null) {
        other.gameObject.GetComponent<player_controls>().pickupable_obj.Add(this.gameObject);
      }
    }

    void OnCollisionExit2D(Collision2D other) {
      if (other.gameObject.GetComponent<player_controls>() != null) {
        other.gameObject.GetComponent<player_controls>().pickupable_obj.Remove(this.gameObject);
      }
    }

    public void thrown () {
      float angleDeg = transform.localEulerAngles.z;
      exParent = transform.parent.GetComponent<Collider2D>();
      Physics2D.IgnoreCollision(exParent, GetComponent<Collider2D>());
      GetComponent<PolygonCollider2D>().isTrigger = false;
      GetComponent<Rigidbody2D>().AddForce(new Vector2(
        Mathf.Cos(angleDeg * Mathf.Deg2Rad)*5f, Mathf.Sin(angleDeg * Mathf.Deg2Rad)*5f) * 1.9f,
        ForceMode2D.Impulse
      );
      transform.parent.GetComponent<player_controls>().weaponR = null;
      transform.parent = null;
      StartCoroutine(throw_wait());
    }

    IEnumerator throw_wait() { // TODO: make this better
      yield return new WaitForSeconds(.2f);
      Physics2D.IgnoreCollision(exParent, GetComponent<Collider2D>(), false);
    }
}
