using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class grasp : MonoBehaviour {
    GameObject copyableObj;

    SteamVR_TrackedObject controller;
    SteamVR_Controller.Device dev;
    
    Rigidbody grasped;
    FixedJoint fixedJoint;

	// Use this for initialization
	void Awake () {
        controller = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        dev = SteamVR_Controller.Input((int)controller.index);
        if (dev.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("Pressing Down");
            if(copyableObj != null)
            {
                //Instantiate(copyableObj, null);
                Instantiate(copyableObj, new Vector3(0, 3, 0), Quaternion.identity);
            }     
        }

        if (dev.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Dropping everything");
            Object.Destroy(fixedJoint);
        }

    }

    void OnTriggerStay(Collider col)
    {
        Debug.Log("Collided with " + col.name + " and activated OnTriggerStay");
        if (dev.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Attempting to grasp object");
            
            grasped = col.gameObject.GetComponent<Rigidbody>();
            fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = this.gameObject.GetComponent<Rigidbody>();

            copyableObj = col.gameObject;
        }
    }

}
