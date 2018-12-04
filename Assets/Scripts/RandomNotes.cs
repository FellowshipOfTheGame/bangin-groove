using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNotes : MonoBehaviour {

	//return an factible random note block code
	public string generate(){
		string notes = string.Empty;
		int aux = Random.Range(1, 4); //sort first note
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
		
		aux = Random.Range(aux + 1, 10); //sort second note

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