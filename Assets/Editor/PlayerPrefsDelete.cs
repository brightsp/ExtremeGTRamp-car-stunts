using UnityEngine;
using System.Collections;
using UnityEditor;

public class PlayerPrefsDelete : EditorWindow {

	[MenuItem("Playerpref/PlayerPrefs/Delete %Q")]
    public static void DeletePrefs() {
        PlayerPrefs.DeleteAll();
        EditorUtility.DisplayDialog("SVS","PlayerPrefs deleted successfully","Ok");
    }

}
