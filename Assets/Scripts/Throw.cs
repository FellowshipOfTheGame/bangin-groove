using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Throw : MonoBehaviour {


	public GameObject knife;

	public GameObject hand, weapon;

	public float swapOffset;

	public GameObject[] forRight;
	public GameObject[] forLeft;

	int dir = 1;

	public void ThrowKnife(){
		Release();
		knife.GetComponent<Knife>().Fly(dir);
		//Game_Manager.instance.SlowMotion();
	}

	public void Release(){
		hand.SetActive(true);
		weapon.SetActive(false);
		knife.SetActive(true);
		knife.transform.SetParent(transform);
	}

	public void PickUp(){
		hand.SetActive(false);
		weapon.SetActive(true);
	}

	public void SwapDir(){
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y ,transform.localScale.z);
		transform.position -= Vector3.right * swapOffset * dir;
		if (dir == 1){
			foreach (GameObject gm in forRight)
				gm.SetActive(true);

			foreach (GameObject gm in forLeft)
				gm.SetActive(false);
		}else{
			foreach (GameObject gm in forRight)
				gm.SetActive(false);

			foreach (GameObject gm in forLeft)
				gm.SetActive(true);
		}
		dir = -dir;

		for(int i = 0; i < transform.GetChild(1).childCount; i++)
			transform.GetChild(1).GetChild(i).GetComponent<SpriteMeshInstance>().sortingOrder += 3;
	}
}
