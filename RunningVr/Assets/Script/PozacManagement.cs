using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PozacManagement : MonoBehaviour {
    public GameObject Dest;
    NavMeshAgent nav;
	// Use this for initialization
	void Awake () {
        Dest = GameObject.FindGameObjectWithTag("Destination");

        nav = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        nav.destination = Dest.transform.position;

        if (nav.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            transform.rotation = Quaternion.LookRotation(nav.velocity.normalized);
        }
    }
}