using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BaseVideoPlayExample : MonoBehaviour {

	[Header("Video player setup")]

	[SerializeField]
	Video360Player player;

	[Tooltip("Set video url to play. on Editor, url can be path of video file on PC. on MobileApp, url can be path of video on mobile.")]
	[SerializeField]
	string url = "set your video url here";

	[Tooltip("Set video clip (on project) to here")]
	[SerializeField]
	VideoClip videoClip;


	[Header("View of video setup")]
	[SerializeField]
	RectTransform screenbound;

	[SerializeField]
	Transform viewQuad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.U)) {
			player.SetUrl(url);
			StartCoroutine(WaitToSetupView());
		}

		if (Input.GetKeyDown(KeyCode.V)) {
			player.SetClip(videoClip);
			StartCoroutine(WaitToSetupView());
		}

		if (Input.GetKeyDown(KeyCode.P)) {
			player.PlayVideo();
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			player.PauseVideo();
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			player.StopVideo();
		}

		if (Input.GetKeyDown(KeyCode.I)) {
			var size = player.GetTextureSize();
			Debug.LogError("Size = " + size);
		}
	}

	IEnumerator WaitToSetupView () {

		Debug.LogError("Wait to setup view");

		yield return null;

		while (!player.IsPrepared()) {
			yield return null;
		}

		SetupView();
	}

	void SetupView() {
		var size = player.GetTextureSize();
		Debug.LogError("Size = " + size);
		var boundSize = screenbound.rect.size;

		float videoRatio = size.x / size.y;
		float boundRatio = boundSize.x / boundSize.y;

		float width = boundSize.x;
		float height = boundSize.y;

		if (videoRatio > boundRatio) {
			// fit view width to bound width
			height = size.y * boundSize.x / size.x;
		} else {
			// fit view height to bound height
			width = size.x * boundSize.y / size.y;
		}

		viewQuad.localScale = new Vector3(width, height, 1);
	}
}
