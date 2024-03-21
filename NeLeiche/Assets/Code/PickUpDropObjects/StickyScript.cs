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
        pickUpScript = player.GetComponent<PickUpScript>();
    }

    // Update is called once per frame
    void Update()
    {

        pickUpScript = player.GetComponent<PickUpScript>();

        if (pickUpScript.heldObj == this)
        {
            foreach (var comp in gameObject.GetComponents<FixedJoint>())
            {
                    Destroy(comp);
            }
            //Destroy(gameObject.GetComponent("FixedJoint"));
            stickyActive = false;
        }
        else
        {
            stickyActive = true;
        }

        if (stickyActive == false)
        {
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (stickyActive == true)
        {
            foreach (ContactPoint contact in collision.contacts)
            {

                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.anchor = contact.point;
                fixedJoint.connectedBody = collision.rigidbody;


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
        Destroy(gameObject.GetComponent("FixedJoint"));
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