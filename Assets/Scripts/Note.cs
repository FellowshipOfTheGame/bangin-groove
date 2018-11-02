using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	char type;
	public Sprite[] arrows;
	Animator anim;
	SpriteRenderer spr;

	// Use this for initialization
	void Awake () {
		spr = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void initialize(char dir){
		type = dir;
		switch (dir){
			case 'L':
				spr.sprite = arrows[0];
				break;
			case 'D':
				spr.sprite = arrows[1];
				break;
			case 'U':
				spr.sprite = arrows[2];
				break;
			case 'R':
				spr.sprite = arrows[3];
				break;
		}
	}
}
