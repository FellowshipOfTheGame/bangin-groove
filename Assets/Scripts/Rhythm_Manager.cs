using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhythm_Manager : MonoBehaviour {

	public float bpm;
	int count = 1;
	float offset;
	float crotchet;
	float songPos, initialPos;
	public AudioSource source;
	public double dspTimeSong;

	public static Rhythm_Manager instance;
	public Note_Spawner[] spawners;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
		
	}

	// Use this for initialization
	void Start () {
		initialPos = (float)(AudioSettings.dspTime - dspTimeSong) * source.pitch - offset;
		crotchet = 60.0f / bpm;
	}
	
	// Update is called once per frame
	void Update () {
		songPos = (float)(AudioSettings.dspTime - dspTimeSong) * source.pitch - offset - initialPos;
		if (songPos >= count * crotchet){
			foreach (Note_Spawner ns in spawners){
				ns.ChangeStep();
			}
			count++;
		}
	}
}
