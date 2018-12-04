using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Song")]
public class Music : ScriptableObject {

	public AudioClip song; //the audio
	public string title; 
	public string[] notes; //note code sequel
	public int bpm;
	public int songSize, halfSize, halfGap; //number of notes in all song, 1st round and the gap between rounds
	public float offset, ending; //adjusts of time on start and end of the song
	
	//public AudioClip preview;
	//public Sprite album;
	//public string artist;

}
