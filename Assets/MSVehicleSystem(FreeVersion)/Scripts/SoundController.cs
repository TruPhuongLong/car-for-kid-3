using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	//public GameObject projectile;
	public AudioClip yeahSound;
	public AudioClip hornCarSound;


	//private float throwSpeed = 2000f;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRange = 1.0f;


	void Awake()
	{

		source = GetComponent<AudioSource>();

	}

	public void yeah() {
		source.PlayOneShot(yeahSound, volHighRange);
	}

	public void horn() {
		source.PlayOneShot(hornCarSound, 0.35f);
	}

}
