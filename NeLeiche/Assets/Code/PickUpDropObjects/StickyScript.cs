using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StickyScript : MonoBehaviour
{
    public PickUpScript pickUpScript;
    public GameObject player;
    Rigidbody rBody;
    public bool stickyActive;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("MainCamera");
        rBody = GetComponent<Rigidbody>();
        //pickUpScript = player.GetComponent<PickUpScript>();
    }

    // Update is called once per frame
    void Update()
    {
        pickUpScript = player.GetComponent<PickUpScript>();


        if (pickUpScript.heldObj == this)
        {
            //Destroy(gameObject.GetComponent("FixedJoint"));
            stickyActive = false;
        }
        else
        {
            stickyActive = true;
        }

        if (stickyActive == false)
        {
            FixedJoint[] hingeJoints;

            hingeJoints = gameObject.GetComponents<FixedJoint>();

            foreach (FixedJoint joint in hingeJoints)
            {
                Destroy(joint);
            }

            foreach (var comp in gameObject.GetComponents<FixedJoint>())
            {
                    Destroy(comp);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (stickyActive == true && collision.gameObject.tag != "Player")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.anchor = contact.point;
                fixedJoint.connectedBody = collision.rigidbody;
                Debug.Log(collision.gameObject.name);

                stickyActive = false;

                //DisableRagdoll();
            }

        }
        else
        {
            Destroy(gameObject.GetComponent("FixedJoint"));
            //EnableRagdoll();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //Destroy(gameObject.GetComponent("FixedJoint"));
        FixedJoint[] hingeJoints;

        hingeJoints = gameObject.GetComponents<FixedJoint>();

        foreach (FixedJoint joint in hingeJoints)
        {
            Destroy(joint);
        }

        foreach (var comp in gameObject.GetComponents<FixedJoint>())
        {
            Destroy(comp);
        }
    }

    void EnableRagdoll()
    {
        rBody.isKinematic = false;
        rBody.detectCollisions = true;
    }
    void DisableRagdoll()
    {
        rBody.isKinematic = true;
        rBody.detectCollisions = false;

    }
}