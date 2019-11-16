using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeDevice : MonoBehaviour {

	private int setting = 0;
	private Vector3 targetColor = new Vector3();
	public void Activate() {
		Operate();
	}

	public void Deactivate() {
		Operate();
	}

	public void Operate() {
		setting++;
		UpdateColor();
	}
	
	// Start is called before the first frame update
	void Start() {
		UpdateColor();
	}
	
	// Update is called once per frame
	void Update() {

		Vector3 originColor = new Vector3(
			GetComponent<Renderer>().material.color.r,
			GetComponent<Renderer>().material.color.g,
			GetComponent<Renderer>().material.color.b
		);

		Vector3 transitionColor = Vector3.Lerp(originColor, targetColor, Time.deltaTime * 2.5f);

		GetComponent<Renderer>().material.color = new Color(
			transitionColor.x, transitionColor.y, transitionColor.z
		);
	}

	private void UpdateColor() {
		targetColor = new Vector3(
			setting % 2 == 0 ? 1f : 0.2f,
			setting % 3 == 0 ? 1f : 0.2f,
			setting % 5 == 0 ? 1f : 0.2f
		);
	}
}