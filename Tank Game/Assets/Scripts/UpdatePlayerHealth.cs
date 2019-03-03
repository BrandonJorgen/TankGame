using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerHealth : MonoBehaviour
{

	[Header("Text Objects")]
	public GameObject FullHealth;
	public GameObject ThreeFourths, HalfHealth, OneFourth, ZeroHealth;
	[Header("Required Data")]
	public FloatData PlayerHealth;

	private void Start()
	{
		FullHealth.SetActive(false);
		ThreeFourths.SetActive(false);
		HalfHealth.SetActive(false);
		OneFourth.SetActive(false);
		ZeroHealth.SetActive(false);
	}

	void Update ()
	{
		// Check for players current health
		// Players current health is 100%
		if (PlayerHealth.Value <= 1.0f && PlayerHealth.Value >= 0.76f)
		{
			FullHealth.SetActive(true);
			ThreeFourths.SetActive(false);
			HalfHealth.SetActive(false);
			OneFourth.SetActive(false);
			ZeroHealth.SetActive(false);
		}
		// Players health is below 76%, check to see where it falls
		if (PlayerHealth.Value <= 0.75f)
		{
			FullHealth.SetActive(false);
			HalfHealth.SetActive(false);
			OneFourth.SetActive(false);
			ZeroHealth.SetActive(false);
			ThreeFourths.SetActive(true);
			if (PlayerHealth.Value <= 0.5f) //Less than 51% health
			{
				FullHealth.SetActive(false);
				ThreeFourths.SetActive(false);
				OneFourth.SetActive(false);
				ZeroHealth.SetActive(false);
				HalfHealth.SetActive(true);
				if (PlayerHealth.Value <= 0.25f) //Less than 26% health
				{
					FullHealth.SetActive(false);
					ThreeFourths.SetActive(false);
					HalfHealth.SetActive(false);
					ZeroHealth.SetActive(false);
					OneFourth.SetActive(true);
					if (PlayerHealth.Value <= 0.0f) //Less than 1% health
					{
						FullHealth.SetActive(false);
						ThreeFourths.SetActive(false);
						HalfHealth.SetActive(false);
						OneFourth.SetActive(false);
						ZeroHealth.SetActive(true);
					}
				}
			}
		} 
	}
}