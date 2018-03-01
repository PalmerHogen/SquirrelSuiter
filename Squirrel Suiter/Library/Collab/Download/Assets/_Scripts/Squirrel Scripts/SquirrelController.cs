using System.Collections;
using UnityEngine;


public class SquirrelController : MonoBehaviour {

	public float terrainWidth;

	public float horizontalSpeed = 10.0f;
	public float forwardSpeed = 90;
	public float tilt;
	private int tiltspeed;
	public int Acorncount = 0;
	public PlayerManager playerManager;
	public GameObject Scoretxt;
	public GameObject playerExplosion;
	public AudioSource explosionAudio;
	public AudioSource engineSound;
	public AudioSource windSound;
	public AudioClip crunch1, crunch2, crunch3, boost;
	public AudioSource cruncher;

	private float startTime;
	private bool direction;
	private bool rolling;
	private float coroutineStart;
	private float currentX;
	private float boostStart;
	private bool boosting;
	//private List<int> timesHit;
	private float lastscore;

	private Rigidbody rb;
	private ParticleSystem acornParticles;

	public int boostsAvailable = 0;

	public GameObject soundBarrier;
	private Vector3 soundBarStart, soundBarEnd;
	private Light engineLight;

	void Start() {
		//forwardSpeed = 50;
		//startTime = Time.time;
		lastscore = Scoretxt.GetComponent<ScoreText>().ScoreNum;
		rb = GetComponent<Rigidbody>();
		rolling = false;
		Vector3 InitLocation = new Vector3(100, 25, 0);
		//rb.transform.SetPositionAndRotation(InitLocation, Quaternion.Euler(70, 0, 0));
		acornParticles = GetComponent<ParticleSystem>();
		engineSound.Play();
		windSound.Play();
		tiltspeed = 0;
		Time.timeScale = 1;
		boosting = false;
		//startTime = Time.time;

		soundBarStart = new Vector3 (0, 0.025f, 0);
		soundBarEnd = new Vector3 (1000.0f, 0.025f, 1000.0f);
		soundBarrier.transform.localScale = soundBarStart;
		engineLight = soundBarrier.GetComponent<Light> ();
	}

	void FixedUpdate() {
		int score = Scoretxt.GetComponent<ScoreText>().ScoreNum;
		if (score > lastscore + 1000) {
			forwardSpeed += 5;
			lastscore = score;
		}

		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Vector3 movement = new Vector3(moveHorizontal, moveVertical, forwardSpeed*Time.deltaTime);
		//rb.velocity = horizontalSpeed * movement;
		//rb.AddForce(moveHorizontal*horizontalSpeed, moveVertical*horizontalSpeed, 0.0f);
		//transform.Translate(moveHorizontal, forwardspeed * Time.deltaTime, 0);
		//transform.Translate(moveHorizontal, speed * Time.deltaTime, 0);
		if (rb.transform.position.y <= 3 && moveVertical<0) {
			moveVertical = 0;
		}
		if (rb.transform.position.y >=60  && moveVertical > 0) {
			moveVertical = 0;
		}
		if (rb.transform.position.x <= 1 && moveHorizontal < 0) {
			moveHorizontal = 0;
		}
		if (rb.transform.position.x >= 500 && moveHorizontal > 0) {
			moveHorizontal = 0;
		}

		if (moveHorizontal < 0) {
			windSound.pitch = 1.0f + Mathf.Abs(moveHorizontal) / 2;
			if (Input.GetKeyDown (KeyCode.Space)) {
				direction = false;
				rolling = true;
				coroutineStart = Time.time;
				currentX = transform.position.x;

				StartCoroutine("barrelRoll");
			}
			if (!rolling) {
				rb.rotation = Quaternion.Euler(70 - (moveHorizontal * -tilt * 0.25f), (moveHorizontal * -tilt * 0.80f), (moveHorizontal * -tilt * 0.83f));
			}

		} else if (moveHorizontal > 0) {
			windSound.pitch = 1.0f + Mathf.Abs (moveHorizontal)/2;
			if (Input.GetKeyDown (KeyCode.Space)) {
				direction = true;
				rolling = true;
				coroutineStart = Time.time;
				currentX = transform.position.x;

				StartCoroutine("barrelRoll");
			}
			if (!rolling) {
				rb.rotation = Quaternion.Euler(70 + (moveHorizontal * -tilt * 0.25f), (moveHorizontal * -tilt * 0.80f), (moveHorizontal * -tilt * 0.83f));
			}
		} else {
			rb.rotation = Quaternion.Euler(70, 0, 0);
		}

		if (!rolling && boostsAvailable > 0 && !boosting) {
			if (Input.GetKeyDown (KeyCode.B)) {
				boostStart = Time.time;
				boosting = true;

				boostsAvailable -= 1;
				cruncher.volume = 2.0f;
				cruncher.dopplerLevel = 0.0f;
				cruncher.pitch = Random.Range (0.8f, 1.1f);
				cruncher.PlayOneShot (boost);
				Scoretxt.GetComponent<ScoreText>().DecBoosts();
				StartCoroutine("SpeedBoost");
			}
		}

		Vector3 movement = new Vector3(moveHorizontal, moveVertical, forwardSpeed * Time.deltaTime);
		rb.velocity = horizontalSpeed * movement;

		if (Mathf.Abs (transform.position.x - 250) > terrainWidth) {
			Collision other = new Collision ();
			OnCollisionEnter (other);
		}

	}

	void OnTriggerEnter(Collider acorn) {
		if (acorn.gameObject.CompareTag("acorn")) {
			acorn.gameObject.SetActive(false);
			Scoretxt.GetComponent<ScoreText>().IncAcorns();

			acornParticles.Play(true);
			acornParticles.Play(false);

			//cruncher.volume = 2.0f;
			cruncher.dopplerLevel = 0.0f;
			cruncher.pitch = Random.Range (0.8f, 1.1f);

			int randomSound = Random.Range (0, 2);
			if (randomSound == 0) {
				//cruncher.clip = crunch1;
				cruncher.PlayOneShot (crunch1);

			} else if (randomSound == 1) {
				//cruncher.clip = crunch2;
				cruncher.PlayOneShot (crunch2);
			} else {
				//cruncher.clip = crunch3;
				cruncher.PlayOneShot (crunch3);
			}
		}
		if (acorn.gameObject.CompareTag("Boost")){
			acorn.gameObject.SetActive(false);

			//acornParticles.Play(true);
			//acornParticles.Play(false);

			boostsAvailable += 1;
			Scoretxt.GetComponent<ScoreText>().IncBoosts();

			//			boostStart = Time.time;
			//			boosting = true;
			//
			//			cruncher.volume = 2.0f;
			//			cruncher.dopplerLevel = 0.0f;
			//			cruncher.pitch = Random.Range (0.8f, 1.1f);
			//			cruncher.PlayOneShot (boost);
			//			StartCoroutine("SpeedBoost");
		}
	}

	void OnCollisionEnter(Collision other) {
		Instantiate(playerExplosion, transform.position, transform.rotation);
		Scoretxt.GetComponent<ScoreText>().playerDead = true;
		engineSound.Stop();
		windSound.Stop();
		explosionAudio.Play();
		playerManager.Die();
		gameObject.SetActive(false);
	}

	IEnumerator SpeedBoost() {
		while (boosting) {
			if (Time.time - boostStart < 1.0f) {
				forwardSpeed += 15.0f * Mathf.Cos (Mathf.PI * (Time.time - boostStart));
				Vector3 ScaleFactor = Vector3.Lerp (soundBarStart, soundBarEnd, Time.time - boostStart);
				soundBarrier.transform.localScale = ScaleFactor;
				engineLight.intensity += Mathf.Cos (Mathf.PI * (Time.time - boostStart));
			}
			else{
				boosting = false;
				soundBarrier.transform.localScale = soundBarStart;
				engineLight.intensity = 0.0f;
			}
			yield return new WaitForFixedUpdate ();
		}
	}

	IEnumerator barrelRoll() {
		while (rolling) {
			if (Time.time - coroutineStart < 0.5f) {
				float corkscrew = Mathf.Cos ((Time.time - coroutineStart) * (1 * Mathf.PI));
				if (direction) {
					transform.position = new Vector3 (currentX + 80*(Mathf.Sin ((Time.time - coroutineStart) * 1*Mathf.PI)), transform.position.y, transform.position.z);
					transform.RotateAround (transform.position, Vector3.forward, -130.0f*corkscrew/(Mathf.PI));


				} else {
					transform.position = new Vector3 (currentX - 80*(Mathf.Sin ((Time.time - coroutineStart) * 1*Mathf.PI)), transform.position.y, transform.position.z);
					transform.RotateAround (transform.position, Vector3.forward, 130.0f*corkscrew/(Mathf.PI));
				}
			} else {
				rolling = false;
			}
			yield return new WaitForFixedUpdate();
		}
	}


}