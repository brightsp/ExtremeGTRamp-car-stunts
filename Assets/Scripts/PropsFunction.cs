using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsFunction : MonoBehaviour
{
    public enum PropType {none,rotation,position,scale};


    public enum Axis {x,y,z};


    public PropType propType = PropType.none;


    public Axis axis = Axis.x;


    public iTween.EaseType type=iTween.EaseType.easeOutBack;


    public iTween.LoopType looptype = iTween.LoopType.none;


    [SerializeField] float starAfter, axisValue,timeValue,delayValue;
   
    void OnEnable()
    {

        Invoke(propType.ToString(), starAfter);
        
    }

    void rotation()
    {
        iTween.RotateBy(gameObject, iTween.Hash(axis.ToString(), axisValue, "easeType", type, "loopType", "pingPong", "time", timeValue, "delay", delayValue));
    }
    void position()
    {
        iTween.MoveBy(gameObject, iTween.Hash(axis.ToString(), axisValue, "easeType", type, "loopType", "pingPong", "time", timeValue, "delay", delayValue));
    }
    void scale()
    {
        iTween.ScaleTo(gameObject, iTween.Hash(axis.ToString(), axisValue, "easeType", type,  "time", timeValue, "delay", delayValue));

    }
    void none()
    {

    }
}
