using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour {

	public static Game_Manager instance; //singleton

	void Awake(){
		//singleton process
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
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
}
