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

	DragonController dragon;

	public float maxResponseTime = 5.0f;
	public Text responseTimerText;
	float timer;

	XmlDocument dialogScript;
	XmlNodeList scenarioList;
	int emoLevel;
	int currentScenario;
	int lastScenario;
	public int GameOverScenario = 4;
	public int DelayScenario = 3;


	// Use this for initialization
	void Start () {
		currentScenario = 0;
		lastScenario = 0;
		DelayScenario = GameOverScenario - 1;
		this.gameObject.GetComponent<SpriteRenderer>().sprite = face_neutral;
		dragon = GameObject.Find("Dragon").GetComponent<DragonController>();

		timer = maxResponseTime;

		InitScript();
		LoadScenario(currentScenario);
	}


	// Update is called once per frame
	void Update () {

		if (currentScenario != GameOverScenario)
		{
			timer -= Time.deltaTime;
			int seconds = Mathf.FloorToInt(timer);
			string niceTime = string.Format("0:{0:00}", seconds);
			responseTimerText.text = niceTime.ToString();
			if (timer <= 0.0f)
			{
				timer = maxResponseTime;
				if (currentScenario != DelayScenario)
				{
					lastScenario = currentScenario;
					LoadScenario(DelayScenario);
				}
				else
				{
					currentScenario = GameOverScenario;
					dragon.stopDragonTimer();
					LoadScenario(GameOverScenario);
				}
			}
		}
		else
		{
			dragon.stopDragonTimer();
		}
	
	}

	void InitScript()
	{
		dialogScript = new XmlDocument();
		dialogScript.LoadXml(scriptFile.text);
		scenarioList = dialogScript.GetElementsByTagName("scenario");
	}

	void LoadScenario(int currSen)
	{
		currentScenario = currSen;
		XmlNode node1 = scenarioList[currSen];

		if (currSen == 0)
		{
			dragon.resetDragon();
			dragon.stopDragonTimer();
		}
		else
			dragon.startDragonTimer();

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

		if (currSen == DelayScenario)
			csrResponse1.GetComponent<CSRResponseClick>().nextPrompt = lastScenario;

		Debug.Log("Current Scenario = " + currentScenario);
			
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

		timer = maxResponseTime;

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
