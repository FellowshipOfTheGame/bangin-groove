using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note_Spawner : MonoBehaviour {

	//public Music music;
	public float speed;
	public int score;
	public Vector3 finalPos;
	public GameObject nbPrefab;
	public List<Note_Block> blocks;
	//int score e bool canplay;

	// Use this for initialization
	void Start () {
		blocks = new List<Note_Block>();
		Rhythm_Manager.instance.offset -= Time.deltaTime * (this.transform.position.y - finalPos.y) / speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ChangeStep(){
		string notes = RandomNote();
		if (notes != string.Empty){
			Note_Block nb = Instantiate(nbPrefab).GetComponent<Note_Block>();
			nb.Build(notes);
			nb.transform.position = this.transform.position;
			blocks.Add(nb);
		}
	}

	string RandomNote(){
		string notes = string.Empty;
		int aux = Random.Range(1, 7);	
			switch (aux){
				case 1:
					if(notes != "L") notes+="L";
					break;
				case 2:
					if(notes != "D") notes+="D";
					break;
				case 3:
					if(notes != "U") notes+="U";
					break;
				case 4:
					if(notes != "R") notes+="R";
					break;

			}
		if (aux > 4)
			aux = Random.Range(1, 7);
		else
			aux = Random.Range(aux + 1, 7);

		switch (aux){
				case 1:
					if(notes != "L") notes+="L";
					break;
				case 2:
					if(notes != "D") notes+="D";
					break;
				case 3:
					if(notes != "U") notes+="U";
					break;
				case 4:
					if(notes != "R") notes+="R";
					break;

			}
		
		return notes;
	}
}
