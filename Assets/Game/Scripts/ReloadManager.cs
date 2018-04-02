using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadManager : MonoBehaviour {

    public static ReloadManager Instance;

    public SteamVR_TrackedObject leftController;

    //Handle
    public GameObject handle;

    public Vector3 handleStartDist; //get the position when the player actually preses the grip on the handle

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Handle
        //SteamVR_Controller.Device 

    }

    private void OnTriggerStay(Collider other)
    {
        /*var deviceLeft = SteamVR_Controller.Input((int)leftController.index);
        if (other.gameObject == handle && CrossbowManager.Instance.handlePull == false)
        {
            if (deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
            {
                handleStartDist = leftController.transform.position;
            }

            if (deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                CrossbowManager.Instance.HandlePull();
            }
            //CrossbowManager.Instance.handlePull = true;


        } */
    }
}
