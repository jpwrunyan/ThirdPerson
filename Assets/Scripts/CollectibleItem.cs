﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

	[SerializeField]
	private string itemName;

	private void OnTriggerEnter(Collider other) {
		Debug.Log("item collected: " + itemName);
		Managers.Inventory.AddItem(itemName);
		Destroy(this.gameObject);
	}

	// Start is called before the first frame update
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}
}