using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MikeGallery : GalleryRoom {
    [SerializeField]
    private Material mikeSkyboxMat;

    private Material origSkyboxMat;

    protected override void Awake() {
        base.Awake();
        origSkyboxMat = RenderSettings.skybox;
        RenderSettings.skybox = mikeSkyboxMat;
    }

    private void OnDestroy() {
        RenderSettings.skybox = origSkyboxMat;
    }
}
