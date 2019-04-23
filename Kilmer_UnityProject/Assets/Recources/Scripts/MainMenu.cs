using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("HomeSign")]
    [SerializeField] private CanvasGroup creditsGroup = null;

    [Header("SettingsBoard")]
    [SerializeField] private Slider playerSlider = null;
    [SerializeField] private Slider timeSlider = null;
    [SerializeField] private Slider volumeSlider = null;
    [SerializeField] private TextMeshProUGUI playerText = null;
    [SerializeField] private TextMeshProUGUI timeText = null;
    [SerializeField] private TextMeshProUGUI volumeText = null;
    [SerializeField] private Transform clockGroteTransform = null;
    [SerializeField] private Transform clockKlijneTransform = null;
    [SerializeField] private Image volumeRender = null;
    [SerializeField] private float clockSpeed = 5000;
    [SerializeField] private List<Sprite> volumeSprites = new List<Sprite>();
    [SerializeField] private List<Image> playerSprites = new List<Image>();

    [Header("Extra")]
    [SerializeField] private Animator camaraAnimatior = null;
    [SerializeField] private int countdownTime = 6;
    [SerializeField] private GameObject[] hideInMenu;




    private void Start()
    {

        for (int i = 0; i < hideInMenu.Length; i++) { hideInMenu[i].SetActive(false); }

        playerSlider.value = PlayerPrefs.GetInt("playerCount");
        timeSlider.value = PlayerPrefs.GetInt("gameTime");
        volumeSlider.value = PlayerPrefs.GetInt("gameVolume");
        creditsGroup.alpha = 0;
        creditsGroup.gameObject.SetActive(false);
        AudioListener.volume = (volumeSlider.value / 100f);
    }

    private void Update()
    {
        PlayerSliderIcon();
        TimeSliderIcon();
        VolumeSliderIcon();
    }

    /// <summary>
    /// Updates The player slider icon
    /// </summary>
    void PlayerSliderIcon()
    {

        playerText.text = playerSlider.value.ToString();
        if (playerSlider.value == 2)
        {
            playerSprites[0].enabled = true;
            playerSprites[1].enabled = true;
            playerSprites[2].enabled = false;
            playerSprites[3].enabled = false;

        }
        else if (playerSlider.value == 3)
        {
            playerSprites[0].enabled = true;
            playerSprites[1].enabled = true;
            playerSprites[2].enabled = true;
            playerSprites[3].enabled = false;
        }
        else if (playerSlider.value == 4)
        {
            playerSprites[0].enabled = true;
            playerSprites[1].enabled = true;
            playerSprites[2].enabled = true;
            playerSprites[3].enabled = true;
        }

    }

    /// <summary>
    /// Updates The Time slider icon
    /// </summary>
    void TimeSliderIcon()
    {

        Quaternion grootRot = Quaternion.Euler(new Vector3(0, 0, (timeSlider.value / timeSlider.maxValue) * clockSpeed));
        Quaternion klijnRot = Quaternion.Euler(new Vector3(0, 0, (timeSlider.value / timeSlider.maxValue) * clockSpeed / 6));

        clockGroteTransform.localRotation = Quaternion.Slerp(clockGroteTransform.localRotation, grootRot, Time.deltaTime / 0.1f);
        clockKlijneTransform.localRotation = Quaternion.Slerp(clockKlijneTransform.localRotation, klijnRot, Time.deltaTime / 0.1f);

        int time = (int)((timeSlider.value * 30) + 5 * 60);

        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        string correctTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        timeText.text = correctTime;

    }

    /// <summary>
    /// Updates The Volume slider icon
    /// </summary>
    void VolumeSliderIcon()
    {

        volumeText.text = volumeSlider.value + "%";
        if (volumeSlider.value == 0)
            volumeRender.sprite = volumeSprites[0];
        else if (volumeSlider.value > 0 && volumeSlider.value < 33)
            volumeRender.sprite = volumeSprites[1];
        else if (volumeSlider.value > 33 && volumeSlider.value < 66)
            volumeRender.sprite = volumeSprites[2];
        else if (volumeSlider.value > 66)
            volumeRender.sprite = volumeSprites[3];

    }


    /// <summary>
    /// Sets up the cameras and starts the game via the game manager
    /// </summary>
    public void StartGame()
    {
        if (GameManager.instance.GetGameState().Equals(GameState.Menu))       
            StartCoroutine(IStartGame());      
    }
    private IEnumerator IStartGame()
    {
        camaraAnimatior.SetTrigger("StartGame");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(1);
        for (int i = 0; i < hideInMenu.Length; i++) { hideInMenu[i].SetActive(true);}
        GameUI.instance.StartGameUI(PlayerPrefs.GetInt("playerCount"), (PlayerPrefs.GetInt("gameTime") * 30) + (5 * 60),countdownTime);
        GameManager.instance.StartGame(PlayerPrefs.GetInt("playerCount"),countdownTime);
    }

    /// <summary>
    /// Moves the camera to the home screen
    /// </summary>
    public void SaveButton()
    {
        if (GameManager.instance.GetGameState().Equals(GameState.Menu))
            camaraAnimatior.SetTrigger("HomeMenu");

        PlayerPrefs.SetInt("playerCount", (int)playerSlider.value);
        PlayerPrefs.SetInt("gameTime", (int)timeSlider.value);
        PlayerPrefs.SetInt("gameVolume", (int)volumeSlider.value);

        AudioListener.volume = (volumeSlider.value / 100f);
    }

    /// <summary>
    /// Moves Camara to settings screen
    /// </summary>
    public void SettingsButton()
    {
        if(GameManager.instance.GetGameState().Equals(GameState.Menu))
            camaraAnimatior.SetTrigger("Settings");
    }


    /// <summary>
    /// Opens the credits screen
    /// </summary>
    public void CreditsButton(bool open)
    {
        if (open)
            StartCoroutine(IOpenCredits());
        else
            StartCoroutine(ICloseCredits());
    }

    /// <summary>
    /// Quits the aplication
    /// </summary>
    public void QuitButton()
    {
        Application.Quit();
    }


    public IEnumerator IOpenCredits()
    {
        creditsGroup.gameObject.SetActive(true);

        if (creditsGroup.alpha != 0)
            yield break;

        while (creditsGroup.alpha < 1)
        {
            creditsGroup.alpha += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator ICloseCredits()
    {
        if (creditsGroup.alpha != 1)
            yield break;

        while (creditsGroup.alpha > 0)
        {
            creditsGroup.alpha -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        creditsGroup.gameObject.SetActive(false);

    }
}
