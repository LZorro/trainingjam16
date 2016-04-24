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
	public GameObject reticle;
	public GameObject weapon;
	public float timeDragonDescend;
	public int maxDragonHealth = 5;
	public float maxReloadTime = 1.0f;
	bool isTimerRunning;
	float timer;
	float reloadTimer;
	bool isReloading;
	Vector3 tempPos;
	DragonState currentState;
	int dragonHealth;
	Vector3 initPosition;

	void Awake()
	{
		initPosition = this.transform.position;
	}

	// Use this for initialization
	void Start () {
		currentState = DragonState.dead;
		isTimerRunning = true;
		timer = timeDragonDescend;
		dragonHealth = maxDragonHealth;
		reloadTimer = maxReloadTime;
		isReloading = false;
	}

	public void resetDragon()
	{
		this.transform.position = initPosition;
		currentState = DragonState.dead;
		isTimerRunning = true;
		timer = timeDragonDescend;
		dragonHealth = maxDragonHealth;
		reloadTimer = maxReloadTime;
		isReloading = false;
		stallHealth.value = stallHealth.maxValue;
		stallHealth.GetComponentInParent<Canvas>().enabled = false;
		reticle.GetComponent<SpriteRenderer>().enabled = false;
		weapon.GetComponent<SpriteRenderer>().enabled = false;
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
					reticle.GetComponent<SpriteRenderer>().enabled = true;
					weapon.GetComponent<SpriteRenderer>().enabled = true;
					tempPos = this.transform.position;
					tempPos.y -= 3.0f;
					GameObject.Find("Stress Button").GetComponent<StressController>().increaseStressRate();
				}
			break;

			case DragonState.enter:
				this.transform.position = Vector3.MoveTowards(this.transform.position, tempPos, 0.02f);
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
					dragonHealth = maxDragonHealth;
				}
			break;
		}

		if (isTimerRunning)
			timer -= Time.deltaTime;

		if (isReloading)
		{
			reloadTimer -= Time.deltaTime;
			if (reloadTimer <= 0)
			{
				isReloading = false;
				reloadTimer = maxReloadTime;
			}
		}
			
	}

	void damageStall()
	{
		stallHealth.value -= 1;
		if (stallHealth.value <= 0)
		{
			Debug.Log("Dragon flew off");
			exitDragonState();
		}
	}

	void OnMouseDown()
	{
		//Debug.Log("DRAGON HIT");
		if (!isReloading)
		{
			dragonHealth--;
			isReloading = true;
		}

		if (dragonHealth <= 0)
		{
			Debug.Log("Dragon killed!");
			exitDragonState();
		}
	}

	void exitDragonState()
	{
		timer = 5.0f;
		currentState = DragonState.exit;
		stallHealth.GetComponentInParent<Canvas>().enabled = false;
		reticle.GetComponent<SpriteRenderer>().enabled = false;
		weapon.GetComponent<SpriteRenderer>().enabled = false;
		tempPos = this.transform.position;
		tempPos.y += 3.0f;
		GameObject.Find("Stress Button").GetComponent<StressController>().decreaseStressRate();
	}

	public void stopDragonTimer()
	{
		isTimerRunning = false;
	}

	public void startDragonTimer()
	{
		isTimerRunning = true;
	}
}
