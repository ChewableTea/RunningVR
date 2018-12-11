using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PozacManagement : MonoBehaviour {
    public float speed;
	// Use this for initialization
	void Start () {
        speed = 50.0f;
    }
	
	// Update is called once per frame
	void Update () {
        float move = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * move);
    }
}
