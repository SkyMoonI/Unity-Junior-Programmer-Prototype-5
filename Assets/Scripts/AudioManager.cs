using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
public class AudioManager : MonoBehaviour
{
	[SerializeField] Slider volumeSlider;
	[SerializeField] AudioSource audioSource;
	float startVolume = 0.5f;
	// Start is called before the first frame update
	void Start()
	{
		volumeSlider.value = startVolume;
		audioSource.volume = volumeSlider.value;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void ChangeVolume()
	{
		audioSource.volume = volumeSlider.value;
	}

}
