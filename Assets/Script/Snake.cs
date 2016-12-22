using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//对数组方法的扩展
using System.Linq;

public class Snake : MonoBehaviour {
	//默认的移动速度
	Vector2 move = Vector2.right;
	//transform对象可以直接修改坐标，和保存坐标
	List<Transform> tailTransformList = new List<Transform>();
	//遇到吃的flag
	bool ate = false;
	//保存尾巴部分
	public GameObject tailPrefab;
	// Use this for initialization
	void Start () {
		//0.3秒开始，每0.2秒重复一次
		InvokeRepeating ("Move", 0.3f, 0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		//按键的控制
		if(Input.GetKey(KeyCode.RightArrow)){
			move = Vector2.right;
		}else if(Input.GetKey(KeyCode.LeftArrow)){
			move = Vector2.left;
		}else if(Input.GetKey(KeyCode.UpArrow)){
			move = Vector2.up;
		}else if(Input.GetKey(KeyCode.DownArrow)){
			move = Vector2.down;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		//遇到食物，销毁食物
		if(col.name.StartsWith("FoodPrefab")){
			ate = true;
			Destroy (col.gameObject);
			//遇到尾巴死亡 遇到墙也死亡
		}else if(col.name.StartsWith("TailPrefab")||col.name.StartsWith("Border")){
			DestroyAllSnake ();
		}

	
	}

	//死亡逻辑
	void DestroyAllSnake(){
		Destroy (gameObject);
		foreach(Transform tf in tailTransformList){
			Destroy(tf.gameObject);
		}
		print ("你输了");
	
	}

	//移动逻辑，整个项目最难点
	void Move(){
		//获取到未处理前的坐标保存到临时变量中
		Vector2 pos = transform.position;
		//头开始移动
		transform.Translate (move);
		//如果可以吃
		if (ate) {
			//实现一个新的尾巴，放在头文件原来的位子，看上去很自然
			GameObject gameObject = Instantiate (tailPrefab,pos,Quaternion.identity);
			//尾巴的transform插入到数组中
			tailTransformList.Insert (0,gameObject.transform);
			ate = false;
		
		}	//普通移动 有尾巴的时候，需要把尾巴也移动，这里只是把最后的一块移动到最强面。然后通过插入，删除就可以不破原有数组。
		//你也可以通过遍历移动，很显然没有该方法优秀
		else if(tailTransformList.Count>0){
			tailTransformList.Last().position = pos;
			tailTransformList.Insert (0,tailTransformList.Last());
			tailTransformList.RemoveAt (tailTransformList.Count-1);
		}
	}
}
