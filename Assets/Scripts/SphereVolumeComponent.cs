using System;
using UnityEngine.Rendering;

// Youtube Unity <Introduction to the Render Graph in Unity 6> 참조
// https://youtu.be/U8PygjYAF7A?si=8USTeVS6gpL7_88M

[Serializable]
[VolumeComponentMenu("Custom/SphereVolumeComponent")]
public class SphereVolumeComponent : VolumeComponent, IPostProcessComponent
{
    public ClampedFloatParameter intensity = new ClampedFloatParameter(value: 0, min: 0, max: 1, overrideState: true);

    public bool IsActive() => intensity.value > 0;
}