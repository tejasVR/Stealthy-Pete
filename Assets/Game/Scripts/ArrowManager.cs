using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    public static ArrowManager Instance;

    bool isAttached = false;
    private bool isFired = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        AttachArrow();
    }

    private void OnTriggerEnter(Collider other)
    {
        AttachArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFired)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Fired()
    {
        isFired = true;
    }

    private void AttachArrow()
    {
        if (CrossbowManager.Instance.stringReady == true)
        {
            CrossbowManager.Instance.AttachBowToArrow();
            isAttached = true;
        }
            
    }
}
