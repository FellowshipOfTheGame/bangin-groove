using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

	static float spd = 1000.0f;
	Rigidbody2D rigid;
	public Vector3 pos, rot;
	public Player player;
	
	void Start(){
		rigid = this.GetComponent<Rigidbody2D>();
	}

	public void Fly(int dir){
		Debug.Log("DIR" + dir.ToString());
		if(player!=null){
			this.transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
			this.transform.localEulerAngles = rot;
		}
		rigid = this.GetComponent<Rigidbody2D>();
		rigid.velocity =  Quaternion.Euler(0.0f, 0.0f, -dir * 40.1f) * this.transform.right * dir * spd * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			rigid.velocity = Vector2.zero;

			this.transform.SetParent(col.transform.parent.GetChild(1).GetChild(0).GetChild(0));
			
			col.transform.parent.GetComponent<Player>().Die(this.GetComponent<SpriteRenderer>(), player);
			Debug.Log("Morri");
		}
	}
}
