using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KincaidGallery : GalleryRoom {
    private Transform cubeTransform;
    private Transform[] cubes;

    public float rotateRate = 10f, cubeRotateRate = 10f;
    
    protected override void Awake() {
        base.Awake();
        cubeTransform = transform.Find("Cubes").transform;
        cubes = cubeTransform.GetComponentsInChildren<Transform>();
    }
    
	private void Update () {
        cubeTransform.Rotate(0, rotateRate * Time.deltaTime, 0);
        for (var i = 0; i < cubes.Length; i++) {
            cubes[i].Rotate(0, cubeRotateRate * Time.deltaTime, 0);
        }
	}
}
