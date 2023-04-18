using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    StageController stageContoller;

    [Header("Common")]
    [SerializeField]
    TextMeshProUGUI textCurrentScore;

    [SerializeField]
    TextMeshProUGUI textHighScore;
    [SerializeField]
    UIPausePanelAnimation pausePanel;
    // Update is called once per frame
    void Update()
    {
        textCurrentScore.text = stageContoller.CurrentScore.ToString();
        textHighScore.text = stageContoller.HighScore.ToString();
    }

	public void BtnClickPause()
	{
		// 일시정지 Panel 활성화, 등장 애니메이션 재생
		pausePanel.OnAppear();
	}

	public void BtnClickHome()
	{
		SceneManager.LoadScene("Main");
	}

	public void BtnClickRestart()
	{
		SceneManager.LoadScene("Game");
		// 현재 활성화되어 있는 씬이 "02Game"이기 때문에 아래와 같이 써도 됨
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void BtnClickPlay()
	{
		// 일시정지 Panel 퇴장 애니메이션 재생, 비활성화
		pausePanel.OnDisappear();
	}	
}
