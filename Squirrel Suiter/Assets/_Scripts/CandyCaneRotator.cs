﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneRotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * 10);
        //transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime * 10);
    }
}
