using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainScenario : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textHighScore;

    private void Awake()
    {
        textHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
    }

    public void BtnClickGameStart()
    {
        SceneManager.LoadScene("Game");
    }

    public void BtnClickGameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }
}
