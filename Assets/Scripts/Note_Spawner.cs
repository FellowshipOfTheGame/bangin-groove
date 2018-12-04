using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Note_Spawner : MonoBehaviour {

	int count = 0; //control counter

	public float speed; //the speed of each note
	public GameObject nbPrefab; //the prefab of an note block

	[HideInInspector] public int score; //player score count
	[HideInInspector] public int victories; //player victory count
	[HideInInspector] public List<Note_Block> blocks; //spawned block list

	[Space(5)]
	public Text scoretxt; //the score text on canvas
    
	RandomNotes rand = null; //randomize script

	void Start () {
		//initialize everything
		blocks = new List<Note_Block>();
        victories = 0;
		rand = this.GetComponent<RandomNotes>();
	}

	public float getOffset(){
		//calculate note travel delay
		return (this.transform.position.y - this.transform.GetChild(0).position.y) / speed;
	}
	
	void Update () {
        scoretxt.text = "Score: " + score.ToString(); //show score on screen
	}

	public void ChangeStep(){
		string notes;
		if (rand != null) //if random notes script exists, use it
			notes = rand.generate();
		else
			notes = Rhythm_Manager.instance.music.notes[count]; //get next note ot the song

		if (notes != string.Empty){ //empty blocks are useless
			Note_Block nb = Instantiate(nbPrefab).GetComponent<Note_Block>();
			nb.Build(notes); //initialize block with the right notes
			nb.transform.position = this.transform.position;
			blocks.Add(nb); //add new block on the list
		}
	}
}