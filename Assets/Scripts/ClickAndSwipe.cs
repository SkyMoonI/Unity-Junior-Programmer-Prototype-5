using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
	GameManager gameManager;
	Camera cam;
	Vector3 mousePos;
	TrailRenderer trail;
	BoxCollider boxCollider;
	bool swiping = false;
	// Start is called before the first frame update
	void Awake()
	{
		cam = Camera.main;
		trail = GetComponent<TrailRenderer>();
		boxCollider = GetComponent<BoxCollider>();
		trail.enabled = false;
		boxCollider.enabled = false;

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (gameManager.isGameActive && !gameManager.isGamePaused)
		{
			if (Input.GetMouseButtonDown(0))
			{
				swiping = true;
				UpdateComponents();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				swiping = false;
				UpdateComponents();
			}
			if (swiping)
			{
				UpdateMousePosition();
			}
		}
	}

	void UpdateMousePosition()
	{
		mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
		transform.position = mousePos;
	}

	void UpdateComponents()
	{
		trail.enabled = swiping;
		boxCollider.enabled = swiping;
	}
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.GetComponent<Target>())
		{
			other.gameObject.GetComponent<Target>().DestroyTarget();
		}
	}


}
