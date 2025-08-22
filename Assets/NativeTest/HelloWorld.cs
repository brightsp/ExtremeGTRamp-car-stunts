using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelloWorld : MonoBehaviour {

    Text textObj;
    public Text M_textObj;

	void Start () {

        // Find the Text UI object attached to the game object this script is attached to
        textObj = gameObject.GetComponent<Text>();

        // Make the call using JNI to the Java Class and write out the response (or write 'Invalid Response From JNI' if there was a problem).
        textObj.text = CaptiveReality.Jni.Util.StaticCall<string>("sayHello", "Invalid Response From JNI", "com.captivereality.texturehelper.HelloWorld");

        //M_textObj.text = CaptiveReality.Jni.Util.StaticCall<bool>("verifyInstallerId", true, "com.captivereality.texturehelper.HelloWorld");



        //AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
       // AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
       // AndroidJavaClass pluginClass = new AndroidJavaClass("com.mycompany.product.UnityPlugin");
       // pluginClass.CallStatic("initialize", new object[1] { activity });

        

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
        AndroidJavaClass pluginClass = new AndroidJavaClass("com.captivereality.texturehelper.HelloWorld");

        if (pluginClass == null)
        {
            M_textObj.text=("plugin class is null");
        }

       // M_textObj.text =  pluginClass.CallStatic("verifyInstallerId", context);

        M_textObj.text = CaptiveReality.Jni.Util.StaticCall<string>("verifyInstallerId", "No data", "com.captivereality.texturehelper.HelloWorld");


    }

}
