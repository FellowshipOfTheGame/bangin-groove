using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Anim : MonoBehaviour {

	public Animator anim;

	public int player;
	public string[] steps;
	int index = 0, count = 0;

	bool blocked = true;

	public bool test;

	float danceTime;

	// Use this for initialization
	void Awake () {
		if(test)
			Initialize(0, null);
	}

	public void Initialize(int player, Music music){
		anim = this.GetComponent<Animator>();
		index = 0;
		count = 0;
		blocked = true;
		danceTime = 0.0f;

		if(!test && music != null){
			//this.player = player;
			this.steps = music.moves;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(!blocked) Move();
	}

	public void Dance(int forward, float offset){
		index = forward;
		blocked = false;
		danceTime = 0.0f;

		if(forward != 0)
			Invoke("Move", offset);
		else
			Move();
	}

	void Move(){
		float songPos = Rhythm_Manager.instance.compass.getSongPos();
		int bpm = Rhythm_Manager.instance.music.bpm;
		if(index < steps.Length && songPos >= danceTime){
			int repeat = 1;
			string step = GetStep(index, player);

			if (step != string.Empty){
				string aux = step.Substring(4);
				if(step[3] == 'x'){
					repeat = int.Parse(step.Substring(4, step.IndexOf('-') - 4));
					aux = step.Substring(step.IndexOf('-') + 1);
				}				
				if(aux != string.Empty && !blocked)
					anim.SetTrigger(aux);

				danceTime += float.Parse(step.Substring(0,3));

				count++;
				if(count == repeat){
					count = 0;
					index++;
				}
			}else{
				index++;
				Move();
			}			
		}
	}

	string GetStep(int index, int player){
		if(steps[index].Contains("/")){
			//Debug.Log("divide!!! :" + steps[index].Substring(0, steps[index].IndexOf('/')) +
			//" " + steps[index].Substring(steps[index].IndexOf('/') + 1));

			if(player == 0)
				return steps[index].Substring(0, steps[index].IndexOf('/'));
			else
				return steps[index].Substring(steps[index].IndexOf('/') + 1);
		}

		return steps[index];
	}

	public void Miss(){
		anim.SetTrigger("miss");
		blocked = true;
		Invoke("Recover", 0.5f);
	}

	void Recover(){
		blocked = false;
	}
}
