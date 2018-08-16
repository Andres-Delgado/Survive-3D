using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Animations : MonoBehaviour {

	#region Variables

	private static Animations _instance;
	public static Animations Instance {
		get {
			if (!_instance) {
				Debug.Log("Error: Animations is null");
			}
			return _instance;
		}
	}

	[Header("Camera Shake")]
	[SerializeField] private float magnitude = 2.0f;
	[SerializeField] private float roughness = 7.5f;
	[SerializeField] private float fadeIn = 0.1f;
	[SerializeField] private float fadeOut = 1.0f;

	#endregion

	private void Awake() {
		_instance = this;
	}

	public void ShakeCamera() {
		CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
	}

	public void Push(Rigidbody other, Rigidbody origin, Vector3 contactPoint) {
		StartCoroutine(PushStart(other, origin, contactPoint));
	}

	IEnumerator PushStart(Rigidbody other, Rigidbody origin, Vector3 contactPoint) {
		Vector3 directionForce = contactPoint - origin.transform.position;
		directionForce = directionForce.normalized;
		directionForce.y = 0;

		other.velocity = directionForce * 25;
		yield return new WaitForSeconds(0.15f);
		if (other != null) {
			other.velocity = new Vector3(0, 0, 0);
		}
	}

}


