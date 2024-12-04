using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColourFilter : MonoBehaviour {
    private ColorGrading colorGrading = default;
    public float colourCode;

    void Start() {
        var postProcessVolume = FindObjectOfType<PostProcessVolume>();
        colorGrading = postProcessVolume.profile.GetSetting<ColorGrading>();
        colorGrading.hueShift.value = colourCode;
    }
}