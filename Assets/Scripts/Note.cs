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
		anim = this.GetComponent<Animator>();
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
				this.transform.GetChild(0).Rotate(Vector3.forward, 90.0f);
				break;
			case 'U':
				spr.sprite = arrows[2];
				this.transform.GetChild(0).Rotate(Vector3.forward, -90.0f);
				break;
			case 'R':
				spr.sprite = arrows[3];
				this.transform.GetChild(0).Rotate(Vector3.forward, 180.0f);
				break;
		}
	}

	public void Erase(){
		anim.SetTrigger("Fail");
	}

	public void Touch(int rank){
		switch (rank){
			case 1:
				this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color (0.2f, 0.4f, 0.4f);
				break;
			case 2:
				this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color (0.7f, 0.7f, 1f);
				break;
			case 3:
				this.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color (1f, 1f, 0.7f);
				break;
		}

		anim.SetTrigger("Hit");
	}
}
