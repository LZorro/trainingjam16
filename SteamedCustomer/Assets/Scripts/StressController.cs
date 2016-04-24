using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StressController : MonoBehaviour {

	public Slider stressMeter;
	public float stressRate;
	float timer;
	bool isStressing;
	public int stressLevel;

	// Use this for initialization
	void Start () {
		timer = stressRate;
		isStressing = true;
	}
	
	// Update is called once per frame
	void Update () {
		stressMeter.value = stressLevel;
		if (isStressing)
		{
			timer -= Time.deltaTime;
			if (timer <= 0.0f)
			{
				stressLevel++;
				timer = stressRate;
			}
		}
	}

	public void startStress()
	{
		timer = stressRate;
		isStressing = true;
	}

	public void stopStress()
	{
		isStressing = false;
	}

	public void reduceStress()
	{
		stressLevel--;
	}

	public void increaseStressRate()
	{
		stressRate /= 2.0f;
	}

	public void decreaseStressRate()
	{
		stressRate *= 2.0f;
	}

	public void joltStress()
	{
		stressLevel += 10;
	}
}
