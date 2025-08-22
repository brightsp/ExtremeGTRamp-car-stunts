using System.Collections;
using System.Collections.Generic;
//using System.Security.Policy;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelContainer : MonoBehaviour
{

    public static LevelContainer mee;
    public GameObject playerpos;
    public GameObject[] AllLevels, Levelend_effect;
   

    // Use this for initialization
    private void OnEnable()
    {
       
        Invoke("Dealy",0.5f);

    }

    void Dealy()
    {
        // Debug.LogError("InDelay");
    }
    void Start()
    {
       // Debug.LogError("Instart");
        mee = this;
        //DontDestroyOnLoad(gameObject);
        //  Application.LoadLevel(2);

        //   SceneManager.LoadScene("FinalScene", LoadSceneMode.Additive);

        EnableContent.mee.EnableThings();

       // MobileEditorControlsHandler.mee.AutoChange();
    }

    // Update is called once per frame
    public void Disablelevels()
    {
        for (int i = 0; i < 20; i++)
        {
            AllLevels[i].SetActive(false);

        }
    }
}
