using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour {

	public GameObject foodPrefab;
	//border
	public Transform borderTop;
	public Transform borderBottom;
	public Transform borderLeft;
	public Transform borderRight;

	// Use this for initialization
	void Start () {
		//2秒后开始运行，然后每1秒钟运行1次
		InvokeRepeating ("Spawn",2,1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//随机实例化坐标豆子。
	void Spawn(){
		int x = (int)Random.Range (borderLeft.position.x+2f,borderRight.position.x);
		int y =(int)Random.Range (borderBottom.position.y+2f,borderTop.position.y);
		Instantiate (foodPrefab,new Vector2(x,y),Quaternion.identity);
	}
}
