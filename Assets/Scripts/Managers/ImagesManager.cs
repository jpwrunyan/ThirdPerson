using UnityEngine;
using System;
public class ImagesManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {
		get;
		private set;
	}

	private NetworkService networkService;

	private Texture2D _webImage;

	public void StartUp(NetworkService networkService) {
		Debug.Log("Images Manager Starting...");

		this.networkService = networkService;

		status = ManagerStatus.Started;
	}

	public void GetWebImage(Action<Texture2D> callback) {
		if (_webImage == null) {
			//StartCoroutine(networkService.DownloadImage(callback));
			StartCoroutine(networkService.DownloadImage((Texture2D image) => {
				_webImage = image;
				callback(_webImage);
			}));
		} else {
			callback(_webImage);
		}
	}
}