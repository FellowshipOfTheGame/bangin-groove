using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance; //singleton

	public GameObject[] characters;
	public Music music;
	bool paused = false, canPause = false;
	bool inGame = true;

	void Awake(){
		//singleton process
		if (instance == null)
			instance = this;
		else{
			Destroy(this.gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		Time.timeScale = 1.0f;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space) && inGame && canPause){
			PausePlay();
		}
	}

	public void Quit(){
		Application.Quit();
	}

	public void BackToMenu(){
		SceneManager.LoadScene(0);
	}

	public void SlowMotion(){
		//Time.timeScale = 0.02f;
	}

	public void chooseMusic(Music music){
		this.music = music;
		Debug.Log("music!");
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
		inGame=true;
		paused=false;
		canPause=false;
		Rhythm_Manager.instance.Initialize(music);
		Anim_Manager.instance.Initialize(characters[0], characters[1]);
		Anim_Manager.instance.screen.SetTrigger("intro");
		Invoke("CallRhythm", 1.5f);
	}

	public void CallRhythm(){
		Anim_Manager.instance.ShowRound("ROUND 1");
		Rhythm_Manager.instance.Invoke("Play", 1.8f);
		//Invoke("EnablePause", 2.0f);
	}

	public void EnablePause(){
		canPause = true;
	}

	public void BlockPause(){
		canPause = false;
	}

	public void PausePlay(){
		if(!paused){
			Rhythm_Manager.instance.Pause();
			Anim_Manager.instance.PausePlayer();
			canPause=false;
			Invoke("EnablePause", 0.42f);
		}else{
			Anim_Manager.instance.UnPausePlayer();
			canPause=false;
			Invoke("Resume", 3.3f);
		}
		paused=!paused;
	}

	public void Resume(){
		Rhythm_Manager.instance.Resume();
		Anim_Manager.instance.ResumePlayer();
		canPause=true;
	}
}
