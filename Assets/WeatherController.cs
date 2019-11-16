using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour {

	[SerializeField]
	private Material sky;
	[SerializeField]
	private Light sun;

	private float _fullIntensity;

	private float _cloudValue = 0f;

	void Awake() {
		Messenger.AddListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
	}

	void OnDestroy() {
		Messenger.RemoveListener(GameEvent.WEATHER_UPDATED, OnWeatherUpdated);
	}

	// Start is called before the first frame update
	void Start() {
		_fullIntensity = sun.intensity;
	}
	
	// Update is called once per frame
	void Update() {
		//SetOvercast(_cloudValue);
		//_cloudValue += 0.005f;
	}

	private void OnWeatherUpdated() {
		SetOvercast(Managers.Weather.cloudValue);
	}

	private void SetOvercast(float value) {
		Debug.Log("SetOvercast: " + value);
		sky.SetFloat("_Blend", value);
		sun.intensity = _fullIntensity - (_fullIntensity * value);
	}
}