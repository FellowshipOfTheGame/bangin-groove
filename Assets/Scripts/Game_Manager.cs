using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance; //singleton

	public GameObject[] characters;
	public Music music;

	

	void Awake(){
		//singleton process
		if (instance == null)
			instance = this;
		else{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Quit(){
		Application.Quit();
	}

	public void chooseMusic(Music music){
		this.music = music;
		Ready();
	}

	public void Ready(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		Invoke("Setup", 3.0f);
	}

	public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //load scene again
		Invoke("Setup", 1.5f);
    }

	public void Setup(){
		
		Rhythm_Manager.instance.Initialize(music);
		Anim_Manager.instance.Initialize(characters[0], characters[1]);
		Anim_Manager.instance.screen.SetTrigger("intro");
		Invoke("CallRhythm", 1.5f);
	}

	public void CallRhythm(){
		Anim_Manager.instance.ShowRound("ROUND 1");
		Rhythm_Manager.instance.Invoke("Play", 1.8f);
	}
}
