using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mash_Manager : MonoBehaviour {

    public static Mash_Manager instance; //singleton

    bool mash = false; //control boolean
    float initialDistance; //space reference
    float timeStart; //time reference

    public float timeLimit; //max time of button mash
    public int clickLimit; //max number of clicks

    int[] counts; //the counters of each player in button mash
    KeyCode[] keys; //the key that each playern needs to press
    
    [Space(5)]
    public Sprite[] arrows; //all arrow sprites

    [Space(5)]
    public Image[] notes; //the notes representation on canvas
    public Transform[] marks; //marks of each player in the barr
    public Animator barr; //the barr
    public GameObject hits; //the animations who indicate which button to press
    public GameObject victoryScreen; //the victory pop up
    public Text victoryText; //that tells who won
    public Knife[] nobodyKnives;

    void Awake(){
        //singeton process
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
		
	}
    //when the Mash Manager is called
    public void Initialize () {
        counts = new int[2]; //initialize counters to zero
        initialDistance = marks[1].position.x; //get mark initial position
        hits.SetActive(true); //show indicators

        nobodyKnives[0].gameObject.SetActive(false);
        nobodyKnives[1].gameObject.SetActive(false);

        //sort key to use
        keys = new KeyCode[2];
        int testkey = Random.Range(1, 4);
        switch (testkey){
            case 1: //use left key
                keys[1] = KeyCode.LeftArrow;
                keys[0] = KeyCode.A;
                break;
            case 2: //use down key
                keys[1] = KeyCode.DownArrow;
                keys[0] = KeyCode.S;
                break;
            case 3: //use up key
                keys[1] = KeyCode.UpArrow;
                keys[0] = KeyCode.W;
                break;
            case 4: //use right key
                keys[1] = KeyCode.RightArrow;
                keys[0] = KeyCode.D;
                break;
        }
        //make choose key appear on indicators
        foreach(Image note in notes){
            note.sprite = arrows[testkey - 1];
        }

        mash = true; //start mash
        timeStart = Time.time; //set time reference
        barr.SetTrigger("on"); //show the barr
    }

	void Update () {
        //if button mash is enabled
        if (mash) {
            //if player 1 pressed the button
            if (Input.GetKeyDown(keys[0])) {
                counts[0]++;
                marks[0].position += (initialDistance / clickLimit) * Vector3.right; //move player1's mark to center
            }
            //if player 2 pressed the button
            if (Input.GetKeyDown(keys[1])) {
                counts[1]++;
                marks[1].position += (initialDistance / clickLimit) * Vector3.left; //move player2's mark to center
            }
            //on time's up or if one player hit the goal
            if (Time.time - timeStart >= timeLimit || counts[0] >= clickLimit || counts[1] >= clickLimit) {
                victoryScreen.SetActive(true); //show victory pop up
                barr.SetTrigger("off");
                hits.SetActive(false); //hide the indicators
                mash = false; //stop button mash
                //check winners
                if (counts[0] > counts[1]){
                    Anim_Manager.instance.Finish(0);
                    Anim_Manager.instance.register.SetWinner(0, counts[0], Time.time - timeStart);
                    
                }else if (counts[0] < counts[1]){
                    Anim_Manager.instance.Finish(1);
                    Anim_Manager.instance.register.SetWinner(1, counts[1], Time.time - timeStart);
                }else{
                    Anim_Manager.instance.Finish(-1);
                    Invoke("Draw", 0.45f);
                    Anim_Manager.instance.register.SetWinner(-1, 0, 0.0f);
                }
            }
        }
    }

    void Draw(){
        nobodyKnives[0].gameObject.SetActive(true);
        nobodyKnives[0].Fly(-1);
        nobodyKnives[1].gameObject.SetActive(true);
        nobodyKnives[1].Fly(1);
    }
}
