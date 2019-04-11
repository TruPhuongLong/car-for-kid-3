using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SceneController : MonoBehaviour
{
	public CarEngine carYellow;
	public CarEngine carGray;
	public CarEngine carGreen;
	public CarEngine carBlue;
	public CarEngine carRed;

	public PlayableDirector timelineSceneStart;
	public PlayableDirector timeLineYellowCar;
	public PlayableDirector timeLineGrayCar;
	public PlayableDirector timeLineGreenCar;
	public PlayableDirector timeLineBlueCar;
	public PlayableDirector timeLineRedCar;
	public PlayableDirector timeLineSceneEnd;

	public SoundController soundController;


	

	// Start is called before the first frame update
	void Start()
	{
		RegisterCarStop();
		RegisterCarHorn();

		SceneStart();
	}

	void RegisterCarStop() {
		carYellow.carStop += CarYellowStop;
		carGray.carStop += CarGrayStop;
		carGreen.carStop += CarGreenStop;
		carBlue.carStop += CarBlueStop;
		carRed.carStop += CarRedStop;
	}

	void RegisterCarHorn()
	{
		carYellow.carHorn += CarHorn;
		carGray.carHorn += CarHorn;
		carGreen.carHorn += CarHorn;
		carBlue.carHorn += CarHorn;
		carRed.carHorn += CarHorn;
	}

		IEnumerator _WaitForSeconds(float second, Action action)
	{
		yield return new WaitForSeconds(second);
		action();
	}



	void SceneStart() {
		var duration = timelineSceneStart.duration;
		timelineSceneStart.Play();
		//StartCoroutine(carYellowStart((float)duration));

		StartCoroutine(_WaitForSeconds((float)duration, () => {
			// run code
			carYellow.isStop = false;

			// run camera
			timeLineYellowCar.Play();
		}));
		
	}



	void CarYellowStop()
	{
		// yeah
		soundController.yeah();

		StartCoroutine(_WaitForSeconds(1.5f, () => {
			carGray.isStop = false;
			timeLineYellowCar.Stop();
			timeLineGrayCar.Play();
		}));
	}



	void CarGrayStop()
	{
		// yeah
		soundController.yeah();

		StartCoroutine(_WaitForSeconds(1.5f, () => {
			carGreen.isStop = false;
			timeLineGrayCar.Stop();
			timeLineGreenCar.Play();
		}));
	}

	void CarGreenStop()
	{
		// yeah
		soundController.yeah();

		StartCoroutine(_WaitForSeconds(1.5f, () => {
			carBlue.isStop = false;
			timeLineGreenCar.Stop();
			timeLineBlueCar.Play();
		}));
	}

	void CarBlueStop()
	{
		// yeah
		soundController.yeah();

		StartCoroutine(_WaitForSeconds(1.5f, () => {
			carRed.isStop = false;
			timeLineBlueCar.Stop();
			timeLineRedCar.Play();
		}));
	}

	void CarRedStop()
	{
		// yeah
		soundController.yeah();

		StartCoroutine(_WaitForSeconds(1.5f, () => {
			timeLineRedCar.Stop();
			timeLineSceneEnd.Play();
		}));
	}


	void CarHorn() {
		soundController.horn();
	}
}
