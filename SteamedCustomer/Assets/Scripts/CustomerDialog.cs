using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;

public class CustomerDialog : MonoBehaviour {

	public TextAsset scriptFile;

	XmlDocument dialogScript;
	XmlNodeList scenarioList;

	public Text customerPrompt;
	public Button csrResponse1;

	// Use this for initialization
	void Start () {
		InitScript();
		LoadScenario();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void InitScript()
	{
		dialogScript = new XmlDocument();
		dialogScript.LoadXml(scriptFile.text);
		scenarioList = dialogScript.GetElementsByTagName("scenario");
	}

	void LoadScenario()
	{
		
	}
}
