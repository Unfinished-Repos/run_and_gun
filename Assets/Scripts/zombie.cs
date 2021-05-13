using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour {
  public float speed = 1f;

  void FixedUpdate() {
    Vector2 movement = Vector2.zero;
    if (transform.position.x < findClosestPlayer().transform.position.x)
      movement.x += (Vector2.right * speed).x;
    else
      movement.x += (Vector2.left * speed).x;
    movement.y = GetComponent<Rigidbody2D>().velocity.y;
    GetComponent<Rigidbody2D>().velocity = movement;
  }

  GameObject findClosestPlayer() {
    GameObject[] objs= GameObject.FindGameObjectsWithTag("Player");
    GameObject closestEnemy = null;
    float closestDistance = 0;
    bool first = true;

    foreach (var obj in objs) {
      float distance = Vector3.Distance(obj.transform.position, transform.position);
      if (first) {
        closestDistance = distance;
        first = false;
        closestEnemy = obj;
      }else if (distance < closestDistance) {
        closestEnemy = obj;
        closestDistance = distance;
      }
    }
    return closestEnemy;
  }
}
