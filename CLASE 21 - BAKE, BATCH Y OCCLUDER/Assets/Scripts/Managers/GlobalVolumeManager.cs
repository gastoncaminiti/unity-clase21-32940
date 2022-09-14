using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlobalVolumeManager : MonoBehaviour
{
    // Start is called before the first frame update
    private PostProcessVolume globalVolume;

    [SerializeField]
    [Range(0.1f, 1f)]
    float resetFilterTime = 0.25f;

    ColorGrading colorFX;

    private void OnEnable()
    {
        PlayerEvents.OnDamage += OnDamageFilter;
        PlayerEvents.OnHeal += OnHealFilter;
    }

    void Start()
    {
        globalVolume = GetComponent<PostProcessVolume>();
        globalVolume.profile.TryGetSettings(out colorFX);
    }

    public void OnDamageFilter(float value)
    {
        colorFX.colorFilter.value = Color.red;
        colorFX.active = true;
        Invoke("ResetColorFilter", 1f);
    }

    public void OnHealFilter(int value)
    {
        colorFX.colorFilter.value = Color.green;
        colorFX.active = true;
        Invoke("ResetColorFilter", resetFilterTime);
    }

    private void ResetColorFilter()
    {
        colorFX.active = false;
    }

    private void OnDisable()
    {
        PlayerEvents.OnDamage -= OnDamageFilter;
        PlayerEvents.OnHeal -= OnHealFilter;
    }
}
