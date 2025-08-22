using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddExtraForce : MonoBehaviour
{
    public Rigidbody M_rgd;
    public int forc = 25;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.transform.name.Contains("Car"))
        {
            Debug.Log(collision.transform.name);
            M_rgd.AddRelativeForce(this.transform.forward*forc, ForceMode.Force);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
