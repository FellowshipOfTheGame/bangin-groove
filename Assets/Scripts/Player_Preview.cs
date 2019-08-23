using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player_Preview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Player player;

	public Image art;
	public Image title;

	public Animator tab;

	public Text flavor;

	public Char_Selector sel;
	public int index;

	Button button;

	static Player_Preview main;

	void Start () {
		button = this.GetComponent<Button>();
	}

    public void OnPointerEnter(PointerEventData eventData){
				main = this;
        if (button.interactable && sel.index == index){
					SetStuff();

					tab.SetTrigger("open");
				}
    }

		void SetStuff(){
				art.sprite = player.charArt;
				title.sprite = player.nameArt;
				flavor.text = player.flavor;
		}

		void EraseStuff(){
				art.sprite = null;
				title.sprite = null;
				flavor.text = string.Empty;
		}

	public void OnPointerExit(PointerEventData eventData){
				if (main == this) main = null;

        if (button.interactable && sel.index == index){
					tab.SetTrigger("close");
					EraseStuff();
				}
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
