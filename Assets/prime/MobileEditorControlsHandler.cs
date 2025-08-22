using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileEditorControlsHandler : MonoBehaviour {

	private RCC_Settings rccSettings;
	public static MobileEditorControlsHandler mee;

    private void Start()
    {
		//AutoChange ();
		HideControls();

	}
    void Awake(){
		mee = this;
		rccSettings = Resources.Load ("RCC Assets/RCC_Settings") as RCC_Settings;
        Debug.Log("rcc settings : "+rccSettings);
		//HideControls();
	}


	bool changeIndex;

	public void HideControls()
	{

		Debug.Log("@@@ change index : " + changeIndex);


		rccSettings.controllerType = RCC_Settings.ControllerType.Keyboard;
		//rccSettings.controllerSelectedIndex = 0;
	}

	public void ShowControls()
	{
		rccSettings.controllerType = RCC_Settings.ControllerType.Mobile;
		//rccSettings.controllerSelectedIndex = 1;
	}
	public void AutoChange(){
		Debug.Log ("change index : "+changeIndex);
		changeIndex = !changeIndex;

		

#if UNITY_EDITOR
		rccSettings.controllerType = RCC_Settings.ControllerType.Keyboard;
		//rccSettings.controllerSelectedIndex = 0;
#elif UNITY_ANDROID
		rccSettings.controllerType = RCC_Settings.ControllerType.Mobile;
       // rccSettings.controllerSelectedIndex = 1;

		
	//	RCC.SetController(1);
#endif


	}
}
