using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnableContent : MonoBehaviour {

    public GameObject MObj;
    public static EnableContent mee;
	// Use this for initialization
	void Awake () {
        mee = this;

           //SceneManager.LoadScene("AllLevels", LoadSceneMode.Additive);

    }

    // Update is called once per frame
    public void EnableThings () {
        MObj.SetActive(true);
     

    }
}
