using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	Rigidbody myRigidbody;
	float minSpeed = 12;
	float maxSpeed = 16;
	float maxTorque = 10;
	float xRange = 4;
	float ySpawnPos = -2;

	GameManager gameManager;

	[SerializeField] int pointValue;

	[SerializeField] ParticleSystem explosionParticle;
	// Start is called before the first frame update
	void Start()
	{
		transform.position = RandomSpawnPosition();

		myRigidbody = GetComponent<Rigidbody>();

		myRigidbody.AddForce(RandomForce(), ForceMode.Impulse);
		myRigidbody.AddTorque(
		RandomTorque(),
		RandomTorque(),
		RandomTorque(),
		ForceMode.Impulse);

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnMouseDown()
	{
		if (gameManager.isGameActive && !gameManager.isGamePaused)
		{
			Destroy(gameObject);
			gameManager.UpdateScore(pointValue);
			Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
		}
	}
	public void DestroyTarget()
	{
		if (gameManager.isGameActive && !gameManager.isGamePaused)
		{
			Destroy(gameObject);
			Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
			gameManager.UpdateScore(pointValue);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		if (!gameObject.CompareTag("Bad"))
		{
			gameManager.UpdateLives(1);
		}
		if (gameManager.lives == 0)
		{
			gameManager.GameOver();
		}
	}
	Vector3 RandomSpawnPosition()
	{
		return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
	}

	float RandomTorque()
	{
		return Random.Range(-maxTorque, maxTorque);
	}

	Vector3 RandomForce()
	{
		return Vector3.up * Random.Range(minSpeed, maxSpeed);
	}

}
