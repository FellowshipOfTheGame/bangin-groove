using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Selector : MonoBehaviour {

	[HideInInspector] public int index = 0;

	bool block = false;

	GameObject[] chars;

	// Use this for initialization
	void Start () {
		chars = new GameObject[2];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetPlayer(GameObject prefab){
		if(prefab != null)
			chars[index] = prefab;

		index++;
		if(index == 2)
			block = true;
	}

	public void Retreat(){
		index--;
		chars[index] = null;
		block = false;
	}

	public void FinishSelect(){
		Game_Manager.instance.characters[0] = chars[0];
		Game_Manager.instance.characters[1] = chars[1];
	}
}
