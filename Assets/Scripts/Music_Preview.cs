using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_Preview : MonoBehaviour
{
	public Music[] musics;
	public AudioSource source;

    public Image album;

    public GameObject loadScr;

    int index = 0;

    // Start is called before the first frame update
    void Start(){
        source.clip = musics[index].song;
        album.sprite = musics[index].art;
        source.volume = musics[index].volume;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	public void SetMusic(int dir){
        if(dir == 1){
            index++;
            if (index == musics.Length)
                index = 0;
        }else{
            index--;
            if (index == -1)
                index = musics.Length - 1;
        }

        source.Stop();
        source.clip = musics[index].song;
        album.sprite = musics[index].art;
        source.volume = musics[index].volume;
        source.Play();
	}

    public void ChooseMusic(){
        source.Stop();
        loadScr.SetActive(true);
        Game_Manager.instance.chooseMusic(musics[index]);
    }
}
