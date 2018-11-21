using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mash_Manager : MonoBehaviour {

    int[] counts;
    public float timeLimit;
    float timeStart;
    public int clickLimit;
    public Transform[] marks;
    int testkey;
    KeyCode[] keys;

    public Sprite[] arrows;
    public Image[] notes;

    public GameObject victoryScreen, hits;
    public Text victoryText;

    float initialDistance;
    bool end = true;
    //Keycode key;
    //public Animator[] players;

    // Use this for initialization
    void Start () {
        counts = new int[2];
        initialDistance = marks[1].position.x;
        //Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        if (!end) {
            if (Input.GetKeyDown(keys[0])) {
                counts[0]++;
                marks[0].position += (initialDistance / clickLimit) * Vector3.right;
            }
            if (Input.GetKeyDown(keys[1])) {
                counts[1]++;
                marks[1].position += (initialDistance / clickLimit) * Vector3.left;
            }

            if (Time.time - timeStart >= timeLimit || counts[0] >= clickLimit || counts[1] >= clickLimit) {
                victoryScreen.SetActive(true);
                hits.SetActive(false);
                end = true;
                if (counts[0] > counts[1]) {
                    victoryText.text = "Player 1 wins";
                } else if (counts[0] < counts[1]) {
                    victoryText.text = "Player 2 wins";
                }
            }
        }
    }

    public void Initialize () {
        hits.SetActive(true);
        keys = new KeyCode[2];
        testkey = Random.Range(1, 4);
        if(testkey == 1) {
            keys[0] = KeyCode.LeftArrow;
            keys[1] = KeyCode.A;
        }
        else if (testkey == 2) {
            keys[0] = KeyCode.DownArrow;
            keys[1] = KeyCode.S;
        }
        else if (testkey == 3) {
            keys[0] = KeyCode.UpArrow;
            keys[1] = KeyCode.W;
        }
        else if (testkey == 4) {
            keys[0] = KeyCode.RightArrow;
            keys[1] = KeyCode.D;
        }
        foreach(Image note in notes){
            note.sprite = arrows[testkey - 1];
        }
        end = false;
        timeStart = Time.time;
    }
}
