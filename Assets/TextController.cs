using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

	public Text text;

	private enum States {start, end, guest, input, granted, meltdown};
	private object[] AlphaNumChars = {KeyCode.A,KeyCode.B,KeyCode.C,KeyCode.D,KeyCode.E,KeyCode.F,KeyCode.G,KeyCode.H,KeyCode.I,KeyCode.J,
									KeyCode.K,KeyCode.L,KeyCode.M,KeyCode.N,KeyCode.O,KeyCode.P,KeyCode.Q,KeyCode.R,KeyCode.S,KeyCode.T,
									KeyCode.U,KeyCode.V, KeyCode.W,KeyCode.X,KeyCode.Y,KeyCode.Z, KeyCode.Asterisk,
									 KeyCode.Slash, KeyCode.Minus, KeyCode.Period, KeyCode.Plus, KeyCode.Space};
	private string inputString, path, user;
	private char inputKey;
	
	bool cursor;

	private States gamestate;
	// Use this for initialization

	void Start () {
		gamestate = States.start;
		text.text = "Start your day? Y/N";
	}
	
	// Update is called once per frame
	
	void Update () {
		//start
		if (gamestate == States.start) {
			if (Input.GetKeyDown(KeyCode.Y)) {
				text.text = "Have a nice day, worker\n";
				gamestate = States.guest;
			} else if (Input.GetKeyDown(KeyCode.N)) {
				End();
			}
		}

		if (gamestate == States.guest) {
			Greet();
		}

		if (gamestate == States.input) {
			foreach(KeyCode aKey in AlphaNumChars ) {
				if(Input.GetKeyDown(aKey)){
					
					string askey = aKey.ToString();
					if (askey == "Space"){askey = " ";}
					if (askey == "Asterisk"){askey = "*";}
					if (askey == "Slash"){askey = "/";}
					if (askey == "Plus"){askey = "+";}
					if (askey == "Minus"){askey = "-";}	

					inputString += askey;   
					text.text += askey;
					}
				}

				if (Input.GetKeyDown(KeyCode.Backspace)) {
					text.text = text.text.Remove(text.text.Length-1);
					inputString = inputString.Remove(inputString.Length-1);
				}

				if(Input.GetKeyDown(KeyCode.Return)){
					string[] inputStringArray = inputString.Split(' ');
					if (inputStringArray[0] == "HELP") {
						text.text = "";
						text.text += "LS ... LISTS ALL ASSETS\n"
						+"CD ... CHANGE DIRECTORY\n"
						+"RM ... REMOVE A FILE\n"
						+"MV ... MOVE A FILE\n"
						+"EXIT ... EXIT CONSOLE\n"
						+"HELP ... THIS HELP MESSAGE";
					}

					if (inputStringArray[0] == "LS") {
						if (path == "~") {
							text.text = "";
							text.text += "FILE LISTING:\n";
							text.text += "<ADMIN>\n";
							text.text += "<BOSS>\n";
							text.text += "WORK.TXT\n";
							text.text += "LONG-LIFE-POEM.TXT";
						}	
						if (path == "ADMIN") {
							text.text = "";
							text.text += "FILE LISTING:\n";
							text.text += "..\n";
							text.text += "PASSWORDS.TXT\n";
							text.text += "NOTES.TXT";
						}

						if (path == "BOSS") {
							text.text = "";
							text.text += "FILE LISTING:\n";
							text.text += "..\n";
							text.text += "JOKES.TXT\n";
							text.text += "SO-BORED-POEM.TXT";
						}						

					}

					if(inputStringArray[0] == "EXIT") {
						End();
					}

					if(inputStringArray[0] == "CD") {

						if(inputStringArray[1] == "BOSS") {
							text.text =  "BOSS DIRECTORY";
							path = "BOSS";
						}
						if(inputStringArray[1] == "ADMIN") {
							text.text =  "ADMIN DIRECTORY";
							path = "ADMIN";
						}
					}

					if(inputString == "MV") {
						End();
					}

					if(inputString == "RM") {
						End();
					}					

					text.text += "\n@" +path +"/" +user +"$: ";
					inputString = "";
				}
			}
		}
		//update
		void Greet () {
			path = "~";
			user = "worker";
			text.text = "Welcome to Allwork OS You're day starts now and ends in eight hours. The factory manager will see to it\n"
			+"that you don't need pauses. Get to it! At anytime, type  HELP to See  commands\n";
			text.text += "Press Space key to continue..";

			if(Input.GetKeyDown(KeyCode.Space) && gamestate == States.guest) {
				text.text += "\n@" +path +"/" +user +"$: ";
				inputString = "";
				gamestate = States.input;
			}
		}

	
		void End () {
			text.text = "Goodbye!";
			gamestate = States.end;
		}

}
