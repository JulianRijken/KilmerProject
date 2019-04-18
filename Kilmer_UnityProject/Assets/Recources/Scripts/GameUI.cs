using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Animator camaraAnimatior = null;
    [SerializeField] GameManager gameManager = null;
    [SerializeField] GameObject inGameMenu = null;
    [SerializeField] CanvasGroup inGameUIGroup = null;
    [SerializeField] private TextMeshProUGUI[] playersScoreText = null;
    [SerializeField] private TextMeshProUGUI gameTimeLeftText = null;
    [SerializeField] private TextMeshProUGUI countdownText = null;

    [SerializeField] private TextMeshProUGUI winScreenText = null;
    [SerializeField] CanvasGroup winScreenCanvasGroup = null;
    [SerializeField] GameObject winScreenGroup = null;
    [SerializeField] private float cheatTimeMakeZero;

    [SerializeField] private Color overtimeColor = Color.red;

    private int[] playersScores = new int[4];
    private float timeScaleTimer;

    private int playerCount;
    private float gameTimeLeft;
    private float gameStartTime;

    [SerializeField] private Transform lightTransform = null;
    [SerializeField] private Vector3 toLightRot = new Vector3();
    private Vector3 fromLightRot;


    private void Start()
    {
        fromLightRot = lightTransform.eulerAngles;

        countdownText.gameObject.SetActive(false);
        inGameMenu.SetActive(false);
        inGameUIGroup.alpha = 0;
        inGameUIGroup.gameObject.SetActive(false);
        for (int i = 0; i < playersScoreText.Length; i++)
        {
            playersScoreText[i].gameObject.SetActive(false);
        }

        winScreenCanvasGroup.alpha = 0;
        winScreenGroup.SetActive(false);

        cheatTimeMakeZero = 0;
    }

    public void Update()
    {
        UsePauseMenu();
        UpdateUI();
        UpdateLight();
    }

    /// <summary>
    /// Rotates the diractional light
    /// </summary>
    private void UpdateLight()
    {
        float time = Mathf.Abs((gameTimeLeft / gameStartTime) - 1);

        if (float.IsNaN(time))
            lightTransform.rotation = Quaternion.Euler(fromLightRot);
        else
            lightTransform.rotation = Quaternion.Slerp(Quaternion.Euler(fromLightRot), Quaternion.Euler(toLightRot), time);

    }

    /// <summary>
    /// Updates the score
    /// </summary>
    private void UpdateUI()
    {
        if (gameManager.GetGameState().Equals(GameState.Playing) && !gameManager.GetGameState().Equals(GameState.winScreen))
        {
            // Update scores
            for (int i = 0; i < playerCount; i++)
            {
                playersScoreText[i].text = playersScores[i].ToString();
            }


            // Update GameTime
            if (gameTimeLeft <= 0)
            {

                gameTimeLeft = 0;

                if (OverTime() == false)
                {
                    StartCoroutine(WinScreen());
                }
                else
                {
                    gameTimeLeftText.color = overtimeColor;

                    gameTimeLeftText.text = "OVERTIME";
                    gameTimeLeftText.alpha = Mathf.Abs(Mathf.Sin(Time.time * 2));
                }

            }
            else
            {
                inGameUIGroup.gameObject.SetActive(true);
                // alpha
                inGameUIGroup.alpha += Time.deltaTime;

                int minutes = Mathf.FloorToInt(gameTimeLeft / 60F);
                int seconds = Mathf.FloorToInt(gameTimeLeft - minutes * 60);
                string correctTime = string.Format("{0:0}:{1:00}", minutes, seconds);
                gameTimeLeftText.text = correctTime;

                gameTimeLeft -= Time.deltaTime;
            }




        }
    }

    /// <summary>
    /// Returns a bool if overtime is active
    /// </summary>
    /// <returns></returns>
    bool OverTime()
    {
        int bestScore = 0;
        for (int i = 0; i < playersScores.Length; i++)
        {
            if (playersScores[i] > bestScore)
            {
                bestScore = playersScores[i];
            }
        }

        int timesDone = 0;
        for (int i = 0; i < playersScores.Length; i++)
        {
            if (playersScores[i] == bestScore)
            {
                timesDone++;
            }
        }

        if (timesDone >= 2)
            return true;
        else
            return false;
    }

    /// <summary>
    /// Shows the win screen
    /// </summary>
    IEnumerator WinScreen()
    {
        gameManager.SetGameState(GameState.winScreen);
        winScreenGroup.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        camaraAnimatior.SetTrigger("WinScreen");


        int bestPlayer = 0;
        for (int i = 0; i < playersScores.Length; i++)
        {
            if (playersScores[i] > playersScores[bestPlayer])
            {
                bestPlayer = i;
            }
        }

        switch (bestPlayer)
        {
            case 0:
                winScreenText.text = "Yellow Wins!";

                break;
            case 1:
                winScreenText.text = "Red Wins!";

                break;
            case 2:
                winScreenText.text = "Blue Wins!";

                break;
            case 3:
                winScreenText.text = "Purple Wins!";

                break;
        }

        while (inGameUIGroup.alpha > 0)
        {
            inGameUIGroup.alpha -= Time.unscaledDeltaTime * 3;
            yield return new WaitForEndOfFrame();
        }

        while (winScreenCanvasGroup.alpha < 1)
        {
            winScreenCanvasGroup.alpha += Time.unscaledDeltaTime * 3;
            yield return new WaitForEndOfFrame();
        }

    }

    /// <summary>
    /// Sets up all the game Ui
    /// </summary>
    public void StartGameUI(int _playerCount, int _gameTime, int _countdownTime)
    {
        StartCountdown(_countdownTime);

        playerCount = _playerCount;
        gameTimeLeft = _gameTime - cheatTimeMakeZero;
        gameStartTime = gameTimeLeft;

        for (int i = 0; i < playerCount; i++)
        {
            playersScoreText[i].gameObject.SetActive(true);
            switch (Mathf.Abs(playerCount - 4))
            {
                case 0:
                    playersScoreText[i].rectTransform.position += new Vector3(0,0);
                    break;
                case 1:
                    playersScoreText[i].rectTransform.position += new Vector3(20, 0);
                    break;
                case 2:
                    playersScoreText[i].rectTransform.position += new Vector3(60, 0);
                    break;

            }
        }
    }

    /// <summary>
    /// Adds score to the playerScores
    /// </summary>
    public void AddPlayerScore(int score, PlayerId playerID)
    {
        StartCoroutine(AnimateScore(score, playerID));
    }
    IEnumerator AnimateScore(int score, PlayerId playerID)
    {
        for (int i = 0; i < score; i++)
        {
            yield return new WaitForSeconds(0.1f);
            playersScoreText[(int)playerID].GetComponent<Animator>().SetTrigger("Add");
            playersScores[(int)playerID]++;
        }
    }

    /// <summary>
    /// Turns the menu on and off by button
    /// </summary>
    private void UsePauseMenu()
    {

        if (gameManager.GetGameState().Equals(GameState.Playing) || gameManager.GetGameState().Equals(GameState.pause))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameManager.GetGameState().Equals(GameState.pause))
                {
                    Resume();
                }
                else if (gameManager.GetGameState().Equals(GameState.Playing))
                {
                    OpenPauseMenu();
                }
            }

            timeScaleTimer += Time.unscaledDeltaTime;

            if (gameManager.GetGameState().Equals(GameState.pause))
                Time.timeScale = 0;
            else
                Time.timeScale = Mathf.Lerp(0, 1, timeScaleTimer * 0.75f);
        }
    }

    /// <summary>
    /// Works the ui back to the menu and gives the rest to game manager
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// QUit Button
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Works the ui back to the menu and gives the rest to game manager
    /// </summary>
    public void Resume()
    {
        inGameMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        timeScaleTimer = 0;
        gameManager.SetGameState(GameState.Playing);

    }
    public void OpenPauseMenu()
    {
        inGameMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        timeScaleTimer = 0;
        gameManager.SetGameState(GameState.pause);

    }


    public void StartCountdown(int _countdownTime)
    {
        StartCoroutine(IStartCountdown(_countdownTime));
    }
    private IEnumerator IStartCountdown(int _countdownTime)
    {
        int time = _countdownTime;
        Animator animator = countdownText.GetComponent<Animator>();
        countdownText.gameObject.SetActive(true);

        for (int i = 0; i < _countdownTime; i++)
        {
            countdownText.text = time.ToString();
            animator.SetTrigger("Add");
            time--;
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "START!";
        animator.SetTrigger("Add");
        yield return new WaitForSeconds(1);

        Destroy(countdownText.gameObject);
    }

}
