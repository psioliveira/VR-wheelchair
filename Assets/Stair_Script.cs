using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Stair_Script : MonoBehaviour
{
    [SerializeField]
    VRTK_BodyPhysics body;
    Rigidbody me;

    private void LateStart()
    {
        me = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        transform.rotation = me.rotation;
        body.enableBodyCollisions = false;
    }
    private void LateUpdate()
    {
        body.enableBodyCollisions = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Rigid")
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(transform.forward * 80);
        }
    }
}
