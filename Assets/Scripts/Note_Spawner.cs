using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_Spawner : MonoBehaviour {

    public int playerIndex;
	public int modifier; //player modifier count

    int count = 0; //control counter
	string[] sequel;

	[Space(5)]
	public float speed; //the speed of each note
	public GameObject nbPrefab; //the prefab of a note block

	[HideInInspector] public int score; //player score count
	[HideInInspector] public int victories; //player victory count
	[HideInInspector] public List<Note_Block> blocks; //spawned block list

	[Space(5)]
	public Text scoretxt; //the score text on canvas
	public Text modtxt; //the modifier text on canvas

	RandomNotes rand = null; //randomize script

	public void Initialize () {
		//initialize everything
		blocks = new List<Note_Block>();
        victories = 0;
		rand = this.GetComponent<RandomNotes>();
		sequel = Rhythm_Manager.instance.music.notes;
	}

	public float getOffset(){
		//calculate note travel delay
		return (this.transform.position.y - this.transform.GetChild(0).position.y) / speed;
	}
	
	void Update () {
        scoretxt.text = score.ToString(); //show score on screen
	}

	public void ChangeStep(int count){
		string notes = string.Empty;
		if (rand != null) //if random notes script exists, use it
			notes = rand.generate();
		else if (count < sequel.Length){
			notes = sequel[count]; //get next note ot the song

			//notation:	xL_R-> L for player 1 and R for player 2
			//			1L-> just L for player 1
			//			2R-> just R for player 2
			//			LR-> LR for both
			if(notes != string.Empty){
                if (notes[0] == 'x'){ //if we have different notes for each of them
                    if (playerIndex == 0)
                        notes = notes.Substring(1, notes.IndexOf('_') - 1);
                    else
                        notes = notes.Substring(notes.IndexOf('_') + 1, notes.Length - notes.IndexOf('_') - 1);
                }
                if ((notes[0] == (playerIndex + 1).ToString()[0])){ //if is just to this guy
                    notes = notes.Substring(1, notes.Length - 1);
                }
                if ((notes[0] == (2 - playerIndex).ToString()[0])){ //if is just to the other guy
                    notes = string.Empty;
                }
			}

		}
		if (notes != string.Empty && notes[0] != '-'){ //empty blocks are useless
			Note_Block nb = Instantiate(nbPrefab).GetComponent<Note_Block>();
			nb.Build(notes); //initialize block with the right notes
			nb.transform.position = this.transform.position;
			blocks.Add(nb); //add new block on the list
		}
		count++;
	}

	public void setFreeze(bool value){
        foreach (Note_Block nb in blocks)
			nb.FreezeBlock(value);
    }
}