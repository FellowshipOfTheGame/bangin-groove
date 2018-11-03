using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Receptor : MonoBehaviour {

	Note_Spawner spawn;
	public Animator player;
	public float tolerance;
	float startime;
	bool[] pressed;
	public float okRange;
	public float goodRange;
	public float perfectRange;

	// Use this for initialization
	void Start () {
		pressed = new bool[4];
		spawn = this.transform.parent.GetComponent<Note_Spawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (spawn.blocks.Count > 0){
			if(spawn.blocks[0].transform.position.y <= this.transform.position.y - okRange){ 
				Destroy(spawn.blocks[0].gameObject);
				spawn.blocks.RemoveAt(0);
			}
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			if (!pressed[0] && !pressed[1] && !pressed[2] && !pressed[3]) 
				startime = Time.time;
			pressed[0] = true;
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			if (!pressed[0] && !pressed[1] && !pressed[2] && !pressed[3]) 
				startime = Time.time;
			pressed[1] = true;
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			if (!pressed[0] && !pressed[1] && !pressed[2] && !pressed[3]) 
				startime = Time.time;
			pressed[2] = true;
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			if (!pressed[0] && !pressed[1] && !pressed[2] && !pressed[3]) 
				startime = Time.time;
			pressed[3] = true;
		}

		if ((Time.time - startime <= tolerance) && (pressed[0] || pressed[1] || pressed[2] || pressed[3])){
			string aux = "";
			if (pressed[0]) aux += 'L';
			if (pressed[1]) aux += 'D';
			if (pressed[2]) aux += 'U';
			if (pressed[3]) aux += 'R';

			for(int i=0; i<4; i++) pressed[i] = false;

			Check_Notes(aux);
			//Debug.Log(spawn.score);
		}
	}

	public void Check_Notes(string notes){
		if(spawn.blocks[0].notes == notes){
			float distance = Mathf.Abs(spawn.blocks[0].transform.position.y - this.transform.position.y);
			Debug.Log(distance);
			if(distance < okRange){
				if(distance < perfectRange) spawn.score += 3;
				else if(distance < goodRange) spawn.score += 2;
				else spawn.score += 1;	

				Destroy(spawn.blocks[0].gameObject);
				spawn.blocks.RemoveAt(0);
			}
		}
	}
}
