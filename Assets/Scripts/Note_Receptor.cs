using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_Receptor : MonoBehaviour {

    int playerIndex; //player identifier

	public float okRange; //max distance to a hit
	public float goodRange; //max distance to a good hit
	public float perfectRange; //mas distance to a perfect hit

    [Space(5)]
    public float tolerance; //time tolerance to press notes together

	Note_Spawner spawn; //the relative spawn of the receptor
	float startime; //time reference

	bool[] pressed; //the registers who stores what keys enter on input
    KeyCode[] keys; //the keys chosen for the notes

    bool inRange = false; //if note is in range
	int sequence = 0; //how many notes the player has hit without missing
    int hitCount = 0; //how many notes from the block the player hit

	void Start () {
		pressed = new bool[4]; //set key registers to false
		spawn = this.transform.parent.GetComponent<Note_Spawner>();
        playerIndex = spawn.playerIndex; //to know which player is this guy
        //set keys
		spawn.modifier = 1;
        if (playerIndex == 0)
            keys = new KeyCode[4] {KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.D}; //to player 1
        else
            keys = new KeyCode[4] {KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.RightArrow}; //to player 2

	}

    void Sync_Range(){
        //if we have blocks to hit
		if (spawn.blocks.Count > 0){
            //if current note is in range
            if(spawn.blocks[0].transform.position.y >= this.transform.position.y - okRange &&
                                spawn.blocks[0].transform.position.y <= this.transform.position.y + okRange){
                inRange = true;
            
			}else{
                inRange = false;
                //if current note is going away
                if(spawn.blocks[0].transform.position.y <= this.transform.position.y - okRange){
                    spawn.blocks[0].Destroy();
                    spawn.blocks.RemoveAt(0);
                    Miss();
                }
			}
        }else
            inRange = false;
    }

	void Update () {
        Sync_Range();

		spawn.modtxt.text = "x\n" + spawn.modifier.ToString(); //show modifier on screen

        if (inRange){
            //check all keys pressed
            for(int i = 0; i < 4; i++){
                if (Input.GetKeyDown(keys[i])) {
                    if(Have_Note(i)){
                        if (!pressed[0] && !pressed[1] && !pressed[2] && !pressed[3]) //if no keys are pressed yet
                            startime = Time.time; //set time reference
                        pressed[i] = true;
                        hitCount++;
                    }else
                        Miss();
                }
            }
            //if tolerance time's up
            if ((Time.time - startime >= tolerance) && (pressed[0] || pressed[1] || pressed[2] || pressed[3]))
                Miss();
            //it the player got all the notes of the block
            if(hitCount == spawn.blocks[0].notes.Length)
                Hit();

        }else{
            //check all keys pressed
            for(int i = 0; i < 4; i++){
                if (Input.GetKeyDown(keys[i]))
                    pressed[i] = true;
            }
            //if any key is pressed the player fail
            if(pressed[0] || pressed[1] || pressed[2] || pressed[3])
                Miss();
        }
	}

    void Miss(){
        //Debug.Log("missed");
		sequence = 0;
		spawn.modifier = 1;
        Reset();
    }

    void Hit(){
        //calculate points by distance
		float distance = Mathf.Abs(spawn.blocks[0].transform.position.y - this.transform.position.y);

		if(distance < okRange){
			if(distance < perfectRange){
				spawn.score += 300 * spawn.modifier; //perfect score
                spawn.blocks[0].HitBlock(3);
            }else if(distance < goodRange){
				spawn.score += 200 * spawn.modifier; //good score
                spawn.blocks[0].HitBlock(2);
            }else {
				spawn.score += 100 * spawn.modifier; //default score
                spawn.blocks[0].HitBlock(1);
            } 
			sequence++;
			if (sequence == 10) spawn.modifier = 2;
			else if (sequence == 25) spawn.modifier = 3;
			else if (sequence == 50) spawn.modifier = 5;
			else if (sequence == 100) spawn.modifier = 10;
			spawn.blocks.RemoveAt(0);
		}
        Reset();
    }

    bool Have_Note(int i){
        switch (i){
            case 0:
                return spawn.blocks[0].notes.Contains("L");
            case 1:
                return spawn.blocks[0].notes.Contains("D");
            case 2:
                return spawn.blocks[0].notes.Contains("U");
            case 3:
                return spawn.blocks[0].notes.Contains("R");
            default:
                return false;
        }
    }

	void Reset(){
        hitCount = 0;
        //reset button registers
        for(int i=0; i<4; i++)
            pressed[i] = false;
        
	}
}
