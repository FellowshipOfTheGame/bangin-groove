using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Change_Color_Editor : MonoBehaviour {

	[Range(0,4)] public int color;



	// Use this for initialization
	void OnValidate(){
		GetComponent<Change_Color>().Paint(color);
	}
}
