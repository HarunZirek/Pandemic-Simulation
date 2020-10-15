using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;

    [SerializeField, Range(0, 24)] private float TimeOfDay;

    [SerializeField] TMPro.TMP_Text timeOfDayText;

    [SerializeField] Material emissionWindow;
    private bool emissionDisabled = false;
    public bool isNight
    {
        get;
        private set;
    } = true;

    public float speed = 40f;
    private float gameSpeed = 0.4f;

    private bool dayEnd = false;

    public bool workTime()
    {
        return ((TimeOfDay > 7 ? true : false) && (TimeOfDay < 17 ? true : false));
    }

    public bool parkTime()
    {
        return ((TimeOfDay > 11 ? true : false) && (TimeOfDay < 19 ? true : false));
    }

    public void ResetTime()
    {
        TimeOfDay = 0f;
        dayEnd = false;
    }

    private void Start()
    {
        emissionWindow.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        if (Preset == null)
            return;

        if (Application.isPlaying)
        {
            TimeOfDay += Time.deltaTime * gameSpeed;
            TimeOfDay %= 25; 
            
            if (Mathf.Round(TimeOfDay) == 24 && !dayEnd)
            {
                Time.timeScale = 0;
                GameObject.FindObjectOfType<DailyTable>().DayEnd();
                dayEnd = true;
                return;
            }

            UpdateLighting(TimeOfDay / 24f);

            if (TimeOfDay > 6 && TimeOfDay < 17 && isNight)
            {
                isNight = false;
            }
            else if (!isNight && !(TimeOfDay > 6 && TimeOfDay < 17))
            {
                isNight = true;
            }
            if (!isNight && !emissionDisabled)
            {
                emissionWindow.DisableKeyword("_EMISSION");
                emissionDisabled = true;
            }
            else if (isNight && emissionDisabled)
            {

                emissionWindow.EnableKeyword("_EMISSION");
                emissionDisabled = false;
            }
            timeOfDayText.text = ((int)TimeOfDay).ToString();
        }
        else
        {
            UpdateLighting(TimeOfDay / 24f);
        }
    }


    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        RenderSettings.skybox.SetFloat("_Exposure", Mathf.Sin(timePercent * 3.25f));
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }
}