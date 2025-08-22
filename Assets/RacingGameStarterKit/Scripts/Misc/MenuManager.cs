using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//The MenuManager handles all menu activity. Feel free to extend it, learn from it or even use it in your own menu.
//Please note that the menu manager was intended for demo purposes.

namespace RGSK
{
    public class MenuManager : MonoBehaviour
    {

        public static MenuManager mee;
        //		public Texture[] BikeMaterial;
        //		public Texture[] PersonMaterial;
        [System.Serializable]
        public class MenuVehicle
        {
            [Header("Details")]
            public string name;
            public string resourceName; // Make sure this string matches the coresponding vehicle name in a Reources/PlayerVehicles folder!
            public Transform vehicle;
            public int price;
            public bool unlocked;
            public Material PersonBody;
            public Material vehicleBody;
            public Material VehicleRims;


            [Header("Specs")]
            [Range(0, 1)]
            public float speed;
            [Range(0, 1)]
            public float acceleration;
            [Range(0, 1)]
            public float handling;
            [Range(0, 1)]
            public float braking;



        }
        //		public int[] BikeMaxSpeed;
        //		public float[] BikeNitroFillrate;


        [System.Serializable]
        public class MenuTrack
        {
            public string name;
            public string trackLength;
            public string sceneName;
            public Sprite image;
            //            public RaceManager.RaceType raceType = RaceManager.RaceType.Circuit;
            //            public OpponentControl.AiDifficulty aiDifficulty = OpponentControl.AiDifficulty.Meduim;
            public int laps = 3;
            public int aiCount = 4;
            public int price;
            public bool unlocked;
        }

        #region Customization Classes
        [System.Serializable]
        public class CustomizeItem
        {
            public string name;
            public int ID;
            public int price;
            public Text priceText;
            [HideInInspector] public bool unlocked;
        }

        [System.Serializable]
        public class VisualUpgrade : CustomizeItem
        {
            public BodyColorAndRims[] visualUpgrade;
        }

        [System.Serializable]
        public class BodyColorAndRims
        {
            public string vehicle_name;
            public Texture texture;
        }


        [System.Serializable]
        public class VehicleUpgrade
        {
            public string vehicle_name;
            [Space(10)]
            [Range(0, 1)]
            public float speed;
            [Range(0, 1)]
            public float acceleration;
            [Range(0, 1)]
            public float handling;
            [Range(0, 1)]
            public float braking;
        }
        #endregion

        public enum State { Main, Settings, TrackSelect, VehicleSelect, Loading, Customize, store }
        public State state;
        public State Laststate;

        [Header("Vehicle settings")]
        public MenuVehicle[] menuVehicles;
        //		public GameObject[] EuroTrucksArray;


        [Header("Track Settings")]
        public MenuTrack[] menuTracks;

        [Header("Customize Settings")]
        //        public VisualUpgrade[] bodyColors;
        //        public VisualUpgrade[] rims;

        [Header("Panels")]
        public GameObject mainPanel;
        public GameObject vehicleSelectPanel;
        public GameObject trackSelectPanel;
        public GameObject customizePanel, vehicleStats;
        public GameObject settingsPanel;
        public GameObject promptPanel;
        public GameObject loadingPanel;
        public GameObject StorePageObj;

        [Header("Top Panel UI")]
        public Text playerCurrency;
        public Text menuState;

        [Header("Vehicle Select UI")]
        public Text vehicleName;
        public Button selectVehicleButton, buyVehicleButton, customizeButton;
        public Image speed, accel, handling, braking;
        public GameObject UnlockAllCarsBtn;

        [Header("Track Select UI")]
        public Image trackImage;
        public Text trackName, raceType, lapCount, aiCount, Levelrewardtxt, bestTime, trackLength;
        public Button raceButton, buyTrackButton;
        public GameObject UnlockAllLevelsBtn;

        [Header("Customization UI")]
        public Button apply;
        public Button buy;
        public GameObject colorsPanel;
        private int incartCr, bodyColPrice, rimPrice, upgradePrice, selectedColorID, selectedRimID, selectedUpgradeID;

        [Header("Settings UI")]
        public InputField playerName;
        public Slider masterVolume;
        public Dropdown graphicLevel;
        public Toggle mobileTouchSteer, mobileTiltSteer, mobileAutoAcceleration, mobileSteeringWheel;
        public bool applyExpensiveGraphicChanges = false;

        [Header("Loading UI")]
        public Image loadingBar;

        [Header("Prompt Panel UI")]
        public Text promptTitle;
        public Text promptText;
        public Button accept, cancel;

        [Header("Misc UI")]
        public Text itemPrice;
        public Image locked;
        public Image cart;
        public Button nextArrow, prevArrow;

        [Header("Extra Settings")]
        public bool autoRotateVehicles = true;
        public bool rotateVehicleByDrag = true;
        public float rotateSpeed = 5.0f;
        public int maxOpponents = 5;
        [Range(1, 7)] public int raceTypes = 7;

        //Private vars
        public static int vehicleIndex;
        private int prevVehicleIndex;
        public int trackIndex;
        private int raceTypeIndex = 1;
        private int aiDiffIndex = 1;
        private AsyncOperation async;
        private State previousState;
        private bool raycastTarget;
        private bool _autoRotate; // cache
        private float rotateDir = 1;
        private Texture lastColTex;
        private Texture lastRimTex;

        public static int CurrentLevel = 1;
        public static int CurrentLevelReward = 1;
        public static bool ComingForLevels = false;
        public static bool ComingForUpgrade = false;

        public string[] DummyVehicleNames;
        public Text DummyVehicleNameText;

        public int LevelsCount, CarsCount;

        public static string UnlockAllLevelsPopupPrefs = "UnlockAllLevelsPopup";
        public static string UnlockAllCarsPopupPrefs = "UnlockAllCarsPopup";
        public static string OnlyOneTime_LSPrefs = "ShowOnlyOneTime_LS";
        public static string OnlyOneTime_UpgPrefs_lvl3 = "ShowOnlyOneTime_Upg_Lvl3";
        public static string OnlyOneTime_UpgPrefs_lvl5 = "ShowOnlyOneTime_Upg_Lvl5";


        public static string UnlockAllLevelsCount = "UnlockAllLevelsCount";
        public static string UnlockAllCarsCount = "UnlockAllCarsCount";

        public bool AllCarsUnlocked, AllLevelsUnlocked;

        public static string Tutorial = "Tutorial";
        public static string EnterPlayerName = "EnterPlayerName";

        public GameObject EnterPlayerNamePopup;
        void Awake()
        {
            PlayerPrefs.SetInt("UnlockedLevels", 20);

            //PlayerPrefs.DeleteAll ();
            //			for (int i = 1; i < MenuManager.mee.menuVehicles.Length; i++) {
            //				PlayerPrefs.SetInt ("EuroTruck_"+i, 1);
            //				if(MenuManager.mee){
            //					MenuManager.mee.menuVehicles[i].unlocked = true;
            //
            //				}
            //			}



            LoadValues();
        }


        public Image screenFade;
        public float fadeSpeed = 0.5f;
        public bool fadeOnStart = true;
        public bool fadeOnExit = true;
        int count;
        void Start()
        {


            mee = this;
            if (fadeOnStart && screenFade) StartCoroutine(ScreenFadeOut(fadeSpeed));

            //PlayerPrefs.DeleteAll ();
            state = State.Main;
            //			GameConfigs2018.mee.showRotationAds (MenuManager.CurrentLevel,AdsPageType.menu);
            vehicleIndex = PlayerPrefs.GetInt("SelectedVehicle");
            ////sr Debug.Log("aaa    " + vehicleIndex);
            CycleVehicles();

            if (masterVolume) masterVolume.onValueChanged.AddListener(delegate { SetMasterVolFromSlider(); });
            if (playerName) playerName.onEndEdit.AddListener(delegate { SetPlayerNameFromInputField(); });
            if (graphicLevel) graphicLevel.onValueChanged.AddListener(delegate { GetGrahicLevelFromDropdown(); });
            if (graphicLevel) graphicLevel.value = QualitySettings.GetQualityLevel();
            if (mobileAutoAcceleration) mobileAutoAcceleration.onValueChanged.AddListener(delegate { ToggleAutoAccel(); });
            _autoRotate = autoRotateVehicles;
            selectedColorID = -1;
            selectedRimID = -1;
            selectedUpgradeID = -1;

            //
            //			for (int i = 1; i < 16; i++) {
            //				PlayerPrefs.SetInt ("EuroTruck_" + i, 1);
            //				//sr Debug.Log ("vehicles unlocked::"+PlayerPrefs.GetInt("EuroTruck_" + i));
            //			}


            if (PlayerPrefs.HasKey("levelss0") == false)
            {


                PlayerPrefs.SetInt(("levelss" + 0), 30);
                for (int i = 1; i < 30; i++)
                {

                    PlayerPrefs.SetInt(("levelss" + i), 0);
                }
            }



            for (int i = 1; i < PlayerPrefs.GetInt(("UnlockedLevels")); i++)
            {

                PlayerPrefs.SetInt(("levelss" + i), 1);
            }

            //sr Debug.Log(" ----unlocked " + PlayerPrefs.GetInt(("UnlockedLevels")));
            if (PlayerPrefs.GetInt("UnlockedLevels") == 30)
            {
                UnlockAllLevelsBtn.SetActive(false);
            }


            if (ComingForLevels)
            {
                TrackSelect();
                ComingForLevels = false;
                trackIndex = CurrentLevel;
                CycleTracks();
            }
            if (ComingForUpgrade)
            {
                VehicleSelect();
                ComingForUpgrade = false;
                CycleTracks();
                //				trackIndex = PlayerPrefs.GetInt (("UnlockedLevels"))-1;
                CurrentLevel = Levelmanager.RetryLevelNum;
                //sr Debug.Log("Unlocked Levels::" + trackIndex);

            }



            if (!PlayerPrefs.HasKey(OnlyOneTime_LSPrefs))
            {
                PlayerPrefs.SetInt(OnlyOneTime_LSPrefs, 1);
            }
            if (!PlayerPrefs.HasKey(OnlyOneTime_UpgPrefs_lvl3))
            {
                PlayerPrefs.SetInt(OnlyOneTime_UpgPrefs_lvl3, 1);
            }

            if (!PlayerPrefs.HasKey(OnlyOneTime_UpgPrefs_lvl5))
            {
                PlayerPrefs.SetInt(OnlyOneTime_UpgPrefs_lvl5, 1);
            }
            if (!PlayerPrefs.HasKey(Tutorial))
            {
                PlayerPrefs.SetInt(Tutorial, 0);
            }
            if (!PlayerPrefs.HasKey(EnterPlayerName))
            {
                PlayerPrefs.SetInt(EnterPlayerName, 0);
            }
            //sr Debug.Log("Enter Player Name::" + PlayerPrefs.GetInt(EnterPlayerName));
            //			if (PlayerPrefs.GetInt (EnterPlayerName) == 0)
            //			{
            //				EnterPlayerNamePopup.SetActive (true);
            //			}
        }


        void nofun()
        {

        }
        public IEnumerator ScreenFadeInOut(float speed, string MyFun = "nofun")
        {
            //Get the color
            Color col = screenFade.color;
            if (col.a > 0.0f) yield break;

            //Change the alpha to 1
            //col.a = 1;
            //screenFade.color = col;


            //Fade in
            while (col.a < 1.0f)
            {
                col.a += Time.deltaTime * speed;
                screenFade.color = col;
                yield return null;

                //Load the menu scene when fade completes
                //if (col.a >= 1.0f)
                //	SceneManager.LoadScene(scene);
            }
            //MyFun ();
            Invoke(MyFun, 0.01f);
            yield return new WaitForSeconds(0.3f);
            //Fade out
            while (col.a > 0.0f)
            {
                col.a -= Time.deltaTime * speed;
                screenFade.color = col;
                yield return null;
            }
        }

        public IEnumerator ScreenFadeOut(float speed)
        {
            //Get the color
            Color col = screenFade.color;
            if (col.a > 0.0f) yield break;

            //Change the alpha to 1
            col.a = 1;
            screenFade.color = col;

            //Fade out
            while (col.a > 0.0f)
            {
                col.a -= Time.deltaTime * speed;
                screenFade.color = col;
                yield return null;
            }
        }

        public IEnumerator ScreenFadeIn(float speed, bool loadScene, string scene)
        {
            //Get the color
            Color col = screenFade.color;

            //Change the alpha to 0
            col.a = 0;
            screenFade.color = col;

            //Fade in
            while (col.a < 1.0f)
            {
                col.a += Time.deltaTime * speed;
                screenFade.color = col;
                yield return null;

                //Load the menu scene when fade completes
                if (col.a >= 1.0f)
                    SceneManager.LoadScene(scene);
            }

        }

        void Update()
        {

            //            if (Input.GetKeyDown(KeyCode.Escape)) Back();

            RotateVehicle();

            LerpStats();
        }
        void CycleVehicles()
        {
            //sr Debug.Log("previous vehicle index::" + prevVehicleIndex);
            // Cycle between vehicles based on the "vehicleIndex" value
            for (int i = 0; i < menuVehicles.Length; i++)
            {
                if (vehicleIndex == i)
                {
                    menuVehicles[i].vehicle.rotation = menuVehicles[prevVehicleIndex].vehicle.rotation;
                    menuVehicles[i].vehicle.gameObject.SetActive(true);
                    iTween.MoveTo(menuVehicles[i].vehicle.gameObject, iTween.Hash("x", -0.03f, "y", -0.11f, "z", 0.81f, "time", 0.5, "delay", 0.5f));

                    //					menuVehicles [0].vehicleBody.mainTexture=BikeMaterial [i];//(menuVehicles [0].BikeMaterial [i]);// =menuVehicles [0].BikeMaterial [i];
                    //					menuVehicles [0].PersonBody.mainTexture= PersonMaterial [i];//(menuVehicles [0].BikeMaterial [i]);// =menuVehicles [0].BikeMaterial [i];
                    //
                    //					iTween.RotateFrom (menuVehicles [0].vehicle.transform.GetChild(0).gameObject,iTween.Hash("y",(menuVehicles [0].vehicle.transform.GetChild(0).transform.localRotation.x+50),"time",0.6,"easetype",iTween.EaseType.linear));
                    //					iTween.MoveFrom (menuVehicles [0].vehicle.gameObject,iTween.Hash("y",-6));
                    UpdateUI();
                }
                else
                {
                    iTween.MoveTo(menuVehicles[i].vehicle.gameObject, iTween.Hash("x", 11.2f, "y", -0.11f, "z", 0.81f, "time", 0.5));
                    //                    menuVehicles[i].vehicle.gameObject.SetActive(false);
                    //					Invoke("EuroTrucksDisable",0.6f);
                }
            }
        }

        void EuroTrucksDisable()
        {
            menuVehicles[prevVehicleIndex].vehicle.gameObject.SetActive(false);
        }
        void CycleTracks()
        {
            // Cycle between tracks based on the "trackIndex" value

            UpdateUI();
        }

        public void UpdateUI()
        {

            if (playerCurrency) playerCurrency.text = " " + PlayerData.currency.ToString("N0");

            if (cart) cart.enabled = state == State.Customize;

            if (nextArrow) nextArrow.gameObject.SetActive(state == State.VehicleSelect || state == State.TrackSelect);

            if (prevArrow) prevArrow.gameObject.SetActive(state == State.VehicleSelect || state == State.TrackSelect);

            if (vehicleStats) vehicleStats.SetActive(state == State.VehicleSelect || state == State.Customize);

            switch (state)
            {

                case State.Main:


                    mainPanel.SetActive(true);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    if (itemPrice) itemPrice.text = string.Empty;

                    if (locked) locked.enabled = false;

                    break;

                case State.VehicleSelect:


                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(true);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);



                    //sr Debug.Log(vehicleIndex + "  vehicle ");
                    if (vehicleName)
                        vehicleName.text = menuVehicles[vehicleIndex].resourceName;
                    //				if (DummyVehicleNameText)
                    //sr Debug.Log("Vehiclename is::" + DummyVehicleNames[vehicleIndex]);
                    DummyVehicleNameText.text = DummyVehicleNames[vehicleIndex];
                    if (itemPrice) itemPrice.text = menuVehicles[vehicleIndex].unlocked ? string.Empty : " " + menuVehicles[vehicleIndex].price.ToString("N0");

                    if (locked) locked.enabled = !menuVehicles[vehicleIndex].unlocked;

                    if (selectVehicleButton) selectVehicleButton.gameObject.SetActive(menuVehicles[vehicleIndex].unlocked);

                    if (buyVehicleButton) buyVehicleButton.gameObject.SetActive(!menuVehicles[vehicleIndex].unlocked);

                    if (customizeButton) customizeButton.gameObject.SetActive(menuVehicles[vehicleIndex].unlocked);

                    if (menuState) menuState.text = "VEHICLE SELECT";

                    break;

                case State.TrackSelect:

                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(true);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);
                    //sr Debug.Log(trackIndex + " locked :: " + menuTracks[trackIndex].unlocked);
                    if (trackName)
                        trackName.text = menuTracks[trackIndex].name;

                    if (trackLength)
                        trackLength.text = "Level " + (trackIndex + 1);//menuTracks [trackIndex].trackLength;

                    if (trackImage && menuTracks[trackIndex].image)
                        trackImage.sprite = menuTracks[trackIndex].image;

                    //				if (raceType)
                    //					raceType.text = menuTracks [trackIndex].raceType.ToString ();

                    if (lapCount)
                        lapCount.text = menuTracks[trackIndex].laps.ToString();

                    if (aiCount)
                        aiCount.text = menuTracks[trackIndex].aiCount.ToString();

                    if (Levelrewardtxt)
                        Levelrewardtxt.text = " " + menuTracks[trackIndex].price.ToString();

                    //if (itemPrice)
                    //	itemPrice.text = menuTracks [trackIndex].unlocked ? string.Empty : "$ "+menuTracks [trackIndex].price.ToString ("N0") ;

                    if (locked)
                        locked.enabled = !menuTracks[trackIndex].unlocked;

                    if (raceButton)
                        raceButton.gameObject.SetActive(menuTracks[trackIndex].unlocked);

                    if (buyTrackButton)
                    {
                        buyTrackButton.gameObject.SetActive(!menuTracks[trackIndex].unlocked);

                    }

                    if (bestTime)
                    {
                        //					//sr Debug.LogWarning ("BestLap time::"+menuTracks [trackIndex].unlocked);
                        if (menuTracks[trackIndex].unlocked == true)
                        {
                            bestTime.text = PlayerPrefs.GetString("BestTime" + menuTracks[trackIndex].sceneName);
                        }
                        else
                        {
                            bestTime.text = "--:--:--";
                        }
                        //					bestTime.text = (PlayerPrefs.HasKey ("BestTime" + menuTracks [trackIndex].sceneName)) ? PlayerPrefs.GetString ("BestTime" + menuTracks [trackIndex].sceneName) : "--:--:--";

                    }

                    if (menuState)
                        menuState.text = "TRACK SELECT";


                    if (menuTracks[trackIndex].unlocked)
                    {

                        CurrentLevel = trackIndex + 1;
                        CurrentLevelReward = menuTracks[trackIndex].price;
                        ////sr Debug.Log ("selected level "+CurrentLevel);
                    }

                    break;

                case State.Customize:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    //                    customizePanel.SetActive(true);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(false);

                    //Calculate the in cart currency
                    incartCr = bodyColPrice + rimPrice + upgradePrice;

                    //Fill in the price texts (BODY COLORS)
                    //                    for (int c = 0; c < bodyColors.Length; c++)
                    //                    {
                    //                        if (bodyColors[c].priceText) bodyColors[c].priceText.text = !bodyColors[c].unlocked ? bodyColors[c].price.ToString("N0") : "Owned";
                    //                    }

                    //Fill in the price texts (RIMS)
                    //                    for (int r = 0; r < rims.Length; r++)
                    //                    {
                    //                        if (rims[r].priceText) rims[r].priceText.text = !rims[r].unlocked ? rims[r].price.ToString("N0") : "Owned";
                    //                    }

                    if (colorsPanel) colorsPanel.SetActive(true);

                    if (apply) apply.gameObject.SetActive(incartCr <= 0 && selectedColorID >= 0 || incartCr <= 0 && selectedRimID >= 0 || incartCr <= 0 && selectedUpgradeID >= 0);

                    if (buy) buy.gameObject.SetActive(incartCr > 0);

                    if (itemPrice) itemPrice.text = " " + incartCr.ToString("N0");

                    if (menuState) menuState.text = "CUSTOMIZE";

                    break;

                case State.Settings:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(true);
                    loadingPanel.SetActive(false);

                    if (menuState) menuState.text = "SETTINGS";

                    break;

                case State.Loading:
                    mainPanel.SetActive(false);
                    vehicleSelectPanel.SetActive(false);
                    trackSelectPanel.SetActive(false);
                    customizePanel.SetActive(false);
                    settingsPanel.SetActive(false);
                    loadingPanel.SetActive(true);

                    break;
            }
        }

        /// <summary>
        /// Lerps the stat values to suit the selected vehicle
        /// </summary>
        private void LerpStats()
        {
            //Normal Stats
            if (speed) speed.fillAmount = Mathf.Lerp(speed.fillAmount, menuVehicles[vehicleIndex].speed, Time.deltaTime * 3.0f);

            if (accel) accel.fillAmount = Mathf.Lerp(accel.fillAmount, menuVehicles[vehicleIndex].acceleration, Time.deltaTime * 3.0f);

            if (handling) handling.fillAmount = Mathf.Lerp(handling.fillAmount, menuVehicles[vehicleIndex].handling, Time.deltaTime * 3.0f);

            if (braking) braking.fillAmount = Mathf.Lerp(braking.fillAmount, menuVehicles[vehicleIndex].braking, Time.deltaTime * 3.0f);
        }


        private void RotateVehicle()
        {
            if (autoRotateVehicles) menuVehicles[vehicleIndex].vehicle.Rotate(0, (rotateSpeed * Time.deltaTime) * rotateDir, 0);


            //Rotate by drag raycast check
            if (rotateVehicleByDrag)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        Collider[] childTransforms = menuVehicles[vehicleIndex].vehicle.GetComponentsInChildren<Collider>();

                        foreach (Collider t in childTransforms)
                        {
                            if (hit.collider == t)
                            {
                                autoRotateVehicles = false;
                                raycastTarget = true;
                            }
                            else
                            {
                                raycastTarget = false;
                                if (_autoRotate) autoRotateVehicles = true;
                            }
                        }
                    }
                }

                if (Input.GetButtonUp("Fire1"))
                {
                    Vector3 mPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

                    if (raycastTarget) rotateDir = (mPos.x < 0.5f) ? 1 : -1;

                    if (_autoRotate) autoRotateVehicles = true;

                    raycastTarget = false;
                }

                if (!raycastTarget) return;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL

                menuVehicles[vehicleIndex].vehicle.Rotate(0, -Input.GetAxis("Mouse X"), 0);

#else
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 fingerPos = Input.GetTouch(0).deltaPosition;

                    menuVehicles[vehicleIndex].vehicle.Rotate(0, -fingerPos.x, 0);
                }
#endif
            }
        }

        //        private void ApplyColorCustomization(int bodyCol, int tovehicleIndex)
        //        {
        //
        //            bodyColors[bodyCol].unlocked = true;
        //
        //            //Unclock the color
        //            if (!PlayerPrefs.HasKey("BodyColor" + bodyColors[bodyCol].ID + menuVehicles[tovehicleIndex].resourceName))
        //                PlayerPrefs.SetInt("BodyColor" + bodyColors[bodyCol].ID + menuVehicles[tovehicleIndex].resourceName, 1);
        //
        //            //Save as the vehicle's current color
        //            PlayerPrefs.SetInt("CurrentBodyColor" + menuVehicles[tovehicleIndex].resourceName, bodyCol);
        //
        //            try { menuVehicles[tovehicleIndex].vehicleBody.mainTexture = bodyColors[bodyCol].visualUpgrade[tovehicleIndex].texture; }
        //            catch { //sr Debug.LogError("You haven't properly configured color customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + bodyCol + "] of this customization exists or isn't null."); }
        //
        //            lastColTex = null;
        //        }

        //        private void ApplyRimCustomization(int rimIndex, int tovehicleIndex)
        //        {
        //
        //            rims[rimIndex].unlocked = true;
        //
        //            if (!PlayerPrefs.HasKey("VehicleRim" + rims[rimIndex].ID + menuVehicles[vehicleIndex].resourceName))
        //                PlayerPrefs.SetInt("VehicleRim" + rims[rimIndex].ID + menuVehicles[vehicleIndex].resourceName, 1);
        //
        //            //Save as the vehicle's current rim
        //            PlayerPrefs.SetInt("CurrentRim" + menuVehicles[tovehicleIndex].resourceName, rimIndex);
        //
        //            try { menuVehicles[tovehicleIndex].VehicleRims.mainTexture = rims[rimIndex].visualUpgrade[tovehicleIndex].texture; }
        //            catch { //sr Debug.LogError("You haven't properly configured rim customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + rimIndex + "] of this customization exists or isn't null."); }
        //
        //
        //            lastRimTex = null;
        //        }

        /// <summary>
        /// Loads important values such as currency & preferences
        /// </summary>
        private void LoadValues()
        {
            PlayerData.LoadCurrency();

            //Last selected vehicle
            if (PlayerPrefs.HasKey("SelectedVehicle")) vehicleIndex = PlayerPrefs.GetInt("SelectedVehicle");

            //Master Vol
            if (masterVolume) masterVolume.value = (PlayerPrefs.HasKey("MasterVolume")) ? PlayerPrefs.GetFloat("MasterVolume") : 1;

            //Graphic Level
            if (PlayerPrefs.HasKey("GraphicLevel"))
            {

                if (SystemInfo.graphicsMemorySize > 600)
                {

                    PlayerPrefs.SetInt("GraphicLevel", 2);
                }
                else if (SystemInfo.graphicsMemorySize > 256)
                {

                    PlayerPrefs.SetInt("GraphicLevel", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("GraphicLevel", 0);


                }


                SetGraphicsQuality(PlayerPrefs.GetInt("GraphicLevel"));
            }

            //Player Name
            if (PlayerPrefs.HasKey("PlayerName")) { if (playerName) playerName.text = PlayerPrefs.GetString("PlayerName"); }

            //Toggles
            if (mobileAutoAcceleration) mobileAutoAcceleration.isOn = PlayerPrefs.GetString("AutoAcceleration") == "True";
            if (mobileTouchSteer) mobileTouchSteer.isOn = PlayerPrefs.GetString("MobileControlType") == "Touch";
            if (mobileTiltSteer) mobileTiltSteer.isOn = PlayerPrefs.GetString("MobileControlType") == "Tilt";
            if (mobileSteeringWheel) mobileSteeringWheel.isOn = PlayerPrefs.GetString("MobileControlType") == "Steer";

            //Other important stuff
            CheckForUnlockedVehiclesAndTracks();
            //            LoadCustomizations();

        }

        //        private void LoadCustomizations()
        //        {
        //            for (int i = 0; i < menuVehicles.Length; i++)
        //            {
        //                if (PlayerPrefs.HasKey("CurrentBodyColor" + menuVehicles[i].resourceName))
        //                {
        //                    ApplyColorCustomization(PlayerPrefs.GetInt("CurrentBodyColor" + menuVehicles[i].resourceName), i);
        //                }
        //
        //                if (PlayerPrefs.HasKey("CurrentRim" + menuVehicles[i].resourceName))
        //                {
        //                    ApplyRimCustomization(PlayerPrefs.GetInt("CurrentRim" + menuVehicles[i].resourceName), i);
        //                }
        //            }
        //        }
        public void UnlockAllLevelsBtnClicked()
        {
            //			Btm_IABManager.mee.BUY (2);
            //			PlayerPrefs.SetInt (("UnlockedLevels"), 30);
            //
            //			if (MenuManager.mee) {
            //				MenuManager.mee.CheckForUnlockedVehiclesAndTracks ();
            //				MenuManager.mee.UpdateUI ();
            //				MenuManager.mee.UnlockAllLevelsBtn.SetActive (false);
            //			}
            //			PlayerPrefs.SetInt (MenuManager.UnlockAllLevelsPopupPrefs, 1);
            //			MenuManager.mee.AllLevelsUnlocked=true;
        }
        public void UnlockAllCarsBtnClicked()
        {
            //			Btm_IABManager.mee.BUY (1);

        }
        public void CheckForUnlockedVehiclesAndTracks()
        {

            //sr Debug.Log(menuTracks.Length + " Start----unlocked " + PlayerPrefs.GetInt(("UnlockedLevels")));

            if (PlayerPrefs.HasKey("UnlockedLevels") == false)
            {
                //sr Debug.Log(" ----unlocked -----");
                PlayerPrefs.SetInt(("UnlockedLevels"), 1);
            }
            //PlayerPrefs.DeleteAll ();
            //Check for unlokced vehicles
            for (int i = 0; i < menuVehicles.Length; i++)
            {
                //First check if the vehicle is pre-unlocked
                if (menuVehicles[i].unlocked)
                {
                    PlayerPrefs.SetInt(menuVehicles[i].resourceName, 1);
                }

                if (PlayerPrefs.GetInt(menuVehicles[i].resourceName) == 1)
                {
                    menuVehicles[i].unlocked = true;
                }
                else
                {
                    menuVehicles[i].unlocked = false;
                }
            }


            for (int i = 0; i < menuTracks.Length; i++)
            {
                if (i < PlayerPrefs.GetInt(("UnlockedLevels")))
                {
                    menuTracks[i].unlocked = true;
                }
                else
                {
                    menuTracks[i].unlocked = false;
                }
            }
            //Check for unlokced tracks
            //            for (int i = 0; i < menuTracks.Length; i++)
            //            {
            //			//	//sr Debug.Log (PlayerPrefs.GetInt(("levelss"+i))+"  :: trackk "+i);
            //                //First check if the track is pre-unlocked
            //                if (menuTracks[i].unlocked)
            //                {
            //                   // PlayerPrefs.SetInt(menuTracks[i].name, 1);
            //					PlayerPrefs.SetInt(("levelss"+i), 1);
            //                }
            //
            //				if (PlayerPrefs.GetInt(("levelss"+i)) == 1)
            //                {
            //                    menuTracks[i].unlocked = true;
            //                }
            //                else
            //                {
            //                    menuTracks[i].unlocked = false;
            //                }
            //            }

            //sr Debug.Log("startLoad");
        }

        //        private void CheckForUnlockedCustomizations()
        //        {
        //            for (int i = 0; i < bodyColors.Length; i++)
        //            {
        //                if (PlayerPrefs.GetInt("BodyColor" + bodyColors[i].ID + menuVehicles[vehicleIndex].resourceName) == 1)
        //                {
        //                    bodyColors[i].unlocked = true;
        //                }
        //                else
        //                {
        //                    bodyColors[i].unlocked = false;
        //                }
        //            }
        //
        //            for (int i = 0; i < rims.Length; i++)
        //            {
        //                if (PlayerPrefs.GetInt("VehicleRim" + rims[i].ID + menuVehicles[vehicleIndex].resourceName) == 1)
        //                {
        //                    rims[i].unlocked = true;
        //                }
        //                else
        //                {
        //                    rims[i].unlocked = false;
        //                }
        //            }
        //        }

        private void RevertCustomizationChanges()
        {
            if (lastColTex && menuVehicles[vehicleIndex].vehicleBody) menuVehicles[vehicleIndex].vehicleBody.mainTexture = lastColTex;
            if (lastRimTex && menuVehicles[vehicleIndex].VehicleRims) menuVehicles[vehicleIndex].VehicleRims.mainTexture = lastRimTex;

            incartCr = 0;
            bodyColPrice = 0;
            rimPrice = 0;
            upgradePrice = 0;
            selectedColorID = -1;
            selectedRimID = -1;
            selectedUpgradeID = -1;
            lastColTex = null;
            lastRimTex = null;

            //for (int i = 0; i < bodyColors.Length; i++)
            //{
            //    bodyColors[i].unlocked = false;
            //}
        }

        private void CreatePromptPanel(string title, string prompt)
        {

            if (promptTitle) promptTitle.text = title;

            if (promptText) promptText.text = prompt;

            if (promptPanel) promptPanel.SetActive(true);
        }

        private bool ObjClik = true;
        void resetClick()
        {
            ObjClik = true;

        }
        #region Button Functions

        public void NextArrow()
        {
            ButtonSFX();

            if (state == State.VehicleSelect && ObjClik)
            {
                if (vehicleIndex < menuVehicles.Length - 1)
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex++;
                }
                else
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex = 0;
                }


                CycleVehicles();
                Invoke("EuroTrucksDisable", 0.6f);
            }

            if (state == State.TrackSelect)
            {
                if (trackIndex < menuTracks.Length - 1)
                {
                    trackIndex++;
                }
                else
                {
                    trackIndex = 0;
                }

                CycleTracks();
            }
            Invoke("resetClick", 0.8f);
            ObjClik = false;
        }

        public void PreviousArrow()
        {
            ButtonSFX();

            if (state == State.VehicleSelect && ObjClik)
            {
                if (vehicleIndex > 0)
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex--;
                }
                else
                {
                    prevVehicleIndex = vehicleIndex;
                    vehicleIndex = menuVehicles.Length - 1;
                }

                CycleVehicles();
                Invoke("EuroTrucksDisable", 0.6f);
            }

            if (state == State.TrackSelect)
            {
                if (trackIndex > 0)
                {
                    trackIndex--;
                }
                else
                {
                    trackIndex = menuTracks.Length - 1;
                }

                CycleTracks();
            }
            Invoke("resetClick", 0.8f);
            ObjClik = false;

        }

        public void Play()
        {
            state = State.Loading;

            UpdateUI();

            PlayerPrefs.SetInt("SelectedVehicle", vehicleIndex);

            //sr Debug.Log(menuVehicles[vehicleIndex].resourceName + " Selected vehicle " + vehicleIndex);

            //Save all preferences
            PlayerPrefs.SetString("PlayerVehicle", menuVehicles[vehicleIndex].resourceName);
            //            PlayerPrefs.SetString("RaceType", menuTracks[trackIndex].raceType.ToString());
            //            PlayerPrefs.SetString("AiDifficulty", menuTracks[trackIndex].aiDifficulty.ToString());
            PlayerPrefs.SetInt("Opponents", menuTracks[trackIndex].aiCount);
            PlayerPrefs.SetInt("Laps", menuTracks[trackIndex].laps);

            PlayerPrefs.SetInt("MVehicleIndex", vehicleIndex);
            StartCoroutine(LoadScene());
        }

        void myfun()
        {
            //sr Debug.Log("myfun");
        }

        void myfun2()
        {
            //sr Debug.Log("myfun2");
        }
        public void Buy()
        {
            ButtonSFX();

            //BUY VEHILCE
            //sr Debug.Log("---- ");
            if (state == State.VehicleSelect)
            {
                if (PlayerData.currency >= menuVehicles[vehicleIndex].price)
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => AcceptPrompt());
                    }

                    if (cancel)
                    {
                        cancel.gameObject.SetActive(true);
                        cancel.onClick.RemoveAllListeners();
                        cancel.onClick.AddListener(() => ClosePromptPanel());
                    }

                    CreatePromptPanel("CONFIRM ACTION", "Do you really want to purchase this vehicle?");
                }
                else
                {
                    //sr Debug.Log("Open in app purchase UNLOCK ALL TRUCKS HERE...");
                   
                  //if ( AdManager.instance)
                  //  {

                        //sr AdManager.instance.BuyItem(1, true);
                   // }


                }
            }

            //BUY TRACK
            if (state == State.TrackSelect)
            {
                if (PlayerData.currency >= menuTracks[trackIndex].price)
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => AcceptPrompt());

                    }

                    if (cancel)
                    {
                        cancel.gameObject.SetActive(true);
                        cancel.onClick.RemoveAllListeners();
                        cancel.onClick.AddListener(() => ClosePromptPanel());
                    }

                    CreatePromptPanel("CONFIRM ACTION", "Do you really want to purchase this track?");
                }
                else
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => ClosePromptPanel());
                    }

                    if (cancel) cancel.gameObject.SetActive(false);

                    CreatePromptPanel("NOT ENOUGH CURRENCY", "You do not have enough currency to buy this track");
                }
            }


            //BUY CUSTOMIZATION
            if (state == State.Customize)
            {
                if (PlayerData.currency >= incartCr)
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => AcceptPrompt());
                    }

                    if (cancel)
                    {
                        cancel.gameObject.SetActive(true);
                        cancel.onClick.RemoveAllListeners();
                        cancel.onClick.AddListener(() => ClosePromptPanel());
                    }

                    CreatePromptPanel("CONFIRM ACTION", "Do you really want to make this purchase?");
                }
                else
                {
                    if (accept)
                    {
                        accept.onClick.RemoveAllListeners();
                        accept.onClick.AddListener(() => ClosePromptPanel());
                    }

                    if (cancel) cancel.gameObject.SetActive(false);

                    CreatePromptPanel("NOT ENOUGH CURRENCY", "You do not have enough currency to make this purchase");
                }
            }
        }

        void OpenStore()
        {
            StorePageObj.SetActive(true);
        }
        public void StorePage()
        {
            ButtonSFX();
            //sr Debug.Log(state.GetHashCode() + " state");
            Laststate = state;
            state = State.store;
            //sr Debug.Log(state + " state");
            if (fadeOnStart && screenFade)
            {
                StartCoroutine(ScreenFadeInOut(fadeSpeed, "OpenStore"));
            }
        }


        public void VehicleSelect()
        {
            ButtonSFX();

            state = State.VehicleSelect;
            if (fadeOnStart && screenFade)
            {
                StartCoroutine(ScreenFadeInOut(fadeSpeed, "UpdateUI"));
            }

            //			GameConfigs2018.mee.showRotationAds (MenuManager.CurrentLevel,AdsPageType.upgradeOrLevelSelection);

            //UpdateUI();
        }

        public void TrackSelect()
        {
            ButtonSFX();

            state = State.TrackSelect;
            trackIndex = PlayerPrefs.GetInt(("UnlockedLevels")) - 1;
            //sr Debug.Log("clevel " + PlayerPrefs.GetInt(("UnlockedLevels")));
            if (fadeOnStart && screenFade)
            {
                StartCoroutine(ScreenFadeInOut(fadeSpeed, "UpdateUI"));
            }

            //			GameConfigs2018.mee.showRotationAds (MenuManager.CurrentLevel,AdsPageType.upgradeOrLevelSelection);

            // UpdateUI();
        }

        public void Customize()
        {
            ButtonSFX();

            state = State.Customize;

            //            CheckForUnlockedCustomizations();

            UpdateUI();
        }

        public void Settings()
        {
            ButtonSFX();

            if (state != State.Settings) previousState = state;

            state = state != State.Settings ? State.Settings : previousState;

            UpdateUI();
        }

        public void SetGraphicsQuality(int level)
        {
            QualitySettings.SetQualityLevel(0, applyExpensiveGraphicChanges);

            PlayerPrefs.SetInt("GraphicLevel", level);
        }

        private void GetGrahicLevelFromDropdown()
        {
            SetGraphicsQuality(graphicLevel.value);
        }

        private void SetMasterVolFromSlider()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);

            if (SoundManager.instance)
                SoundManager.instance.SetVolume();
        }

        public void SetPlayerNameFromInputField()
        {
            PlayerPrefs.SetString("PlayerName", playerName.text);
        }

        public void ToggleTouchControl(bool b)
        {
            mobileTouchSteer.isOn = true;

            mobileTiltSteer.isOn = false;
            mobileSteeringWheel.isOn = false;

            PlayerPrefs.SetString("MobileControlType", "Touch");
        }

        public void ToggleTiltControl(bool b)
        {
            mobileTouchSteer.isOn = false;

            mobileTiltSteer.isOn = true;
            mobileSteeringWheel.isOn = false;


            PlayerPrefs.SetString("MobileControlType", "Tilt");
        }

        public void ToggleSteeringControl(bool b)
        {
            mobileSteeringWheel.isOn = true;
            mobileTouchSteer.isOn = false;
            mobileTiltSteer.isOn = false;

            PlayerPrefs.SetString("MobileControlType", "Steer");

        }

        public void ToggleAutoAccel()
        {
            string isOn = mobileAutoAcceleration.isOn ? "True" : "False";
            PlayerPrefs.SetString("AutoAcceleration", isOn);
        }

        public void ChooseVehicle()
        {
            PlayerPrefs.SetInt("SelectedVehicle", vehicleIndex);
            Back();
        }

        public void AdjustRaceType(int val)
        {
            raceTypeIndex += val;
            raceTypeIndex = Mathf.Clamp(raceTypeIndex, 1, raceTypes);

            switch (raceTypeIndex)
            {

                case 1:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Circuit;
                    break;

                case 2:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.LapKnockout;
                    break;

                case 3:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.TimeTrial;
                    break;

                case 4:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.SpeedTrap;
                    break;

                case 5:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Checkpoints;
                    break;

                case 6:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Elimination;
                    break;

                case 7:
                    //                    menuTracks[trackIndex].raceType = RaceManager.RaceType.Drift;
                    break;
            }

            UpdateUI();
        }

        public void AdjustLaps(int val)
        {
            menuTracks[trackIndex].laps += val;
            menuTracks[trackIndex].laps = Mathf.Clamp(menuTracks[trackIndex].laps, 1, 1000);

            UpdateUI();
        }

        public void AdjustAiCount(int val)
        {
            menuTracks[trackIndex].aiCount += val;
            menuTracks[trackIndex].aiCount = Mathf.Clamp(menuTracks[trackIndex].aiCount, 0, maxOpponents);

            UpdateUI();
        }

        public void AdjustAiDifficulty(int val)
        {

            aiDiffIndex += val;
            aiDiffIndex = Mathf.Clamp(aiDiffIndex, 1, 4);

            switch (aiDiffIndex)
            {

                case 1:
                    //                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Custom;
                    break;

                case 2:
                    //                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Easy;
                    break;
                case 3:
                    //				menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.EasyMedium;
                    break;

                case 4:
                    //                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Meduim;
                    break;

                case 5:
                    //                    menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.Hard;
                    break;

                case 6:
                    //				menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.veryHard1;
                    break;
                case 7:
                    //				menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.extremHard2;
                    break;
                case 8:
                    //				menuTracks[trackIndex].aiDifficulty = OpponentControl.AiDifficulty.TooHard3;
                    break;
            }

            UpdateUI();
        }

        //        public void SelectColor(int c)
        //        {
        //            ButtonSFX();
        //
        //            if (!menuVehicles[vehicleIndex].vehicleBody) return;
        //
        //            if (!lastColTex) lastColTex = menuVehicles[vehicleIndex].vehicleBody.mainTexture;
        //
        //            for (int i = 0; i < bodyColors.Length; i++)
        //            {
        //                if (c == bodyColors[i].ID)
        //                {
        //                    selectedColorID = i;
        //                    bodyColPrice = !bodyColors[selectedColorID].unlocked ? bodyColors[selectedColorID].price : 0;
        //
        //                    try { menuVehicles[vehicleIndex].vehicleBody.mainTexture = bodyColors[i].visualUpgrade[vehicleIndex].texture; }
        //                    catch { //sr Debug.Log("You haven't properly configured color customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + i + "] of this customization exists or isn't null."); }
        //                }
        //            }
        //
        //            UpdateUI();
        //        }

        //        public void SelectRim(int r)
        //        {
        //            ButtonSFX();
        //
        //            if (!menuVehicles[vehicleIndex].VehicleRims) return;
        //
        //            if (!lastRimTex) lastRimTex = menuVehicles[vehicleIndex].VehicleRims.mainTexture;
        //
        //            for (int i = 0; i < rims.Length; i++)
        //            {
        //                if (r == rims[i].ID)
        //                {
        //                    selectedRimID = i;
        //                    rimPrice = !rims[selectedRimID].unlocked ? rims[selectedRimID].price : 0;
        //
        //                    try { menuVehicles[vehicleIndex].VehicleRims.mainTexture = rims[i].visualUpgrade[vehicleIndex].texture; }
        //                    catch { //sr Debug.Log("You haven't properly configured rim customizations for this vehicle! Ensure you have assigned a material for your vehicle and the index [" + i + "] of this customization exists or isn't null."); }
        //                }
        //            }
        //
        //            UpdateUI();
        //        }

        //        public void ApplyCustomizationChanges()
        //        {
        //            if (selectedColorID >= 0) ApplyColorCustomization(selectedColorID, vehicleIndex);
        //
        //            if (selectedRimID >= 0) ApplyRimCustomization(selectedRimID, vehicleIndex);
        //
        //            Back();
        //        }

        public void AcceptPrompt()
        {
            switch (state)
            {
                case State.VehicleSelect:
                    PlayerData.DeductCurrency(menuVehicles[vehicleIndex].price);

                    menuVehicles[vehicleIndex].unlocked = true;
                    PlayerPrefs.SetInt(menuVehicles[vehicleIndex].resourceName, 1);
                    //sr Debug.Log("RESOURCE unlocked Name::" + menuVehicles[vehicleIndex].resourceName);
                    //sr Debug.Log("Cars Unlocked prefs::" + PlayerPrefs.GetInt(menuVehicles[vehicleIndex].resourceName));
                    CheckButton();

                    break;

                case State.TrackSelect:
                    PlayerData.DeductCurrency(menuTracks[trackIndex].price);

                    menuTracks[trackIndex].unlocked = true;
                    //PlayerPrefs.SetInt(menuTracks[trackIndex].name, 1);
                    PlayerPrefs.SetInt(("levelss" + trackIndex), 1);
                    break;

                case State.Customize:
                    PlayerData.DeductCurrency(incartCr);

                    //                    if (selectedColorID >= 0) ApplyColorCustomization(selectedColorID, vehicleIndex);

                    //                    if (selectedRimID >= 0) ApplyRimCustomization(selectedRimID, vehicleIndex);

                    Back();
                    break;
            }

            UpdateUI();
            ClosePromptPanel();
        }

        public void CheckButton()
        {
            bool boughtAll = true;
            for (int i = 1; i < 16; i++)
            {
                if (PlayerPrefs.GetInt("EuroTruck_" + i) == 0)
                {
                    boughtAll = false;
                }

            }


            if (boughtAll)
            {
                UnlockAllCarsBtn.SetActive(false);

            }

        }

        public void ClosePromptPanel()
        {
            if (promptPanel) promptPanel.SetActive(false);

            RevertCustomizationChanges();

            UpdateUI();
        }

        public void Back()
        {
            ButtonSFX();
            StorePageObj.SetActive(false);
            //sr Debug.Log(state);
            switch (state)
            {
                case State.Main:
                    Application.Quit();
                    break;

                case State.VehicleSelect:
                    state = State.TrackSelect;
                    vehicleIndex = PlayerPrefs.GetInt("SelectedVehicle");
                    // CycleVehicles();
                    break;

                case State.TrackSelect:
                    state = State.Main;
                    //				GameConfigs2018.mee.showRotationAds (MenuManager.CurrentLevel,AdsPageType.menu);
                    break;

                case State.Customize:
                    RevertCustomizationChanges();
                    state = State.VehicleSelect;
                    break;

                case State.Settings:
                    state = previousState;
                    break;

                case State.store:
                    state = Laststate;
                    break;
            }

            if (fadeOnStart && screenFade)
            {
                StartCoroutine(ScreenFadeInOut(fadeSpeed, "UpdateUI"));
            }
            // UpdateUI();
        }
        #endregion

        IEnumerator LoadScene()
        {
            async = SceneManager.LoadSceneAsync(menuTracks[trackIndex].sceneName);//menuTracks[trackIndex].sceneName

            while (!async.isDone)
            {
                if (loadingBar) loadingBar.fillAmount = async.progress;

                yield return null;
            }
        }

        void ButtonSFX()
        {
            if (SoundManager.instance) SoundManager.instance.PlaySound("Button", true);
        }



        public void UpdateCoins(int Amount)
        {
            //			//sr Debug.LogError ("Player currency before adding ::"+PlayerData.currency);
            PlayerData.currency = PlayerData.currency + Amount;
            PlayerData.SaveCurrency();
            if (playerCurrency) playerCurrency.text = " " + PlayerData.currency.ToString("N0");

            //			Btm_Utils2018.jarToast("Coins added... " + Amount);

            //			//sr Debug.LogError ("Player currency after adding ::"+PlayerData.currency);
        }

        public GameObject UnlockAllCarsPopup, UnlockAllLevelsPopup;
        public void UnlockAllCars_ContniueBtn()
        {
            //			Btm_IABManager.mee.BUY (1);
            UnlockAllCarsPopup.SetActive(false);

        }
        public void UnlockAllCars_CloseBtn()
        {
            UnlockAllCarsPopup.SetActive(false);
        }

        public void UnlockAllLevels_ContinueBtn()
        {
            //			Btm_IABManager.mee.BUY (2);
            UnlockAllLevelsPopup.SetActive(false);
        }
        public void UnlockAllLevels_CloseBtn()
        {
            UnlockAllLevelsPopup.SetActive(false);
        }
    }
}
