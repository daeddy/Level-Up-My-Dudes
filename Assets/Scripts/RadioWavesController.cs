using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioWavesController : MonoBehaviour
{
    public float duration = 1.0f;

    private Animator animator;
    private float timer = 0.0f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= duration)
            Destroy(gameObject);
	}
}
