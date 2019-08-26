using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rhythm_Manager : MonoBehaviour {

    public static Rhythm_Manager instance; //singleton

    int round; //current round
    bool canSpawn = false, counting = false, suddenDeath = false; //control booleans
    public Compass compass; //step controller

    public Music music; //chosen music
    float pauseTime = 0.0f;

    [Space(5)]
    public Note_Spawner[] spawners; //players 1 and 2
    
	void Awake(){
        //singleton process
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
	}

    public void Initialize(Music music){
        this.music = music;
        //setiting compass
        compass = this.GetComponent<Compass>();
        compass.Initialize(music, spawners[0].getOffset()); //set compass to follow the music
        compass.step += CallSpawn; //synchronize spawn in compass
        round = 1;

        spawners[0].Initialize();
        spawners[1].Initialize();
    }

    //start to dance!
    public void Play(){
        canSpawn = true; //allow spawners to work
        Anim_Manager.instance.CountDown(); //countdown
        Anim_Manager.instance.TurnUp(); //set all start animations
        compass.Play(); //active compass
        Game_Manager.instance.EnablePause();
    }

    //called every step in compass
    public void CallSpawn(int count){
        //wake up the the spawners
        if (canSpawn){
            foreach (Note_Spawner ns in spawners)
                ns.ChangeStep(count); //spawn notes
        }
        //check end of round 1
        if (music.halfSize == count && round == 1) {
            canSpawn = false; //stop spawners
            round++;
            Game_Manager.instance.BlockPause();
            CancelInvoke();
            Invoke("Gap", spawners[0].getOffset() + 0.5f); //wait until the last note end your way to finish the round 1
        }
        //check end of round 2
        if (music.songSize == count && round == 2) {
            canSpawn = false; //stop spawners
            round++;
            Game_Manager.instance.BlockPause();
            CancelInvoke();
            Invoke("FinalScreen", spawners[0].getOffset() + music.ending); //wait until the last note end your way to finish the round 2
        }
    }
    //end round 1
    void Gap() {
        Anim_Manager.instance.register.StoreScore(1);
        //check winner of the round 1
        if (spawners[0].score > spawners[1].score) {
            spawners[0].victories++;
            Anim_Manager.instance.ShowVictory(0, spawners[0].victories, false);
        } else if (spawners[0].score < spawners[1].score) {
            spawners[1].victories++;
            Anim_Manager.instance.ShowVictory(1, spawners[1].victories, false);
        }else{
            spawners[0].victories++;
            spawners[1].victories++;
            Anim_Manager.instance.ShowDraw(spawners[0].victories, spawners[1].victories, false);
        }
        //call the counter and rouns 2 in time
        Invoke("ReCount", music.halfGap * 60.0f / music.bpm - 4.0f);
        Invoke("Continue", music.halfGap * 60.0f / music.bpm - 0.5f - spawners[0].getOffset());
    }
    //reset scores and call the counter
    void ReCount(){
        spawners[0].score = 0;
        spawners[0].modifier = 1;
        spawners[1].score = 0;
        spawners[1].modifier = 1;
        Anim_Manager.instance.CountDown();
    }
    //start round 2
    void Continue () {
        canSpawn = true; //allow spawners to work
        Game_Manager.instance.EnablePause();
    }
    //end round 2
    void FinalScreen() {
        Anim_Manager.instance.register.StoreScore(2);
        //check winner of the round 2
        if (spawners[0].score > spawners[1].score){
            spawners[0].victories++;
            Anim_Manager.instance.ShowVictory(0, spawners[0].victories, true);
        }else if (spawners[0].score < spawners[1].score){ 
            spawners[1].victories++;
            Anim_Manager.instance.ShowVictory(1, spawners[1].victories, true);
        }else{
            spawners[0].victories++;
            spawners[1].victories++;
            Anim_Manager.instance.ShowDraw(spawners[0].victories, spawners[1].victories, true);
        }
        //calculate winner of the match
        if (spawners[0].victories > spawners[1].victories) {
            Anim_Manager.instance.register.SetWinner(0, -1, 0.0f);
            Anim_Manager.instance.Finish(0);
        } else if (spawners[0].victories < spawners[1].victories) {
            Anim_Manager.instance.register.SetWinner(1, -1, 0.0f);
            Anim_Manager.instance.Finish(1);
        } else {
            Invoke("StartMash", 1.8f); //show sudden death pop up
            Anim_Manager.instance.ShowRound("SUDDEN DEATH");
        }
    }
    //pass the bomb to Mash Manager
    public void StartMash(){
        //close spawners
        //spawners[0].gameObject.SetActive(false);
        //spawners[1].gameObject.SetActive(false);
        //call Manager
        Mash_Manager.instance.Invoke("Initialize", 3.2f);
        Anim_Manager.instance.StartMash(3.2f);
        suddenDeath = true;
        Anim_Manager.instance.CountDown(); //countdown
    }
    //restart the match
    public void Reset() {
        Game_Manager.instance.Reset(); //load scene again
    }

    public void Quit(){
        SceneManager.LoadScene("Menu");
    }

    public void Pause(){
        if (!suddenDeath){
             compass.counting=false;
            foreach (Note_Spawner ns in spawners)
                    ns.setFreeze(true); //freeze notes

            pauseTime = (float)(AudioSettings.dspTime);
            compass.source.Pause();
        }else{
            Mash_Manager.instance.PauseMash();
        }
    }

    public void Resume(){
        if(!suddenDeath){
            compass.counting=true;
            compass.payPause((float)(AudioSettings.dspTime) - pauseTime);
            //Debug.Log("GAP: " + ((float)(AudioSettings.dspTime) - pauseTime).ToString());
            foreach (Note_Spawner ns in spawners)
                    ns.setFreeze(false);
            compass.source.UnPause();
        }else{
            Mash_Manager.instance.ResumeMash();
        }
    }
}
