using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour {

    public float speed = .5f;
    public bool moving = true;
    public int L2R = 1;
    //public Renderer colorMat;
    private float startTime;


    void Start () {
        startTime = Time.time;
        speed = Random.Range(.25f, .6f);
        //if (colorMat != null) {
        //    colorMat.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //} else {
        //    GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //}
        GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);


    }

    void Update () {
        if (!moving) {
            return;
        }
        float timeFromStart = Time.time - startTime;
        Vector3 from = new Vector3(-140, 12f, transform.position.z);
        Vector3 to = new Vector3(340, 12f, transform.position.z);
        float t = L2R * Mathf.Sin(timeFromStart * speed * Mathf.PI);
        if (Mathf.Abs(t) >= .99f) {
            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            transform.Rotate(0, 0, 180);// = Quaternion.Euler(-89.9f, 180, transform.rotation.z);
        }
        transform.position = Vector3.Lerp(from, to, t);
    }
}
