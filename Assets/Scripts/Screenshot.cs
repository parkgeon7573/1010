using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    Camera screenshotCamera;
	public Sprite ScreenshotToSprite()
	{
		int width = Screen.width;
		int height = Screen.height;

		// width, height ũ���� RenderTexture ����
		RenderTexture renderTexture = new RenderTexture(width, height, 24);
		// Camera ������Ʈ�� targetTexture ������ renderTexture ��� (ī�޶� �Կ��� ȭ���� renderTexture�� ����)
		screenshotCamera.targetTexture = renderTexture;

		// Render() �޼ҵ带 ȣ���� ���� ī�޶��� ȭ���� �Կ��ϰ�(renderTexture�� �����),
		// renderTexture�� RenderTexture.active�� ����
		screenshotCamera.Render();
		RenderTexture.active = renderTexture;

		// ���� Ȱ��ȭ�Ǿ� �ִ� RenderTexture(=renderTexture)�� Pixels ������ �о��
		// Texture2D Ÿ���� screenshot�� ����
		Texture2D screenshot = new Texture2D(width, width, TextureFormat.RGB24, false);
		screenshot.ReadPixels(new Rect(0, (height - width) * 0.5f, width, width), 0, 0);
		screenshot.Apply();

		screenshotCamera.targetTexture = null;

		// Texture2D -> Sprite�� Ÿ�� ��ȯ
		Rect rect = new Rect(0, 0, screenshot.width, screenshot.height);
		Sprite sprite = Sprite.Create(screenshot, rect, Vector2.one * 0.5f);

		return sprite;
	}
}
