using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance;

    private Slider sensitivitySliderX;
    private Slider sensitivitySliderY;
    private TMP_Text textSensX;
    private TMP_Text textSensY;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // If there is nothing saved, set the default sens
        if (!PlayerPrefs.HasKey("sensx"))
        {
            PlayerPrefs.SetInt("sensx", 150);
        }

        if (!PlayerPrefs.HasKey("sensy"))
        {
            PlayerPrefs.SetInt("sensy", 150);
        }
    }

    public void ChangeSensitivityX()
    {
        GameManager.Instance.SensitivityX = (int)sensitivitySliderX.value;
        int intvalue = (int)sensitivitySliderX.value;
        textSensX.text = intvalue.ToString();
        PlayerPrefs.SetInt("sensx", (int)sensitivitySliderX.value);
    }

    public void ChangeSensitivityY()
    {
        GameManager.Instance.SensitivityY = (int)sensitivitySliderY.value;
        int intvalue = (int)sensitivitySliderY.value;
        textSensY.text = intvalue.ToString();
        PlayerPrefs.SetInt("sensy", (int)sensitivitySliderY.value);
    }

    public void ResetSensitivity()
    {
        PlayerPrefs.SetInt("sensx", 150);
        PlayerPrefs.SetInt("sensy", 150);

        sensitivitySliderX.value = PlayerPrefs.GetInt("sensx");
        sensitivitySliderY.value = PlayerPrefs.GetInt("sensy");
        textSensX.text = PlayerPrefs.GetInt("sensx").ToString();
        textSensY.text = PlayerPrefs.GetInt("sensy").ToString();

        PlayerPrefs.Save();

        GameManager.Instance.SensitivityX = (int)sensitivitySliderX.value;
        GameManager.Instance.SensitivityY = (int)sensitivitySliderY.value;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {
        if (scene.name == "Settings")
        {
            sensitivitySliderX = GameObject.Find("SensitivityXSlider").GetComponent<Slider>();
            sensitivitySliderY = GameObject.Find("SensitivityYSlider").GetComponent<Slider>();
            textSensX = GameObject.Find("SensXNumber").GetComponent<TMP_Text>();
            textSensY = GameObject.Find("SensYNumber").GetComponent<TMP_Text>();

            GameObject.Find("ResetSensButton").GetComponent<Button>().onClick.AddListener(() => { ResetSensitivity(); });

            sensitivitySliderX.value = PlayerPrefs.GetInt("sensx");
            sensitivitySliderY.value = PlayerPrefs.GetInt("sensy");
            textSensX.text = PlayerPrefs.GetInt("sensx").ToString();
            textSensY.text = PlayerPrefs.GetInt("sensy").ToString();

            GameObject.Find("SensitivityXSlider").GetComponent<Slider>().onValueChanged.AddListener(delegate { ChangeSensitivityX(); });
            GameObject.Find("SensitivityYSlider").GetComponent<Slider>().onValueChanged.AddListener(delegate { ChangeSensitivityY(); });
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
