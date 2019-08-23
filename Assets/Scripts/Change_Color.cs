using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Change_Color : MonoBehaviour {

	public SpriteMeshInstance[] meshs;

	public Color[] colors;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Paint(int color){
		if(color < colors.Length){
			foreach(SpriteMeshInstance sm in meshs)
				sm.color = colors[color];
		}
	}
}
