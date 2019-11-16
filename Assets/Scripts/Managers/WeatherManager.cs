using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;

public class WeatherManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {
		get;
		private set;
	}

	public float cloudValue {
		get;
		private set;
	}

	private NetworkService networkService;

	public void StartUp(NetworkService networkService) {
		Debug.Log("Weather Manager Starting...");

		this.networkService = networkService;
		StartCoroutine(networkService.GetWeatherJSON(OnJSONDataLoad));
		//StartCoroutine(networkService.GetWeatherXML(OnXMLDataLoad));
		status = ManagerStatus.Initializing;
	}

	public void OnJSONDataLoad(string data) {
		Dictionary<string, object> dict;
		dict = MiniJSON.Json.Deserialize(data) as Dictionary<string, object>;

		Dictionary<string, object> clouds = (Dictionary<string, object>) dict["clouds"];
		cloudValue = (long)clouds["all"] / 100f;
		Debug.Log("Cloud value from JSON: " + cloudValue);

		Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

		status = ManagerStatus.Started;
	}

	public void OnXMLDataLoad(string data) {
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(data);
		XmlNode root = doc.DocumentElement;

		XmlNode node = root.SelectSingleNode("clouds");
		string value = node.Attributes["value"].Value;
		cloudValue = Convert.ToInt32(value) / 100f;
		Debug.Log("Cloud value from XML: " + cloudValue);

		Messenger.Broadcast(GameEvent.WEATHER_UPDATED);

		status = ManagerStatus.Started;
	}

	private void GetData() {

	}

	private void OnResponse() {

	}
}