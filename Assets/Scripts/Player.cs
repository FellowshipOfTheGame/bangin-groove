using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Player : MonoBehaviour {

	public Sprite nameArt, charArt;
	public string flavor;

	public Player_Anim dancer;
	public Animator killer;
	public int index;

	float spd = 0.0f;

	bool dancing;

	public void Initialize(int player, Music music){
		dancing = true;
		index = player;
		dancer.gameObject.SetActive(true);
		dancer.player = index;

		if(player == 1){
			killer.gameObject.SetActive(true);
			killer.GetComponent<Throw>().SwapDir();
		}

		killer.gameObject.SetActive(false);
		dancer.Initialize(player, music);
	}
	
	// Update is called once per frame
	void Update () {
		if (spd != 0.0f)
			this.transform.position += Vector3.right * spd * (1 - 2 * index) * Time.deltaTime;
	}

	public void Turn(bool pick){
		dancer.gameObject.SetActive(false);
		killer.gameObject.SetActive(true);

		if(pick)
			killer.SetTrigger("pick");
		
		dancing = false;
	}

	public void Kill(){
		if(dancing)
			Turn(true);
		
		killer.SetTrigger("kill");
	}

	public void Stun(){
		killer.SetTrigger("stun");
	}

	public void Die(SpriteRenderer knife, Player winner){
		if(dancing)
			Turn(false);
		
		knife.color = Color.black;
		for(int i = 0; i < killer.transform.GetChild(1).childCount; i++){
			killer.transform.GetChild(1).GetChild(i).GetComponent<SpriteMeshInstance>().color = Color.black;
		}

		if (winner != null){
			for(int i = 0; i < winner.killer.transform.GetChild(1).childCount; i++){
				winner.killer.transform.GetChild(1).GetChild(i).GetComponent<SpriteMeshInstance>().color = Color.black;
			}
		}
		
		if(winner != null)
			Anim_Manager.instance.ShowStats(1 - index);
		else
			Anim_Manager.instance.ShowStats(-1);

		killer.SetTrigger("die");
	}

	public void Attack(){
		killer.SetInteger("player", index);
		killer.SetTrigger("atk");
		spd = 0;
	}

	public void Walk(float spd){
		killer.SetTrigger("go");
		this.spd = spd;
	}

	public void SetStatue(bool value){
		dancer.anim.enabled=!value;
		killer.enabled=!value;
	}
}
