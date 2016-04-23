using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Xml;

public class CustomerDialog : MonoBehaviour {

	public TextAsset scriptFile;
	public Text customerPrompt;
	public Button csrResponse1;
	public Button csrResponse2;
	public Button csrResponse3;
	public Button csrResponse4;

	public Sprite face_neutral;
	public Sprite face_happy;
	public Sprite face_angry;

	XmlDocument dialogScript;
	XmlNodeList scenarioList;
	int emoLevel;
	int currentScenario;

	// Use this for initialization
	void Start () {
		currentScenario = 0;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = face_neutral;

		InitScript();
		LoadScenario(currentScenario);
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

	void LoadScenario(int currSen)
	{
		XmlNode node1 = scenarioList[currSen];

		foreach (XmlNode items in node1.ChildNodes)
		{
			if (items.Name == "prompt")
			{
				customerPrompt.text = items.InnerText;
			}
			else if (items.Name == "emoLevel")
			{
				emoLevel = int.Parse(items.InnerText);
				setFace();
				Debug.Log("Start emo: " + emoLevel);
			}
			else if (items.Name == "response1")
			{
				csrResponse1.GetComponentInChildren<Text>().text = items.InnerText;
				csrResponse1.GetComponent<CSRResponseClick>().emoDiff = int.Parse(items.Attributes["diff"].Value);
				csrResponse1.GetComponent<CSRResponseClick>().nextPrompt = int.Parse(items.Attributes["next"].Value);
			}
			else if (items.Name == "response2")
			{
				csrResponse2.GetComponentInChildren<Text>().text = items.InnerText;
				csrResponse2.GetComponent<CSRResponseClick>().emoDiff = int.Parse(items.Attributes["diff"].Value);
				csrResponse2.GetComponent<CSRResponseClick>().nextPrompt = int.Parse(items.Attributes["next"].Value);
			}
			else if (items.Name == "response3")
			{
				csrResponse3.GetComponentInChildren<Text>().text = items.InnerText;
				csrResponse3.GetComponent<CSRResponseClick>().emoDiff = int.Parse(items.Attributes["diff"].Value);
				csrResponse3.GetComponent<CSRResponseClick>().nextPrompt = int.Parse(items.Attributes["next"].Value);
			}
			else if (items.Name == "response4")
			{
				csrResponse4.GetComponentInChildren<Text>().text = items.InnerText;
				csrResponse4.GetComponent<CSRResponseClick>().emoDiff = int.Parse(items.Attributes["diff"].Value);
				csrResponse4.GetComponent<CSRResponseClick>().nextPrompt = int.Parse(items.Attributes["next"].Value);
			}
		}
			
	}

	public void changeEmo(int diff, int nextprompt)
	{
		emoLevel += diff;

		if (emoLevel > 100)
			emoLevel = 100;
		else if (emoLevel < 0)
			emoLevel = 0;
		Debug.Log("New emo: " + emoLevel);

		setFace();

		LoadScenario(nextprompt);
	}

	void setFace()
	{
		if (emoLevel > 70)
			this.gameObject.GetComponent<SpriteRenderer>().sprite = face_happy;
		else if (emoLevel < 30)
			this.gameObject.GetComponent<SpriteRenderer>().sprite = face_angry;
		else
			this.gameObject.GetComponent<SpriteRenderer>().sprite = face_neutral;
	}
}
