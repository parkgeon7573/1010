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
		// �Ͻ����� Panel Ȱ��ȭ, ���� �ִϸ��̼� ���
		pausePanel.OnAppear();
	}

	public void BtnClickHome()
	{
		SceneManager.LoadScene("Main");
	}

	public void BtnClickRestart()
	{
		SceneManager.LoadScene("Game");
		// ���� Ȱ��ȭ�Ǿ� �ִ� ���� "02Game"�̱� ������ �Ʒ��� ���� �ᵵ ��
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void BtnClickPlay()
	{
		// �Ͻ����� Panel ���� �ִϸ��̼� ���, ��Ȱ��ȭ
		pausePanel.OnDisappear();
	}	
}
