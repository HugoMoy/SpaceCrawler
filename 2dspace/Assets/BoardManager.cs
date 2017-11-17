using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable] public class Count {
		public int min;
		public int max;

		public Count(int pmin, int pmax) {
			min = pmin;
			max = pmax;
		}


	}
		//public GameObject camera;
		//public GameObject player;
		public GameObject exitdown;
		public GameObject exitup;
		public GameObject exitleft;
		public GameObject exitright;
		public GameObject start;
		public GameObject[] allChunks;
		public GameObject[] openUpChunks;
		public GameObject[] openDownChunks;
		public GameObject[] openLeftChunks;
		public GameObject[] openRightChunks;
		public GameObject[] endUpChunks;
		public GameObject[] endDownChunks;
		public GameObject[] endLeftChunks;
		public GameObject[] endRightChunks;
		public GameObject[] outerWallChunks;
		public List<string> OrientationNextPos;
		public List<Vector3> nextPosition;

		private Transform boardHolder;
		private List<Vector3> gridPositions = new List<Vector3>();

		// void initializeFloor(){
		// 	gridPositions.Clear();
		// 	for(int i = 0; i < columns; i++) {
		// 		for(int j = 0; j < rows; j++) {
		// 			gridPositions.Add(new Vector3(i,j,0f));
		// 		}
		// 	}
		// }

		// void boardSetup(){
		// 	boardHolder = new GameObject("Board").transform;
		// 	for(int i = -1; i < columns+1; i++) {
		// 		for(int j = -1; j < rows+1; j++) {
		// 			Debug.Log("heyho"+i+" "+j);
		// 			GameObject toInstantiate = allChunks[Random.Range(0,allChunks.Length)];
		// 			if(i == -1 || j == -1 || i == columns || j == rows) {
		// 				toInstantiate = outerWallChunks[Random.Range(0, outerWallChunks.Length)];
		// 			}
		// 			GameObject instance = Instantiate(toInstantiate, new Vector3(i*8,j*8,0f), Quaternion.identity) as GameObject;
		// 			instance.transform.SetParent(boardHolder);
		// 			Debug.Log(boardHolder.transform);
		// 		}
		// 	}
		// 	for(int i = 0; i < boardHolder.childCount; i++){
		// 		Debug.Log(boardHolder.GetChild(i));
		// 	}
		// }
		
		
		bool isPositionFree(Vector3 pos) {
			for(int i =0; i < boardHolder.childCount; i++){
				if(boardHolder.GetChild(i).transform.position == pos){
					return false;
				}
			}
			return true;
		}
		void fillNextPos(GameObject inst, string anterior){
			Component[] comp = inst.GetComponentsInChildren<Transform>();
			for(int i = 0; i < comp.Length; i++) {
				if(comp[i].name != anterior && comp[i].name == "left") {
					Vector3 np = new Vector3(inst.transform.position.x-8, inst.transform.position.y,0f);
					if(isPositionFree(np)){
						nextPosition.Add(np);
						OrientationNextPos.Add("right");
					}

				}
				if(comp[i].name != anterior && comp[i].name == "right") {
					Vector3 np = new Vector3(inst.transform.position.x+8, inst.transform.position.y,0f);
					if(isPositionFree(np)){
						nextPosition.Add(np);
						OrientationNextPos.Add("left");
					}
				}
				if(comp[i].name != anterior && comp[i].name == "up") {
					Vector3 np = new Vector3(inst.transform.position.x, inst.transform.position.y+8,0f);
					if(isPositionFree(np)){
						nextPosition.Add(np);
						OrientationNextPos.Add("down");
					}
				}
				if(comp[i].name != anterior && comp[i].name == "down") {
					Vector3 np = new Vector3(inst.transform.position.x, inst.transform.position.y-8,0f);
					if(isPositionFree(np)){
						nextPosition.Add(np);
						OrientationNextPos.Add("up");
					}
				}
				//Debug.Log(inst.name + " : " + " name of comp "+i+""+comp[i].name);
			}
			//inst.GetComponentInChildren
		}
		void addNewLayer(int level){
			//Debug.Log("nb in list next pos : "+OrientationNextPos.Count);
			int k = 0;
			while( k < level*3+2) {
				GameObject toInstantiate;
				GameObject instance; 

				string n = OrientationNextPos[0];
				OrientationNextPos.RemoveAt(0);
				Vector3 pos = nextPosition[0];
				nextPosition.RemoveAt(0);
				//Debug.Log(n);
				if(n == "left" && isPositionFree(pos)) {
					toInstantiate = openLeftChunks[Random.Range(0,openLeftChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					fillNextPos(instance,n);
					
				}
				else if(n == "right" && isPositionFree(pos)) {
					toInstantiate = openRightChunks[Random.Range(0,openRightChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					fillNextPos(instance,n);
					
				}
				else if (n == "up" && isPositionFree(pos)) {
					toInstantiate = openUpChunks[Random.Range(0,openUpChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					fillNextPos(instance,n);
				}
				else if (n == "down" && isPositionFree(pos)){
					toInstantiate = openDownChunks[Random.Range(0,openDownChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					fillNextPos(instance,n);
				}
				k++;
			}


		}

		void clearDoubles() {
			for(int i = 0; i < nextPosition.Count; i++) {
				for(int j = 0; j < nextPosition.Count; j++) {
					if(i != j && nextPosition[i] == nextPosition[j] ){
						OrientationNextPos.RemoveAt(i);
						nextPosition.RemoveAt(i);
						break;
					}
				}
			}
		}
		void addNoWayTiles() {
			Debug.Log("nb in list next pos : "+OrientationNextPos.Count);
			int k = 0;
			clearDoubles();
			while( OrientationNextPos.Count > 1) {
				GameObject toInstantiate;
				GameObject instance; 

				string n = OrientationNextPos[0];
				OrientationNextPos.RemoveAt(0);
				Vector3 pos = nextPosition[0];
				nextPosition.RemoveAt(0);
				Debug.Log(n);
				if(n == "left" && isPositionFree(pos)) {
					toInstantiate = endLeftChunks[Random.Range(0,endLeftChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					
				}
				else if(n == "right" && isPositionFree(pos)) {
					toInstantiate = endRightChunks[Random.Range(0,endRightChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					
				}
				else if (n == "up" && isPositionFree(pos)) {
					toInstantiate = endUpChunks[Random.Range(0,endUpChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				else if (n == "down" && isPositionFree(pos)){
					toInstantiate = endDownChunks[Random.Range(0,endDownChunks.Length)];
					instance = Instantiate(toInstantiate, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					fillNextPos(instance,n);
				}
				k++;
			}


		}

		void addExit() {
			if(OrientationNextPos.Count == 1) {
				GameObject instance; 
				string n = OrientationNextPos[0];
				OrientationNextPos.RemoveAt(0);
				Vector3 pos = nextPosition[0];
				nextPosition.RemoveAt(0);
				Debug.Log(n);
				if(n == "left" && isPositionFree(pos)) {
					instance = Instantiate(exitleft, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					
				}
				else if(n == "right" && isPositionFree(pos)) {
					instance = Instantiate(exitright, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					
				}
				else if (n == "up" && isPositionFree(pos)) {
					instance = Instantiate(exitup, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
				}
				else if (n == "down" && isPositionFree(pos)){
					instance = Instantiate(exitdown, pos, Quaternion.identity) as GameObject;
					instance.transform.SetParent(boardHolder);
					fillNextPos(instance,n);
				}
			}
			else {
				Debug.Log("Error no exit possible");
			}
		}
		void boardSetupBetter(int level){
			//Debug.Log("Hello from boardsetup start");
			boardHolder = new GameObject("Board").transform;
			GameObject instance = Instantiate(start, new Vector3(0,0,0f), Quaternion.identity) as GameObject;
			//Debug.Log("start instantiated");
			OrientationNextPos.Add("down");
			nextPosition.Add(new Vector3(0,8,0f));
			instance.transform.SetParent(boardHolder);
			Debug.Log(instance.transform);
			//Debug.Log("Hello from boardsetup middle");
			addNewLayer(level);

			addNoWayTiles();

			addExit();
			//Debug.Log("Hello from boardsetup end");

			// toInstantiate = player;
			// Transform[] list;
			// list = start.GetComponentsInChildren<Transform>();
			// foreach(Transform tf in list) {
			// 	if(tf.name == "spawn"){
			// 		Debug.Log("Start found");
			// 		instance = Instantiate(toInstantiate, tf.position, Quaternion.identity) as GameObject;
			// 		toInstantiate = camera;
			// 		instance = Instantiate(toInstantiate, tf.position, Quaternion.identity) as GameObject;
			// 		break;
			// 	}
			// }
			//GameObject 
			//}
			// add open layers
			// Vector3 NextPos = getNextCoordinates();
			// toInstantiate = allChunks[Random.Range(0,allChunks.Length)];
			// 			// toInstantiate = outerWallChunks[Random.Range(0, outerWallChunks.Length)];
				
			// 	instance = Instantiate(toInstantiate, new Vector3(8,8,0f), Quaternion.identity) as GameObject;
			// 	instance.transform.SetParent(boardHolder);
			// 		Debug.Log(boardHolder.transform);
				
			
		}
		public void setupScene(int level) {
			//Debug.Log("Hello from board setupscene");
			//clearBoard();
			boardSetupBetter(level);
			//initializeFloor();
		}


		// public int columns = 4;
		// public int rows = 4;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
}
