using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class grasp : MonoBehaviour {
    public Rigidbody rigidBodyAttachPoint;

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
            fixedJoint = null;
            toss(grasped);
        }

    }

    void OnTriggerStay(Collider col)
    {
        if (dev == null)
            return;

        Debug.Log("Collided with " + col.name + " and activated OnTriggerStay");
        if (dev.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("Attempting to grasp object");
            copyableObj = col.gameObject;

            grasped = col.gameObject.GetComponent<Rigidbody>();
            fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidBodyAttachPoint;

            
        }
    }

    void toss(Rigidbody bod)
    {
        //Transform origin = controller.origin;
        //bod.velocity = origin.TransformVector(rigidBodyAttachPoint.velocity);
        //bod.angularVelocity = origin.TransformVector(rigidBodyAttachPoint.angularVelocity);
        bod.velocity = dev.velocity;
        bod.angularVelocity = dev.velocity;
        
    }

}
