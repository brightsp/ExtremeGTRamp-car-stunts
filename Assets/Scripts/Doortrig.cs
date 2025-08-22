using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doortrig : MonoBehaviour
{
    public Animation door_l;
    public Animation door_r;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            Debug.Log("car trig ------ door open");
            door_l.GetComponent<Animation>().Play();
            door_r.GetComponent<Animation>().Play(); 

        }
    }
}
