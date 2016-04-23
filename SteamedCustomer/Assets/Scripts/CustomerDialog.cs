using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;

public class CustomerDialog : MonoBehaviour {

	public TextAsset scriptFile;
	public Text customerPrompt;
	public Text csrResponse1;
	public Text csrResponse2;
	public Text csrResponse3;
	public Text csrResponse4;

	XmlDocument dialogScript;
	XmlNodeList scenarioList;
	int emoLevel;

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
		XmlNode node1 = scenarioList[0];

		foreach (XmlNode items in node1.ChildNodes)
		{
			if (items.Name == "prompt")
			{
				customerPrompt.text = items.InnerText;
				Debug.Log("Prompt: " + items.InnerText);
			}
			else if (items.Name == "emoLevel")
			{
				emoLevel = int.Parse(items.InnerText);
				Debug.Log("Emo Level: " + emoLevel);
			}
			else if (items.Name == "response1")
			{
				csrResponse1.text = items.InnerText;
				Debug.Log("Response: " + items.InnerText);
			}
			else if (items.Name == "response2")
			{
				csrResponse2.text = items.InnerText;
				Debug.Log("Response: " + items.InnerText);
			}
			else if (items.Name == "response3")
			{
				csrResponse3.text = items.InnerText;
				Debug.Log("Response: " + items.InnerText);
			}
			else if (items.Name == "response4")
			{
				csrResponse4.text = items.InnerText;
				Debug.Log("Response: " + items.InnerText);
			}
		}
			
	}
}
