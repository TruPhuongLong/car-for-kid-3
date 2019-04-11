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


	// Start is called before the first frame update
	void Start()
	{
		carYellow.carStop += CarYellowStop;
		carGray.carStop += CarGrayStop;
		carGreen.carStop += CarGreenStop;
		carBlue.carStop += CarBlueStop;
		carRed.carStop += CarRedStop;

		SceneStart();
	}

	

	void SceneStart() {
		var duration = timelineSceneStart.duration;
		StartCoroutine(carYellowStart((float)duration));
		timelineSceneStart.Play();
	}

	IEnumerator carYellowStart(float second)
	{
		yield return new WaitForSeconds(second);

		// run code
		carYellow.isStop = false;

		// run camera
		timeLineYellowCar.Play();
	}


	void CarYellowStop()
	{
		carGray.isStop = false;
		timeLineYellowCar.Stop();
		timeLineGrayCar.Play();
	}

	void CarGrayStop()
	{
		carGreen.isStop = false;
		timeLineGrayCar.Stop();
		timeLineGreenCar.Play();
	}

	void CarGreenStop()
	{
		carBlue.isStop = false;
		timeLineGreenCar.Stop();
		timeLineBlueCar.Play();
	}

	void CarBlueStop()
	{
		carRed.isStop = false;
		timeLineBlueCar.Stop();
		timeLineRedCar.Play();
	}

	void CarRedStop()
	{
		timeLineRedCar.Stop();
		timeLineSceneEnd.Play();
		Debug.Log("finish 5 car run");
	}
}
