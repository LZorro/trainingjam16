using UnityEngine;
using System.Collections;

public class CSRResponseClick : MonoBehaviour {

	public int emoDiff;
	public int nextPrompt;
	public GameObject customer;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void buttonClicked()
	{
		customer.GetComponent<CustomerDialog>().changeEmo(emoDiff, nextPrompt);
	}
}
