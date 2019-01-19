using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerHealth : MonoBehaviour
{

	private Image image;
	public FloatData Data;
	
	// Use this for initialization
	void Start ()
	{
		image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		image.fillAmount = Data.Value;
	}
}
