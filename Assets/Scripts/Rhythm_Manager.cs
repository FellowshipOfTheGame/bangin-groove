using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rhythm_Manager : MonoBehaviour {

    public static Rhythm_Manager instance; //singleton

    int round; //current round
    bool canSpawn = false, counting = false; //control booleans
    Compass compass; //step controller

    public Music music; //chosen music
    public float pauseDelay, finalDelay; //delay to call pop ups

    [Space(5)]
    public Note_Spawner[] spawners; //players 1 and 2

    [Space(5)]
    public GameObject instructionScreen, middleScreen, suddenScreen, victoryScreen; //screens
    public Text middleText, victoryText; //they tell who won on screen
    public Animator counter; //the countdown on the round start
    
	void Awake(){
        //singleton process
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
		
        //setiting compass
        compass = this.GetComponent<Compass>();
        compass.Initialize(music, spawners[0].getOffset()); //set compass to follow the music
        compass.step += CallSpawn; //synchronize spawn in compass
        round = 1;

        //control pop ups
        instructionScreen.SetActive(true); //initial pop up
        middleScreen.SetActive(false);
        suddenScreen.SetActive(false);
        victoryScreen.SetActive(false);
	}

    //start to dance!
    public void Play(){
        instructionScreen.SetActive(false); //close pop up
        canSpawn = true; //allow spawners to work
        counter.SetTrigger("Count"); //countdown
        compass.Play(); //active compass
    }

    //called every step in compass
    public void CallSpawn(int count){
        //wake up the the spawners
        if (canSpawn){
            foreach (Note_Spawner ns in spawners)
                ns.ChangeStep(); //spawn notes
        }
        //check end of round 1
        if (music.halfSize == count && round == 1) {
            canSpawn = false; //stop spawners
            round++;
            CancelInvoke();
            Invoke("Pause", spawners[0].getOffset() + 0.5f); //wait until the last note end your way to finish the round 1
        }
        //check end of round 2
        if (music.songSize == count && round == 2) {
            canSpawn = false; //stop spawners
            round++;
            CancelInvoke();
            Invoke("FinalScreen", spawners[0].getOffset() + music.ending); //wait until the last note end your way to finish the round 2
        }
    }
    //end round 1
    void Pause() {
        middleScreen.SetActive(true); //show pop up
        //check winner of the round 1
        if (spawners[0].score > spawners[1].score) {
            spawners[0].victories++;
            middleText.text = "Player 1 wins the first round";
        } else if (spawners[0].score < spawners[1].score) {
            spawners[1].victories++;
            middleText.text = "Player 2 wins the first round ";
        }else{
            spawners[0].victories++;
            spawners[1].victories++;
            middleText.text = "No winners in the first round ";
        }
        //call the counter and rouns 2 in time
        Invoke("ReCount", music.halfGap * 60.0f / music.bpm - 4.0f);
        Invoke("Continue", music.halfGap * 60.0f / music.bpm - 0.5f - spawners[0].getOffset());
    }
    //reset scores and call the counter
    void ReCount(){
        middleScreen.SetActive(false); //close pop up
        spawners[0].score = 0;
        spawners[1].score = 0;
        counter.SetTrigger("Count"); //countdown
    }
    //start round 2
    void Continue () {
        canSpawn = true; //allow spawners to work
    }
    //end round 2
    void FinalScreen() {
        //check winner of the round 2
        if (spawners[0].score > spawners[1].score) spawners[0].victories++;
            else if (spawners[0].score < spawners[1].score) spawners[1].victories++;
            else{
                spawners[0].victories++;
                spawners[1].victories++;
            }
        //calculate winner of the match
        if (spawners[0].victories > spawners[1].victories) {
            victoryScreen.SetActive(true); //show victory pop up
            victoryText.text = "Player 1 wins";
        } else if (spawners[0].victories < spawners[1].victories) {
            victoryScreen.SetActive(true); //show victory pop up
            victoryText.text = "Player 2 wins";
        } else {
            suddenScreen.SetActive(true); //show sudden death pop up
        }
    }
    //pass the bomb to Mash Manager
    public void StartMash(){
        suddenScreen.SetActive(false); //close pop up
        //close spawners
        spawners[0].gameObject.SetActive(false);
        spawners[1].gameObject.SetActive(false);
        //call Manager
        Mash_Manager.instance.Invoke("Initialize", 3.2f);
        counter.SetTrigger("Count"); //countdown
    }
    //restart the match
    public void Reset() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //load scene again
    }
}
