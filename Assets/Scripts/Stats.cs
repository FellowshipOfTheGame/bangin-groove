using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	float[] scores;
	public Text winnerTitle, rnd1, rnd2, mash, final;

	public Image winnerName;

	// Use this for initialization
	void Start () {
		scores = new float[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StoreScore(int round){
		scores[round - 1] = Rhythm_Manager.instance.spawners[0].score;
		scores[round] = Rhythm_Manager.instance.spawners[1].score;
	}

	public void SetWinner(int player, int mashCount, float mashTime){
		if(player != -1){
			winnerTitle.text = "Player " + (player + 1).ToString();
			rnd1.text = "1st Round: " + scores[player].ToString() + "pts";
			rnd2.text = "2nd Round: " + scores[player + 1].ToString() + "pts";
			
			if(mashCount != -1){
				Rhythm_Manager.instance.spawners[player].victories++;
				mash.text = "Sudden Death: " + mashCount.ToString() + "hits / " + mashTime.ToString("F0") + "s";
			}else{
				mash.text = "Sudden Death: -";
			}
			final.text = "Final Result: " + Rhythm_Manager.instance.spawners[0].victories.ToString() + " x " + Rhythm_Manager.instance.spawners[1].victories.ToString();

		}else{
			winnerTitle.text = string.Empty;
			rnd1.text = "1st Round: -";
			rnd2.text = "2nd Round: -";
			final.text = "Final Result: 3x3";
			mash.text = "Sudden Death: -";
		}
	}

	public void ShowScore(){
		Anim_Manager.instance.screen.SetTrigger("stats");
	}
}
