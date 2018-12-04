using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_Receptor : MonoBehaviour {

    public int playerIndex; //player identifier

    [Space(5)]
	public float okRange; //max distance to a hit
	public float goodRange; //max distance to a good hit
	public float perfectRange; //mas distance to a perfect hit

    [Space(5)]
    public float tolerance; //time tolerance to press notes together

	Note_Spawner spawn; //the relative spawn of the receptor
	float startime; //time reference

	bool[] pressed; //the registers who stores what keys enter on input
    KeyCode[] keys; //the keys chosen for the notes

	void Start () {
		pressed = new bool[4]; //set key registers to false
		spawn = this.transform.parent.GetComponent<Note_Spawner>();

        //set keys
        if (playerIndex == 0)
            keys = new KeyCode[4] {KeyCode.A, KeyCode.S, KeyCode.W, KeyCode.D}; //to player 1
        else
            keys = new KeyCode[4] {KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.UpArrow, KeyCode.RightArrow}; //to player 2

	}

	void Update () {
        //if we have blocks to hit
		if (spawn.blocks.Count > 0){
            //if current note is going away out of range
			if(spawn.blocks[0].transform.position.y <= this.transform.position.y - okRange){ 
				Destroy(spawn.blocks[0].gameObject);
				spawn.blocks.RemoveAt(0);
			}
        }
        //check all keys pressed
        for(int i = 0; i < 4; i++){
            if (Input.GetKeyDown(keys[i])) {
                if (!pressed[0] && !pressed[1] && !pressed[2] && !pressed[3]) //if no keys are pressed yet
                    startime = Time.time; //set time reference
                pressed[i] = true;
            }
        }
        //check all keys released
        for(int i = 0; i < 4; i++){
            if (Input.GetKeyUp(keys[i]))
                Check_Notes();
        }
        //if tolerance time's up
        if ((Time.time - startime <= tolerance) && (pressed[0] || pressed[1] || pressed[2] || pressed[3]))
			Check_Notes();
        
	}


	public void Check_Notes(){
        if(spawn.blocks.Count > 0){
            //build input code
            string aux = "";
		    if (pressed[0])aux += 'L';
		    if (pressed[1]) aux += 'D';
		    if (pressed[2]) aux += 'U';
		    if (pressed[3]) aux += 'R';
            //if the code is right
		    if(spawn.blocks[0].notes == aux){
                //calculate points by distance
			    float distance = Mathf.Abs(spawn.blocks[0].transform.position.y - this.transform.position.y);

			    if(distance < okRange){
				    if(distance < perfectRange)
                        spawn.score += 300; //perfect score
                    else if(distance < goodRange)
                        spawn.score += 200; //good score
                    else 
                        spawn.score += 100; //default score
                    
				    Destroy(spawn.blocks[0].gameObject);
				    spawn.blocks.RemoveAt(0);
			    }
		    }
        }
        //reset button registers
        for(int i=0; i<4; i++)
            pressed[i] = false;
	}
}
