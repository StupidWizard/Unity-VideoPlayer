using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Video360Player : MonoBehaviour {

	public enum VideoState {
		Idle = 0,
		Playing = 1,
		Pausing = 2,
		Finished
	}

	[SerializeField]
	VideoPlayer player;

	public VideoState videoState = VideoState.Idle;

	public bool resumeVideoWhenAppResume = false;

	// Use this for initialization
//	void Start () {
//	}

	public void SetUrl(string url) {
		player.url = url;
		player.Prepare();
	}

	public void SetClip(VideoClip clip) {
		player.clip = clip;
		player.Prepare();
	}

	// Update is called once per frame
	void Update () {
		switch (videoState) {
		case VideoState.Playing:
			if (player.time > 0 && ((ulong)player.frame == player.frameCount)) {
//				Debug.Log("Change to Idle");
				videoState = VideoState.Finished;
			}
			break;
		}
	}

	public double CurrentTime() {
		return player.time;
	}


	void OnApplicationPause(bool pauseStatus) {
//		Debug.Log("OnApplicationPause : pauseStatus = " + pauseStatus);
		if (videoState == VideoState.Playing) {
			player.Pause();
		}
		if (resumeVideoWhenAppResume && !pauseStatus) {
			StartCoroutine(ProcessResume());
		}
	}

	IEnumerator ProcessResume() {
		yield return new WaitForEndOfFrame();
		if (videoState == VideoState.Playing) {
			player.Play();
		}
	}

	public void PlayVideo() {
		player.Play();
		videoState = VideoState.Playing;
	}

	public void PauseVideo() {
		player.Pause();
		videoState = VideoState.Pausing;
	}

	public void StopVideo() {
		player.Stop();
		videoState = VideoState.Idle;
	}

	public bool IsPrepared() {
		return player.isPrepared;
	}

	public bool IsFinished() {
		return videoState == VideoState.Finished;
	}

	public Vector2 GetTextureSize() {
		var texture = player.texture;
		return new Vector2(texture.width, texture.height);
	}
}
