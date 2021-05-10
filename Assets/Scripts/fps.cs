using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fps : MonoBehaviour{
    void Update() {
        GetComponent<UnityEngine.UI.Text>().text = ((int)(1.0f / Time.smoothDeltaTime)).ToString();
    }
}
