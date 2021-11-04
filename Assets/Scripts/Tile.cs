using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	public bool isRevealed = false;
	public int state = 0; // 0-8 - 0-8, 9 - bomb
	public int id;
	public int x;
	public int y;
	
	GameObject m_Master;
	BoardManager m_BoardManager;
	SpriteRenderer m_SpriteManager;

	bool m_WasHeld = false;

	private void OnMouseUpAsButton() {
		m_BoardManager.Pressed(x, y);
	}

	private void OnMouseDown() {
		if (!isRevealed) {
			m_BoardManager.OnHold(x, y);
			m_WasHeld = true;
		}
	}

	private void OnMouseUp() {
		if (m_WasHeld && !isRevealed) {
			m_BoardManager.WasReleased(x, y);
			m_WasHeld = false;
		}
	}

	public void SetSprite(Sprite sprite) {
		m_SpriteManager.sprite = sprite;
	}

	public void SetMaster(GameObject gameObject) {
		m_Master = gameObject;
	}

	void Start() {
		m_SpriteManager = GetComponent<SpriteRenderer>();
		m_BoardManager = m_Master.GetComponent<BoardManager>();
    }
}
