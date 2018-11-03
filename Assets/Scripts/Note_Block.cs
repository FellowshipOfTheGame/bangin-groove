using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Block : MonoBehaviour {

	public string notes;
	public float[] pos;
	public GameObject notePrefab;
	float speed;

	// Use this for initialization
	void Start () {
		speed = Rhythm_Manager.instance.spawners[0].speed;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += Vector3.down * speed;
	}

	public void Build(string notes){
		this.notes = notes;
		for(int i=0; i<notes.Length; i++){
			Note n = Instantiate(notePrefab, this.transform).GetComponent<Note>();
			n.initialize(notes[i]);
			switch (notes[i]){
				case 'L':
					n.transform.localPosition = new Vector3(pos[0], 0.0f, 0.0f);
					break;
				case 'D':
					n.transform.localPosition = new Vector3(pos[1], 0.0f, 0.0f);
					break;
				case 'U':
					n.transform.localPosition = new Vector3(pos[2], 0.0f, 0.0f);
					break;
				case 'R':
					n.transform.localPosition = new Vector3(pos[3], 0.0f, 0.0f);
					break;
			}
		}

	}
}
