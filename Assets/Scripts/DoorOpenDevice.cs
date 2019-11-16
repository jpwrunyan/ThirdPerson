using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : MonoBehaviour {

	[SerializeField]
	private AudioSource soundSource;

	[SerializeField]
	private AudioClip openSound;

	[SerializeField]
	private AudioClip closeSound;

	[SerializeField]
	private Vector3 dPos;

	[SerializeField]
	private bool manualOperation = true;

	private Vector3 origin;

	[SerializeField]
	private int duration = 50;
	private int count = 0;

	private bool _open;

	public void Activate() {
		if (!_open) {
			_open = true;
			if (soundSource != null) {
				soundSource.PlayOneShot(openSound);
			}
		}
	}

	public void Deactivate() {
		if (_open) {
			_open = false;
			if (soundSource != null) {
				soundSource.PlayOneShot(closeSound);
			}
		}
	}

	public void Operate() {
		if (manualOperation) {
			if (!_open) {
				Activate();
			} else {
				Deactivate();
			}
		}
	}

	// Start is called before the first frame update
	void Start() {
		origin = transform.position;
	}

	// Update is called once per frame
	void Update() {
		count += _open ? 1 : -1;
		if (_open && count > duration) {
				count = duration;
		} else if (count < 0) {
			count = 0;
		}

		float percent = count / (float) duration;
		transform.position = origin + (dPos * percent);
	}
}