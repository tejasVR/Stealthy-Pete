using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionRig : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    public static LocomotionRig Instance;

    public float triggerAxis;
    public float moveSpeed;
    public float sprintSpeed;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

   void FixedUpdate()
    {
        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);
        transform.forward = trackedObj.transform.forward;

        triggerAxis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x;
        //transform.localPosition = new Vector3(0f, 0f, triggerAxis);//transform.localPosition + transform.forward;

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            sprintSpeed = 2;
        } else
        {
            sprintSpeed = 1;
        }
      
       if (triggerAxis > .05f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, transform.forward * triggerAxis * moveSpeed * sprintSpeed, Time.deltaTime);
        } else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, transform.forward * triggerAxis * moveSpeed * sprintSpeed, Time.deltaTime * 6);
        }

        var eular = trackedObj.transform.rotation.eulerAngles;
        var rotY = Quaternion.Euler(0, eular.y, 0);

        transform.rotation = rotY;


    }
}
