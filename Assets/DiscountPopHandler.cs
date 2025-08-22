using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscountPopHandler : MonoBehaviour
{
    public static DiscountPopHandler _instance;
    public GameObject D_pop;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    public void OpenPop()
    {

        D_pop.SetActive(true);


    }
    public void Close()
    {

        D_pop.SetActive(false);
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
