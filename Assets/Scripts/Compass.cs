using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {


	float bpm;
	int count = 1;
	float offset;
	float crotchet;
	float songPos, initialPos;
	bool counting = false;
	public AudioSource source;
	public AudioClip rhythmMusic;

	public int forward;

	public delegate void Step(int counter);
	public Step step;

	// Use this for initialization


	public void Initialize(Music music, float spawnOffset){
		rhythmMusic = music.song;
		bpm = music.bpm;
		offset = music.offset - spawnOffset;
		crotchet = 60.0f / bpm;
		step = delegate {count++;};
	}
	
	public void Play(){
        initialPos = (float)(AudioSettings.dspTime) * source.pitch + offset;
        counting = true;
		
		source.clip = rhythmMusic;
		source.time = forward * crotchet;
        source.Play();
    }

	// Update is called once per frame
	void Update () {
		if (counting){
            songPos = (float)(AudioSettings.dspTime) * source.pitch - initialPos;

            if (songPos >= count * crotchet)
                step(count - 1 + forward);
        }
	}
}
