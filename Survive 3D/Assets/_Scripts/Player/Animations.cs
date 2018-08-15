using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Animations : MonoBehaviour {

	#region Variables

	[Header("Camera Shake")]
	[SerializeField] private float magnitude = 2.0f;
	[SerializeField] private float roughness = 7.5f;
	[SerializeField] private float fadeIn = 0.1f;
	[SerializeField] private float fadeOut = 1.0f;



	#endregion


	public void ShakeCamera() {
		CameraShaker.Instance.ShakeOnce(magnitude, roughness, fadeIn, fadeOut);
	}

}


