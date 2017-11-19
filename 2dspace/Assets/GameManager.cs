using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public float levelStartDelay = 2f;
	public float turnDelay = .1f;
	public static GameManager instance = null;
	public BoardManager boardScript;

	public bool doingSetup = true;
	private bool disable = true;
	private Text levelText;
	private GameObject levelImage;
	//private List<Enemy> enemies;
	private int level = 1;
	//This is called each time a scene is loaded.
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Debug.Log("Scene Loaded");
		if(!disable){
			Debug.Log("Enable to add level");
			//Add one to our level number.
			level++;
			//Call InitGame to initialize our level.
			InitGame();
		}
	}
	void OnEnable()
	{
		//Tell our ‘OnLevelFinishedLoading’ function to start listening for a scene change event as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}	
	void OnDisable()
	{
		//Tell our ‘OnLevelFinishedLoading’ function to stop listening for a scene change event as soon as this script is disabled.
		//Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
	void Awake() {
		Debug.Log("Awake");
		if(instance == null){
			instance = this;
		}
		else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
		boardScript = GetComponent<BoardManager>();
		InitGame();
	}
	//Hides black image used between levels
	void HideLevelImage()
	{
		//Disable the levelImage gameObject.
		levelImage.SetActive(false);
		
		//Set doingSetup to false allowing player to move again.
		doingSetup = false;

        PauseManager.AllowPause(true);
	}
	void InitGame(){
		Debug.Log("Start Init");

        PauseManager.AllowPause(false);

		//While doingSetup is true the player can't move, prevent player from moving while title card is up.
		doingSetup = true;
            
		//Get a reference to our image LevelImage by finding it by name.
		levelImage = GameObject.Find("LevelImage");
            
		//Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
		levelText = GameObject.Find("LevelText").GetComponent<Text>();
		
		//Set the text of levelText to the string "Day" and append the current level number.
		levelText.text = "Level " + level;
		//Set levelImage to active blocking player's view of the game board during setup.
		levelImage.SetActive(true);
		
		//Call the HideLevelImage function with a delay in seconds of levelStartDelay.
		Invoke("HideLevelImage", levelStartDelay);
		
		//Clear any Enemy objects in our List to prepare for next level.
		//enemies.Clear();
		Debug.Log("images loaded");
		//Call the SetupScene function of the BoardManager script, pass it current level number.
		boardScript.setupScene(0);
		Debug.Log("endInit");
	}
	void Start () {
		disable = false;
	}

	void GameOver() {
		//Set levelText to display number of levels passed and game over message
		levelText.text = "You died on floor " + level + ".";
		
		//Enable black background image gameObject.
		levelImage.SetActive(true);
		
		//Disable this GameManager.
		enabled = false;
	}

	
	
	// Update is called once per frame
	void Update () {
		
	}
}
