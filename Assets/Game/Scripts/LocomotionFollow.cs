using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionFollow : MonoBehaviour {

    public GameObject locomotionRig;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(locomotionRig.transform.position.x, 0f, locomotionRig.transform.position.z);
        //Vector3.Lerp(transform.position, new Vector3(locomotionRig.transform.position.x, 0f, locomotionRig.transform.position.z), Time.deltaTime);


    }
}
