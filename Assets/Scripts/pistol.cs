using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : MonoBehaviour {
    public GameObject blast_effect;

    public void fire() {
      // Find Target TODO: Fire using gameobject not raycast
      float angle = transform.localEulerAngles.z;
      GameObject target = nearestShootable(angle);
      if (target != null)
        Destroy(target);

      // Effect animation
      GameObject temp = Instantiate(blast_effect, new Vector2(this.transform.position.x + 0.251f*Mathf.Cos(angle * Mathf.Deg2Rad), this.transform.position.y + 0.251f*Mathf.Sin(angle * Mathf.Deg2Rad)), transform.rotation);
      temp.transform.eulerAngles = new Vector3(-angle, 90, 0);
      Destroy (temp, .2f);
    }

    GameObject nearestShootable(float angle) {
      RaycastHit2D[] hits;
      Vector2 bulletHole = new Vector2(transform.position.x, transform.position.y);
      hits = Physics2D.RaycastAll(bulletHole, new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)), 100f);
      for (int i = 0; i < hits.Length; i++) {
        if (hits[i].collider.transform.gameObject != this.gameObject)
          if (hits[i].collider.transform.gameObject.tag != "Player")
            if (hits[i].collider.transform.parent == null || hits[i].collider.transform.parent.gameObject.tag != "Player")
              if (hits[i].collider.transform.gameObject.GetComponent<alive>() != null)
                return hits[i].collider.transform.gameObject;
      }
      return null;
    }
}
