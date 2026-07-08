using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class EffectController : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;
    [SerializeField] private GamePause gamePause;
    private ColorCurves curva;
    private LensDistortion lensa;

    private void Start()
    {
        globalVolume.profile.TryGet<ColorCurves>(out curva);
        globalVolume.profile.TryGet<LensDistortion>(out lensa);
    }

    private void Update()
    {
        if (globalVolume != null && curva != null && lensa != null)
        {
            curva.active = gamePause.isPaused;
            lensa.active = gamePause.isPaused;
        }
    }
}
