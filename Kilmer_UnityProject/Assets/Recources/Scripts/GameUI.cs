using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{

    [SerializeField] GameManager gameManager = null;
    [SerializeField] GameObject inGameMenu = null;
    [SerializeField] CanvasGroup inGameUIGroup = null;
    [SerializeField] private TextMeshProUGUI[] playersScoreText = null;
    [SerializeField] private TextMeshProUGUI gameTimeLeftText = null;

    [SerializeField] private TextMeshProUGUI winScreenText = null;
    [SerializeField] CanvasGroup winScreenCanvasGroup = null;
    [SerializeField] GameObject winScreenGroup = null;
    [SerializeField] private float cheatTimeMakeZero;

    [SerializeField] private Color overtimeColor;


    private int[] playersScores = new int[4];
    private float timeScaleTimer;


    private int playerCount;
    private float gameTimeLeft;

    [SerializeField]  private Transform lightTransform;
    [SerializeField] private Vector3 toLightRot;
    private float gameStartTime;
    private Vector3 fromLightRot;

    private void Start()
    {
        fromLightRot = lightTransform.eulerAngles;


        inGameMenu.SetActive(false);
        inGameUIGroup.alpha = 0;
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

    private void UpdateLight()
    {
        float time = Mathf.Abs((gameTimeLeft / gameStartTime) - 1);

        if (float.IsNaN(time))
            lightTransform.rotation = Quaternion.Euler(fromLightRot);
        else
            lightTransform.rotation = Quaternion.Slerp(Quaternion.Euler(fromLightRot),Quaternion.Euler(toLightRot), time);

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

        int bestPlayer = 0;
        for (int i = 0; i < playersScores.Length; i++)
        {
            if (playersScores[i] > playersScores[bestPlayer])
            {
                bestPlayer = i;
            }
        }

        winScreenText.text = "GameEind, the best player is player: " + bestPlayer + " the score was: " + playersScores[bestPlayer];

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
    public void StartGameUI(int _playerCount, int gameTime)
    {
        Cursor.visible = false;
        playerCount = _playerCount;
        gameTimeLeft = gameTime - cheatTimeMakeZero;
        gameStartTime = gameTimeLeft;

        for (int i = 0; i < playerCount; i++)
        {
            playersScoreText[i].gameObject.SetActive(true);
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
        if (!gameManager.GetGameState().Equals(GameState.winScreen))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameManager.GetGameState().Equals(GameState.pause))
                {
                    inGameMenu.SetActive(false);
                    Cursor.visible = false;
                    timeScaleTimer = 0;
                    gameManager.SetGameState(GameState.Playing);
                }
                else if (gameManager.GetGameState().Equals(GameState.Playing))
                {
                    inGameMenu.SetActive(true);
                    Cursor.visible = true;
                    timeScaleTimer = 0;
                    gameManager.SetGameState(GameState.pause);
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
        SceneManager.LoadScene(0);
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
        timeScaleTimer = 0;
        gameManager.SetGameState(GameState.Playing);
    }

}
