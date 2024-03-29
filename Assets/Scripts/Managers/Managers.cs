﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(WeatherManager))]
[RequireComponent(typeof(ImagesManager))]
public class Managers : MonoBehaviour {

	public static PlayerManager Player {
		get;
		private set;
	}

	public static InventoryManager Inventory {
		get;
		private set;
	}
	
	public static WeatherManager Weather {
		get;
		private set;
	}

	public static ImagesManager Images {
		get;
		private set;
	}

	public static AudioManager Audio {
		get;
		private set;
	}

	private List<IGameManager> _startSequence;

	private void Awake() {
		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();
		Weather = GetComponent<WeatherManager>();
		Images = GetComponent<ImagesManager>();
		Audio = GetComponent<AudioManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);
		_startSequence.Add(Weather);
		_startSequence.Add(Images);
		_startSequence.Add(Audio);

		StartCoroutine(StartupManagers());
	}

	private IEnumerator StartupManagers() {
		NetworkService networkService = new NetworkService();

		foreach (IGameManager manager in _startSequence) {
			manager.StartUp(networkService);
		}

		yield return null;

		int numModules = _startSequence.Count;
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;
			foreach(IGameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}
			if (numReady > lastReady) {
				Debug.Log("Progress: " + numReady + "/" + numModules);
			}
			yield return null;
		}
		Debug.Log("All managers started up");
	}
}