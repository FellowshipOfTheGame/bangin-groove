using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rhythm_Manager : MonoBehaviour {

	public float bpm;
	int count = 1;
	public float offset;
	float crotchet;
	float songPos, initialPos;
	public AudioSource source;
    public Animator counter;
    public int songSize;
    public int halfSize;
    public int halfGap;
    public GameObject instructionScreen, middleScreen, suddenScreen, victoryScreen;

    public AudioClip rhythmMusic, mashMusic;
    public Text middleText, victoryText;
    int round;

    bool canSpawn = false, counting = false;

	public static Rhythm_Manager instance;
    public Mash_Manager mash;
	public Note_Spawner[] spawners;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy(this.gameObject);
		
	}

	// Use this for initialization
	void Start () {
        crotchet = 60.0f / bpm;
        round = 1;
        instructionScreen.SetActive(true);
        victoryScreen.SetActive(false);
	}

    public void Play(){
        initialPos = (float)(AudioSettings.dspTime) * source.pitch + offset;
        instructionScreen.SetActive(false);
        canSpawn = true;
        counting = true;
        counter.SetTrigger("Count");
        source.PlayOneShot(rhythmMusic);
    }
	
	// Update is called once per frame
	void Update () {
        if (counting){
            songPos = (float)(AudioSettings.dspTime) * source.pitch - initialPos;

            if (songPos >= count * crotchet)
            {
                if (canSpawn)
                {
                    foreach (Note_Spawner ns in spawners)
                    {
                        ns.ChangeStep();
                        //Debug.Log(songPos.ToString() + " " + count.ToString() + " " + crotchet.ToString());
                    }
                }
                count++;
            }
        }
        
        if (halfSize == count && round == 1) {
            canSpawn = false;
            round++;
            Debug.Log("Pause " + Time.time.ToString());
            Invoke("Pause", 3.5f);
        }
        if (songSize == count && round == 2) {
            canSpawn = false;
            round++;
            CancelInvoke();
            Invoke("FinalScreen", 3.5f);
        }
	}

    void Continue () {
        Debug.Log("Continue " + Time.time.ToString());
        canSpawn = true;
        spawners[0].score = 0;
        spawners[1].score = 0;
        middleScreen.SetActive(false);
    }

    void Pause() {
        middleScreen.SetActive(true);
        if (spawners[0].score > spawners[1].score) {
            spawners[0].victories++;
            middleText.text = "Player 1 wins the first round";
        } else if (spawners[0].score < spawners[1].score) {
            spawners[1].victories++;
            middleText.text = "Player 2 wins the first round ";
        }else{
            spawners[0].victories++;
            spawners[1].victories++;
            middleText.text = "No winners in the first round ";
        }
        Debug.Log(spawners[0].victories + " X " + spawners[1].victories);
        Invoke("ReCount", halfGap * crotchet - 3.0f + (spawners[0].transform.position.y - spawners[0].finalPos.y) / spawners[0].speed);
        Invoke("Continue", halfGap * crotchet);
    }

    void ReCount(){
        middleScreen.SetActive(false);
        spawners[0].score = 0;
        spawners[1].score = 0;
        counter.SetTrigger("Count");
    }

    void FinalScreen() {
        if (spawners[0].score > spawners[1].score) spawners[0].victories++;
            else if (spawners[0].score < spawners[1].score) spawners[1].victories++;
            else{
                spawners[0].victories++;
                spawners[1].victories++;
            }
        Debug.Log(spawners[0].victories + " X " + spawners[1].victories);
        if (spawners[0].victories > spawners[1].victories) {
            victoryScreen.SetActive(true);
            victoryText.text = "Player 1 wins";
        } else if (spawners[0].victories < spawners[1].victories) {
            victoryScreen.SetActive(true);
            victoryText.text = "Player 2 wins";
        } else {
            suddenScreen.SetActive(true);
        }
    }

    public void StartMash(){
        suddenScreen.SetActive(false);
        mash.gameObject.SetActive(true);
        spawners[0].gameObject.SetActive(false);
        spawners[1].gameObject.SetActive(false);
        mash.Invoke("Initialize", 3.2f);
        counter.SetTrigger("Count");
        //mash.Initialize();
    }

    public void Reset() {
        SceneManager.LoadScene("RhythmTest");
    }
}
