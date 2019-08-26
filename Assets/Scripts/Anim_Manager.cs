using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Anim_Manager : MonoBehaviour {

	public static Anim_Manager instance; //singleton
	public Player[] players;

	public Animator[] lights;

	public Stats register;

	public Animator[] receptors;

	public Animator screen, bg, pause;
	public Text rndTitle, rndLabel, rndText;
	public string rhythmDesc, mashDesc;

	public Animator counter; //the countdown on the round start

	public float playerSpd, playerOffset;

	public int danceForward;

	float musicOffset;

	public Vector3[] playerPos;

	public Image[] playerNames;

	public Image victoryName;

	public Sprite defaultName;

	public GameObject[] canvasStuff;

	void Awake(){
		//singleton process
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
	}

	void Start(){
	}


	public void Initialize(GameObject char1, GameObject char2){
		GameObject player1 = Instantiate(char1, this.transform) as GameObject;
		GameObject player2 = Instantiate(char2, this.transform) as GameObject;


		player1.transform.localPosition = playerPos[0];
		player2.transform.localPosition = playerPos[1];

		players[0] = player1.GetComponent<Player>();
		playerNames[0].sprite = players[0].nameArt;
		
		players[1] = player2.GetComponent<Player>();
		playerNames[1].sprite = players[1].nameArt;

		players[0].Initialize(0,Rhythm_Manager.instance.music);
		players[1].Initialize(1,Rhythm_Manager.instance.music);
		receptors[0].SetInteger("index", 0);
		receptors[0].SetTrigger("reset");
		receptors[1].SetInteger("index", 1);
		receptors[1].SetTrigger("reset");
	}

	public void TurnUp(){
		musicOffset = Rhythm_Manager.instance.music.offset;
		players[0].dancer.Dance(danceForward, musicOffset);
		players[1].dancer.Dance(danceForward, musicOffset);
		receptors[0].SetTrigger("show");
		receptors[1].SetTrigger("show");
	}

	public void StartMash(float delay){
		players[0].Turn(true);
		players[1].Turn(true);
		Invoke("Assemble", delay - playerOffset/playerSpd);
		Invoke("Fight", delay);
		receptors[0].SetTrigger("hide");
		receptors[1].SetTrigger("hide");
	}

	void Assemble(){
		players[0].Walk(playerSpd);
		players[1].Walk(playerSpd);
	}

	void Fight(){
		players[0].Attack();
		players[1].Attack();
	}

	public void Finish(int winner){
		receptors[0].SetTrigger("hide");
		receptors[1].SetTrigger("hide");
		if(winner == 0){
			players[0].Kill();
			players[1].Stun();
		}else if (winner == 1){
			players[0].Stun();
			players[1].Kill();
		}else{
			players[0].Stun();
			players[1].Stun();
		}
	}
	
	public void ShowStats(int winner){
		if(winner != -1)
			victoryName.sprite = players[winner].nameArt;
		else
			victoryName.sprite = defaultName;


		foreach (GameObject g in canvasStuff)
			g.SetActive(false);

		bg.SetTrigger("end");
		counter.SetTrigger("kill");
		register.Invoke("ShowScore", 3.5f);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void ShowVictory(int player, int victories, bool final){
		lights[2 * player + victories - 1].SetTrigger("on");

		if (!final)
			ShowRound("ROUND 2");
	}

	public void ShowDraw(int vic1, int vic2, bool final){
		lights[vic1 - 1].SetTrigger("on");
		lights[vic2 + 1].SetTrigger("on");

		if (!final)
			ShowRound("ROUND 2");
	}

	public void CountDown(){
		counter.SetTrigger("Count"); //countdown
	}

	public void ShowRound(string round){
		if(round == "SUDDEN DEATH"){
			rndTitle.text = "SUDDEN \nDEATH";
			rndLabel.text = "BUTTON MASH!";
			rndText.text = mashDesc;
		}else{
			rndTitle.text = round;
			rndLabel.text = "RHYTHM ROUND!";
			rndText.text = rhythmDesc;
		}
		screen.SetTrigger("show");
	}

	public void PausePlayer(){
		players[0].SetStatue(true);
		players[1].SetStatue(true);
		screen.enabled=false;
		pause.SetTrigger("pause");
	}

	public void UnPausePlayer(){
		pause.SetTrigger("unpause");
		Invoke("CountDown", 0.3f);
	}

	public void ResumePlayer(){
		players[0].SetStatue(false);
		players[1].SetStatue(false);
		screen.enabled=true;
	}

	public void SetConfirmScreen(bool value){
		if (value)	pause.SetTrigger("confirm");
		else pause.SetTrigger("back");
	}
}
