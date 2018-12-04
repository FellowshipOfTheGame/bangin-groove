﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Block : MonoBehaviour {

	public string notes; //the note code of the block
	public float[] pos; //the relative pos X of each note
	public GameObject notePrefab; //an prefab of the note
	float speed; //the speed of the block

	void Start () {
		speed = Rhythm_Manager.instance.spawners[0].speed; //get oficial speed
	}
	
	void Update () {
		this.transform.position += Vector3.down * speed * Time.deltaTime; //move block down
	}
	//decrypt note code and add notes to the block
	public void Build(string notes){
		this.notes = notes; //store note code
		for(int i=0; i<notes.Length; i++){
			Note n = Instantiate(notePrefab, this.transform).GetComponent<Note>();
			n.initialize(notes[i]); //initialize note created
			switch (notes[i]){
				case 'L': //case left
					n.transform.localPosition = new Vector3(pos[0], 0.0f, 0.0f);
					break;
				case 'D': //case down
					n.transform.localPosition = new Vector3(pos[1], 0.0f, 0.0f);
					break;
				case 'U': //case up
					n.transform.localPosition = new Vector3(pos[2], 0.0f, 0.0f);
					break;
				case 'R': //case right
					n.transform.localPosition = new Vector3(pos[3], 0.0f, 0.0f);
					break;
			}
		}
	}
}
