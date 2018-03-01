using System.Collections;
using UnityEngine;


public class SquirrelController : MonoBehaviour {

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

    private float startTime;
	private bool direction;
	private bool rolling;
	private float coroutineStart;
	private float currentX;
	private float currentY;
	private float currentZ;
    //private List<int> timesHit;
    private float lastscore;

    private Rigidbody rb;
    private ParticleSystem acornParticles;


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
        //startTime = Time.time;

    }

    void FixedUpdate() {
        /*
        float now = Time.time;
        int elapsed = Mathf.RoundToInt(startTime - now);
        if (elapsed % 10 == 0)
        {
            timesHit.Add(elapsed);
            forwardSpeed = forwardSpeed + 100;
        }
        */
        int score = Scoretxt.GetComponent<ScoreText>().ScoreNum;
        if (score > lastscore + 300) {
            forwardSpeed += 1;
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
				if (transform.position.z < 2900.0f) {
					direction = false;
					rolling = true;
					coroutineStart = Time.time;
					currentX = transform.position.x;
					currentY = transform.position.y;
					currentZ = transform.position.z;
					StartCoroutine ("barrelRoll");
				}
			}
			if (!rolling) {
				rb.rotation = Quaternion.Euler(70 - (moveHorizontal * -tilt * 0.25f), (moveHorizontal * -tilt * 0.80f), (moveHorizontal * -tilt * 0.83f));
			}

        } else if (moveHorizontal > 0) {
			windSound.pitch = 1.0f + Mathf.Abs (moveHorizontal)/2;
			if (Input.GetKeyDown (KeyCode.Space)) {
				if (transform.position.z < 2900.0f) {
					direction = true;
					rolling = true;
					coroutineStart = Time.time;
					currentX = transform.position.x;
					currentY = transform.position.y;
					currentZ = transform.position.z;
					StartCoroutine ("barrelRoll");
				}
			}
			if (!rolling) {
				rb.rotation = Quaternion.Euler(70 + (moveHorizontal * -tilt * 0.25f), (moveHorizontal * -tilt * 0.80f), (moveHorizontal * -tilt * 0.83f));
			}
        } else {
            rb.rotation = Quaternion.Euler(70, 0, 0);
        }

        //rb.rotation = Quaternion.Euler(70 - (moveHorizontal * -tilt * 0.25f), (moveHorizontal * -tilt * 0.80f), (moveHorizontal * -tilt * 0.83f));
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, forwardSpeed * Time.deltaTime);
        rb.velocity = horizontalSpeed * movement;
        //rb.rotation = Quaternion.Euler(0, 0, (rb.velocity.x * -tilt));
        
    }

    void OnTriggerEnter(Collider acorn) {
        if (acorn.gameObject.CompareTag("acorn")) {
            acorn.gameObject.SetActive(false);
            Scoretxt.GetComponent<ScoreText>().IncAcorns();

            acornParticles.Play(true);
            acornParticles.Play(false);
        }
    }

    void OnCollisionEnter(Collision other) {
        Instantiate(playerExplosion, transform.position, transform.rotation);
		engineSound.Stop();
		windSound.Stop();
		explosionAudio.Play();
        playerManager.Die();
        gameObject.SetActive(false);
    }

	IEnumerator barrelRoll(){
		while (rolling) {
			if (Time.time - coroutineStart < 0.5f) {
				float corkscrew = Mathf.Cos ((Time.time - coroutineStart) * (1 * Mathf.PI));
				if (direction) {
					transform.position = new Vector3 (currentX + 80*(Mathf.Sin ((Time.time - coroutineStart) * 1*Mathf.PI)), currentY, currentZ + forwardSpeed * 2 * (Time.time-coroutineStart));
					transform.RotateAround (transform.position, Vector3.forward, -180*corkscrew/(Mathf.PI));

				} else {
					transform.position = new Vector3 (currentX - 80*(Mathf.Sin ((Time.time - coroutineStart) * 1*Mathf.PI)), currentY, currentZ + forwardSpeed * 2 * (Time.time-coroutineStart));
					transform.RotateAround (transform.position, Vector3.forward, 180*corkscrew/(Mathf.PI));
				}
			} else {
				rolling = false;
			}
			yield return new WaitForSeconds (0.01f);
		}
	}
}