using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateScript : MonoBehaviour
{
    public static RotateScript mee;
    //public int counterwise=1;
    public int speed = 20;
    public GameObject starobj, animstar;
    public GameObject stareffect;
    // public Vector3 VAl;

    public Vector3 val = Vector3.up;
    void Awake()
    {
        mee = this;
        if (stareffect != null)
        {
            stareffect.transform.parent = null;
        }

    }

    void Update()
    {
        //if (VAl == Vector3.zero)
        //{
        //    transform.Rotate(0, -10 * 5 * Time.deltaTime * counterwise, 0);
        //}
        //else
        //{
        //    transform.Rotate(Time.deltaTime * VAl.x, Time.deltaTime * VAl.y, Time.deltaTime * VAl.z);
        //}
        transform.RotateAround(this.transform.position, val, speed * Time.deltaTime);

    }
}
