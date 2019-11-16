using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System;
public class NetworkService {

	public IEnumerator GetWeatherJSON(Action<string> callback) {
		//const string api = "http://api.openweathermap.org/data/2.5/weather?q=Tokyo,jp&APPID=ab1fce6dccf98850bc83ce4df5df4a2e";
		const string api = "http://api.openweathermap.org/data/2.5/weather?q=Denver,us&APPID=ab1fce6dccf98850bc83ce4df5df4a2e";
		return CallAPI(api, callback);
	}
	
	public IEnumerator GetWeatherXML(Action<string> callback) {
		const string api = "http://api.openweathermap.org/data/2.5/weather?q=Chicago,us&mode=xml&APPID=ab1fce6dccf98850bc83ce4df5df4a2e";
		return CallAPI(api, callback);
	}

	public IEnumerator DownloadImage(Action<Texture2D> callback) {
		const string webImage = "http://upload.wikimedia.org/wikipedia/commons/c/c5/Moraine_Lake_17092005.jpg";
		UnityWebRequest request = UnityWebRequestTexture.GetTexture(webImage);
		yield return request.SendWebRequest();
		callback(DownloadHandlerTexture.GetContent(request));
	}

	private IEnumerator CallAPI(string url, Action<string> callback) {
		//Try with-resources-style setup.
		using (UnityWebRequest request = UnityWebRequest.Get(url)) {
			yield return request.SendWebRequest();

			if (request.isHttpError) {
				Debug.LogError("error: " + request.error);
			} else if (request.responseCode != (long) System.Net.HttpStatusCode.OK) {
				Debug.LogError("response error: " + request.responseCode);
			} else {
				callback(request.downloadHandler.text);
			}
		}
	}


}