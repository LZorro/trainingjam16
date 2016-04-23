using UnityEngine;
using UnityEngine.UI;
using System.Collections;

enum DragonState
{
	enter, 
	exit, 
	attack, 
	dead
};

public class DragonController : MonoBehaviour {

	//public GameObject customer;
	public Slider stallHealth;
	public float timeDragonDescend;
	float timer;
	Vector3 tempPos;
	DragonState currentState;

	// Use this for initialization
	void Start () {
		currentState = DragonState.dead;
		timer = timeDragonDescend;
	}
	
	// Update is called once per frame
	void Update () {

		switch (currentState)
		{
			case DragonState.dead:				
				if (timer <= 0.0f)
				{
					currentState = DragonState.enter;
					timer = 5.0f;  // 5 seconds to descend
					stallHealth.GetComponentInParent<Canvas>().enabled = true;
					tempPos = this.transform.position;
					tempPos.y -= 3.0f;
				}
			break;

			case DragonState.enter:
				this.transform.position = Vector3.MoveTowards(this.transform.position, tempPos, 0.1f);
				if (timer <= 0.0f)
				{
					currentState = DragonState.attack;
				}
			break;

			case DragonState.attack:
				damageStall();
			break;

			case DragonState.exit:
				this.transform.position = Vector3.MoveTowards(this.transform.position, tempPos, 0.1f);
				if (timer <= 0.0f)
				{
					currentState = DragonState.dead;
					timer = timeDragonDescend;
					stallHealth.value = stallHealth.maxValue;
				}
			break;
		}
		timer -= Time.deltaTime;
	}

	void damageStall()
	{
		stallHealth.value -= 1;
		if (stallHealth.value <= 0)
		{
			timer = 5.0f;
			currentState = DragonState.exit;
			stallHealth.GetComponentInParent<Canvas>().enabled = false;
			tempPos = this.transform.position;
			tempPos.y += 3.0f;
		}
	}
}
