using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	public Animator anims;
	public MonoBehaviour scripts;

	public bool canPause;

	bool paused = false;

	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			Debug.Log("PAUSA");
			if(!paused)
				PauseGame();
			else
				CountToGo();
		}
	}

	public void PauseGame(){
		paused = true;
		Rhythm_Manager.instance.spawners[0].setFreeze(true);
		Rhythm_Manager.instance.spawners[1].setFreeze(true);
		Rhythm_Manager.instance.compass.source.Pause();
		Rhythm_Manager.instance.compass.enabled = false;

		Anim_Manager.instance.players[0].dancer.anim.enabled = false;
		Anim_Manager.instance.players[0].killer.enabled = false;
		Anim_Manager.instance.players[0].dancer.enabled = false;

		Anim_Manager.instance.players[1].dancer.anim.enabled = false;
		Anim_Manager.instance.players[1].killer.enabled = false;
		Anim_Manager.instance.players[1].dancer.enabled = false;

		Anim_Manager.instance.counter.enabled = false;
		Anim_Manager.instance.bg.enabled = false;

		Mash_Manager.instance.hits.SetActive(false);
	}

	public void CountToGo(){
		Anim_Manager.instance.counter.enabled = true;
		Anim_Manager.instance.CountDown();
		Invoke("ResumeGame", 3.5f);
	}

	public void ResumeGame(){
		paused = false;
		Rhythm_Manager.instance.spawners[0].setFreeze(false);
		Rhythm_Manager.instance.spawners[1].setFreeze(false);
		Rhythm_Manager.instance.compass.source.Play();
		Rhythm_Manager.instance.compass.enabled = true;
		Anim_Manager.instance.players[0].dancer.anim.enabled = true;
		Anim_Manager.instance.players[0].killer.enabled = true;
		Anim_Manager.instance.players[0].dancer.enabled = true;
		Anim_Manager.instance.players[1].dancer.anim.enabled = true;
		Anim_Manager.instance.players[1].killer.enabled = true;
		Anim_Manager.instance.players[1].dancer.enabled = true;
		
		Anim_Manager.instance.bg.enabled = true;
		//Mash_Manager.instance.hits.SetActive(true);
	}
}
