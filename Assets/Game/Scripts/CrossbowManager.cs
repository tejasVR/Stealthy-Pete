using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowManager : MonoBehaviour {

    public static CrossbowManager Instance;

    public SteamVR_TrackedObject rightController;
    public SteamVR_TrackedObject leftController;


    //Trigger
    public GameObject boneTrigger;
    private Vector3 boneTriggerStartPosition;
    private Vector3 boneTriggerTargetPosition;
    private Quaternion boneTriggerStartRotation;
    private Quaternion boneTriggerTargetRotation;

    //Handle
    public GameObject handle;
    public GameObject handleMiddleStringAttach;
    public float handleDist;

    //String
    public GameObject middleString;
    public GameObject middleStringStart;
    public GameObject middleStringEnd;
    public GameObject rightString;
    public GameObject leftString;

    //Arrow
    public GameObject arrowPrefab;
    private GameObject currentArrow;
    public GameObject arrowStart;
    public GameObject stringAttachPoint;
    //public int arrowCount = 3; <<--- Save for later

    private bool isAttached = false;

    public bool stringReady = false;
    public bool arrowAttatched = false;

    private float triggerAxis;

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

        //Trigger
        boneTriggerStartPosition = boneTrigger.transform.localPosition;
        boneTriggerStartRotation = boneTrigger.transform.localRotation;
        boneTriggerTargetPosition = new Vector3(0f, -1.1f, 0f);
    }

    // Update is called once per frame
    void Update () {
        AttachArrow();
        //Gets SteamVR Controller and gets trigger press depth
        SteamVR_Controller.Device deviceRight = SteamVR_Controller.Input((int)rightController.index);
        triggerAxis = deviceRight.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x; //get trigger depth


        //Trigger
        boneTrigger.transform.localPosition = Vector3.Lerp(boneTrigger.transform.localPosition, boneTriggerTargetPosition * triggerAxis, Time.deltaTime * 10);
        boneTriggerTargetRotation = Quaternion.Euler(new Vector3(90f + (-9.6f * triggerAxis), 0f, -90f));
        boneTrigger.transform.localRotation = Quaternion.Slerp(boneTrigger.transform.localRotation, boneTriggerTargetRotation, Time.deltaTime * 10);

        if (triggerAxis == 1) //trigger is pulled all the way
        {
            if (isAttached)
            {
                Fire();

            }
        }

        //String Pull back as touchpad down button is pressed
        if (deviceRight.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad;
            touchpad = deviceRight.GetAxis();

            if (touchpad.y < 0f)
            {
                StringPull();
            }
        }
    }

    private void AttachArrow()
    {
        if (currentArrow == null)
        {
            currentArrow = Instantiate(arrowPrefab);
            currentArrow.transform.parent = leftController.transform;
            currentArrow.transform.localPosition = new Vector3(0f, -.02f, -.035f);
            currentArrow.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);

        }
    }

    public void StringPull()
    {
        if (stringReady == false)
        {
            middleString.transform.position = Vector3.MoveTowards(middleString.transform.position, middleStringEnd.transform.position, Time.deltaTime);
            leftString.transform.position = Vector3.MoveTowards(leftString.transform.position, middleStringEnd.transform.position, Time.deltaTime);
            rightString.transform.position = Vector3.MoveTowards(rightString.transform.position, middleStringEnd.transform.position, Time.deltaTime);
            if (middleString.transform.position == middleStringEnd.transform.position)
            {
                stringReady = true;
            }

        }

    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.position = arrowStart.transform.position;
        currentArrow.transform.rotation = arrowStart.transform.rotation;

        isAttached = true;
    }

    public void Fire()
    {
        currentArrow.transform.parent = null;
        currentArrow.GetComponent<ArrowManager>().Fired();
        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * -13f;
        r.useGravity = true;
        r.isKinematic = false;

        middleString.transform.position = Vector3.MoveTowards(middleString.transform.position, middleStringStart.transform.position, Time.deltaTime * 15f);
        leftString.transform.position = Vector3.MoveTowards(leftString.transform.position, middleStringStart.transform.position, Time.deltaTime * 15f);
        rightString.transform.position = Vector3.MoveTowards(rightString.transform.position, middleStringStart.transform.position, Time.deltaTime * 15f);

        if (middleString.transform.position == middleStringStart.transform.position)
        {
            currentArrow = null;
            isAttached = false;
            stringReady = false;
        }
    }    
}
