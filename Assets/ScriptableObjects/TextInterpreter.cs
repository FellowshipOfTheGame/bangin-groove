using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class TextInterpreter : MonoBehaviour {

	public static string[] GetSteps(TextAsset file, out int halfSong, out int endSong){
		//remove empty lines
		string text = Regex.Replace(file.text, @"^\s*$[\r\n]*", string.Empty, RegexOptions.Multiline);
		List<string> step = new List<string>();
		halfSong = 0;
		endSong = 0;


		while (text.Length > 0){
			string line = GetLine(text, "\n", out text);

			//comments sequence
			if(line[0] == '#'){
				int index = line.IndexOf(' ');
				if (line[index + 1] == '1') halfSong = step.Count;
				else if (line[index + 1] == '2') endSong = step.Count;
				Debug.Log(step.Count + " " + line);
				continue;

			}
			while(line.Length > 0) Decode(step, GetLine(line, "; ", out line) );
		}

		string[] notes = new string[step.Count];
		for(int i=0; i<step.Count; i++){
			notes[i] = step[i];
		}

		return notes;
	}

	static void Decode(List<string> step, string code){
		int division = code.IndexOf(' ');
		if(division != -1) step.Add( code.Substring(0, division) );
		for (int i = 1; i <= int.Parse( code.Substring(division + 1) ); i++){
			step.Add("-");
		}
	}

	static string GetLine(string text, string divisor, out string buffer){
		int index = text.IndexOf(divisor);
		string aux = text;

		if(index != -1){
			aux = text.Substring(0, index);
			buffer = text.Substring(index + divisor.Length);
		}else{
			buffer = "";
		}

		return aux;
	}
}
