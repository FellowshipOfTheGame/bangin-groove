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
	}
	
	// Update is called once per frame
	void Update () {
		if (blocks[0].transform.position.y <= -8.0f){ 
			Destroy(blocks[0].gameObject);
			blocks.RemoveAt(0);
		}
	}

	public void ChangeStep(){
		Note_Block nb = Instantiate(nbPrefab).GetComponent<Note_Block>();
		nb.Build(RandomNote());
		nb.transform.position = this.transform.position;
		blocks.Add(nb);
	}

	string RandomNote(){
		string notes = "";
		for (int i = 0; i < 2; i++){
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
		}
		return notes;
	}
}
