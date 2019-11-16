using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopup : MonoBehaviour {

	[SerializeField]
	private AudioClip sound;

	private int _currentSong = 0;

	public void OnSoundToggle() {
		Managers.Audio.soundMute = !Managers.Audio.soundMute;
		Managers.Audio.PlaySound(sound);
	}

	public void OnSoundValue(float value) {
		Managers.Audio.soundVolume = value;
	}

	public void OnMusicToggle() {
		//Button click
		Managers.Audio.PlaySound(sound);
		Managers.Audio.musicMute = !Managers.Audio.musicMute;
	}

	public void OnMusicValue(float value) {
		Managers.Audio.musicVolume = value;
	}

	public void OnSelectMusic(int value) {
		//Button click
		Managers.Audio.PlaySound(sound);

		if (_currentSong != value) {
			_currentSong = value;
			switch (value) {
				case 1:
					Managers.Audio.PlayIntroMusic();
					break;
				case 2:
					Managers.Audio.PlayLevelMusic();
					break;
				default:
					Managers.Audio.StopMusic();
					break;
			}
		}
	}

}