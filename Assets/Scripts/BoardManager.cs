using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {
	struct XY {
		public int x;
		public int y;
	}

	int rows = 10; // min 4 each
	int columns = 10;
	int bombCount = 20; // min = 1; max = rows * columns - 1
	public Sprite[] sprites;
	/*
	 * 0 - with cover
	 * 1 - 0
	 * 2 - 1
	 * 3 - 2
	 * 4 - 3
	 * 5 - 4
	 * 6 - 5
	 * 7 - 6
	 * 8 - 7
	 * 9 - 8
	 * 10 - bomb
	*/

	List<List<Tile>> m_Tiles;
	GameObject m_TilePrefab;
	GameObject m_GameManagerObj;
	GameManager m_GameManager;

	bool m_Started = false;

	string ReturnName(int id) {
		string name = "Tile" + id.ToString();
		return name;
	}

	void SetupTile(Tile tile, int id, int x, int y) {
		tile.id = id;
		tile.x = x;
		tile.y = y;
		tile.SetMaster(gameObject);
	}

	XY NewXY(int x, int y) {
		XY n = new XY();
		n.x = x;
		n.y = y;

		return n;
	}

	public void SetDimensions(int newColumns, int newRows, int newBombCount) {
		columns = newColumns;
		rows = newRows;
		bombCount = newBombCount;
	}

	void SetupBombs(bool safeStart = false, int SafeX = 0, int SafeY = 0) {
		List<List<XY>> available = new List<List<XY>>();
		for(int y = 0; y < rows; y++) {
			available.Add(new List<XY>());
			for(int x = 0; x < columns; x++) {
				available[y].Add(NewXY(x, y));
			}
		}

		if(safeStart) {
			int x = SafeX;
			int y = SafeY;
			int spaces = rows * columns - bombCount;

			available[y].Remove(NewXY(x, y));
			spaces--;

			if(spaces > 0 && IsValid(x - 1, y + 1)) {
				available[y + 1].Remove(NewXY(x - 1, y + 1));
				spaces--;
			}
			if (spaces > 0 && IsValid(x, y + 1)) {
				available[y + 1].Remove(NewXY(x, y + 1));
				spaces--;
			}
			if (spaces > 0 && IsValid(x + 1, y + 1)) {
				available[y + 1].Remove(NewXY(x + 1, y + 1));
				spaces--;
			}
			if (spaces > 0 && IsValid(x - 1, y)) {
				available[y].Remove(NewXY(x - 1, y));
				spaces--;
			}
			if (spaces > 0 && IsValid(x + 1, y)) {
				available[y].Remove(NewXY(x + 1, y));
				spaces--;
			}
			if (spaces > 0 && IsValid(x - 1, y - 1)) {
				available[y - 1].Remove(NewXY(x - 1, y - 1));
				spaces--;
			}
			if (spaces > 0 && IsValid(x, y - 1)) {
				available[y - 1].Remove(NewXY(x, y - 1));
				spaces--;
			}
			if (spaces > 0 && IsValid(x + 1, y - 1)) {
				available[y - 1].Remove(NewXY(x + 1, y - 1));
				spaces--;
			}
		}

		for(int i = 0; i < bombCount; i++) {
			int availableY = Random.Range(0, available.Count - 1);
			int availableX = Random.Range(0, available[availableY].Count - 1);
			XY pos = available[availableY][availableX];

			m_Tiles[pos.y][pos.x].state = 9;
			available[availableY].RemoveAt(availableX);

			if(available[availableY].Count == 0) {
				available.RemoveAt(availableY);
			}
		}
	}

	bool IsBomb(int x, int y) {
		if(y >= 0 && y <= rows - 1) {
			if(x >= 0 && x <= columns - 1) {
				if(m_Tiles[y][x].state == 9) {
					return true;
				}
			}
		}

		return false;
	}

	bool IsValid(int x, int y) {
		if (y >= 0 && y <= rows - 1) {
			if (x >= 0 && x <= columns - 1) {
				return true;
			}
		}

		return false;
	}

	void SetupStates() {
		for(int y = 0; y < rows; y++) {
			for(int x = 0; x < columns; x++) {
				if(m_Tiles[y][x].state == 0) {
					int count = 0;

					if (IsBomb(x - 1, y + 1)) count++;
					if (IsBomb(x, y + 1)) count++;
					if (IsBomb(x + 1, y + 1)) count++;
					if (IsBomb(x - 1, y)) count++;
					if (IsBomb(x + 1, y)) count++;
					if (IsBomb(x - 1, y - 1)) count++;
					if (IsBomb(x, y - 1)) count++;
					if (IsBomb(x + 1, y - 1)) count++;

					m_Tiles[y][x].state = count;
				}
			}
		}
	}

	public void SetupBoard() {
		int count = 0;

		m_Tiles = new List<List<Tile>>();

		for(int y = 0; y < rows; y++) {
			m_Tiles.Add(new List<Tile>());
			for(int x = 0; x < columns; x++) {
				Vector3 tilePos;
				tilePos.x = (float)x;
				tilePos.y = (float)y;
				tilePos.z = 0f;

				GameObject tempTileObj = (Instantiate(m_TilePrefab, tilePos, Quaternion.identity)) as GameObject;
				Tile tempTile = tempTileObj.GetComponent<Tile>(); 
				
				tempTileObj.name = ReturnName(count);


				SetupTile(tempTile, count, x, y);

				tempTileObj.transform.parent = gameObject.transform;

				m_Tiles[y].Add(tempTile);

				count++;
			}
		}
	}

	public void OnHold(int x, int y) {
		Tile tile = m_Tiles[y][x];
		tile.SetSprite(sprites[1]);
	}

	public void WasReleased(int x, int y) {
		Tile tile = m_Tiles[y][x];
		tile.SetSprite(sprites[0]);
	}

	public void Pressed(int x, int y) {
		if(!m_Started) {
			SetupBombs(m_GameManager.useSafeStart, x, y);
			SetupStates();
			m_Started = true;
		}

		Reveal(x, y);
	}

	void Reveal(int x, int y) {
		if (m_Tiles[y][x].state == 9) {
			RevealTile(x, y);
		}
		else if (m_Tiles[y][x].state == 0) {
			RevealEmpty(x, y);
		}
		else {
			RevealTile(x, y);
		}
	}

	void RevealEmpty(int x, int y) {
		RevealTile(x, y);

		if(IsValid(x, y + 1)) {
			Tile tile = m_Tiles[y + 1][x];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if (IsValid(x - 1, y)) {
			Tile tile = m_Tiles[y][x - 1];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if (IsValid(x + 1, y)) {
			Tile tile = m_Tiles[y][x + 1];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if (IsValid(x, y - 1)) {
			Tile tile = m_Tiles[y - 1][x];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if(IsValid(x - 1, y + 1)) {
			Tile tile = m_Tiles[y + 1][x - 1];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if(IsValid(x + 1, y + 1)) {
			Tile tile = m_Tiles[y + 1][x + 1];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if(IsValid(x - 1, y - 1)) {
			Tile tile = m_Tiles[y - 1][x - 1];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
		if(IsValid(x + 1, y - 1)) {
			Tile tile = m_Tiles[y - 1][x + 1];
			if (!tile.isRevealed) {
				if (tile.state == 0) {
					RevealEmpty(tile.x, tile.y);
				}
				else if (tile.state != 9) {
					RevealTile(tile.x, tile.y);
				}
			}
		}
	}

	void RevealTile(int x, int y) {
		int state = m_Tiles[y][x].state;

		m_Tiles[y][x].SetSprite(sprites[state + 1]);
		m_Tiles[y][x].isRevealed = true;
	}

	public void SetTilePrefab(GameObject tilePrefab) {
		m_TilePrefab = tilePrefab;
	}

	private void Start() {
		m_GameManagerObj = GameObject.Find("GameManager");
		m_GameManager = m_GameManagerObj.GetComponent<GameManager>();
	}
}
