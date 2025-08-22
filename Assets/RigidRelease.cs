using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidRelease : MonoBehaviour
{

    public Rigidbody[] AllRigids;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OpenRigid()
    {
        for (int i=0;i<AllRigids.Length;i++)
        {
            AllRigids[i].isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
