using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public bool useSafeStart = true;
	public GameObject boardManagerPrefab;
	public GameObject tilePrefab;
	public GameObject camera;

	public int rows = 10;
	public int columns = 10;
	public int bombCount = 20;

	GameObject m_BoardManagerObj;
	BoardManager m_BoardManager;
	Camera m_Camera;

	void SetupCameraAspect() {
		float targetAspect = (float)columns / rows;
		float screenAspect = (float)Screen.width / Screen.height;
		float scaleHeight = screenAspect / targetAspect;
		if (scaleHeight < 1f) {
			Rect rect = m_Camera.rect;

			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;

			m_Camera.rect = rect;
		}
		else {
			float scaleWidth = 1.0f / scaleHeight;

			Rect rect = m_Camera.rect;

			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scaleWidth) / 2.0f;
			rect.y = 0;

			m_Camera.rect = rect;
		}
	}

	void SetupResolution() {
		int initialScreenHeight = Screen.height;
		
		Screen.SetResolution(initialScreenHeight / rows * columns, initialScreenHeight, false);
	}

	void SetupCamera() {
		SetupResolution();

		// SetupCameraAspect();	
		camera.transform.position = new Vector3((float)(columns - 1) / 2, (float)(rows - 1) / 2, -10f);

		m_Camera.orthographicSize = (float)rows / 2;
		
	}

	private void Start() {
		m_BoardManagerObj = (Instantiate(boardManagerPrefab)) as GameObject;
		m_BoardManagerObj.name = "BoardManager";

		m_BoardManager = m_BoardManagerObj.GetComponent<BoardManager>();
		m_BoardManager.SetTilePrefab(tilePrefab);
		m_BoardManager.SetDimensions(columns, rows, bombCount);
		m_BoardManager.SetupBoard();

		m_Camera = camera.GetComponent<Camera>();
		SetupCamera();
	}
}