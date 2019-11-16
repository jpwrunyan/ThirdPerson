using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {

	[SerializeField]
	private GameObject[] targets;

	public bool requireKey;

	private void OnTriggerEnter(Collider other) {
		if (requireKey && Managers.Inventory.equippedItem != "key") {
			return;
		}
		foreach (GameObject target in targets) {
			target.SendMessage("Activate");
		}
	}

	private void OnTriggerExit(Collider other) {
		if (requireKey && Managers.Inventory.equippedItem != "key") {
			return;
		}
		foreach (GameObject target in targets) {
			target.SendMessage("Deactivate");
		}
	}

	// Start is called before the first frame update
	void Start() {
		GetComponent<MeshRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update() {
		
	}
}