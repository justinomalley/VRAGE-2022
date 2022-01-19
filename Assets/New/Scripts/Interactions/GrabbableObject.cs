using System;
using UnityEngine;

public class GrabbableObject : InteractableObject {

    [SerializeField]
    private Color highlightColor;

    private Color origColor;

    private Material mat;

    protected override void Touch() {
        mat.color = highlightColor;
    }
    
    protected override void Untouch() {
        mat.color = origColor;
    }
    
    public virtual void Grabbed() { }

    public virtual void Dropped() { }

    private void Awake() {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit() {
        mat.color = origColor;
    }
}
