using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song", menuName = "Song")]
public class Music : ScriptableObject {

	public AudioClip song; //the audio
	public string title; 

	public Sprite art;
	public float volume;
	public int bpm;
	[Space(5)]
	public int songSize, halfSize, halfGap; //number of notes in all song, 1st round and the gap between rounds
	public float offset, ajust, ending; //adjusts of time on start and end of the song
	[Space(5)]

	public TextAsset notes;

	public string[] moves;
	

	
	//public AudioClip preview;
	//public Sprite album;
	//public string artist;

}
