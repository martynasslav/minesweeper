using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour {
	public GameObject counterNumberPrefab;
	public int value = 0;

	GameObject[] m_CounterNumbersObj;
	CounterNumber[] m_CounterNumbers;

	void SetNumberByValue(CounterNumber number, int value) {
		if (value == 0) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, true);
			number.ChangeLight(2, true);
			number.ChangeLight(3, false);
			number.ChangeLight(4, true);
			number.ChangeLight(5, true);
			number.ChangeLight(6, true);
		}
		else if (value == 1) {
			number.ChangeLight(0, false);
			number.ChangeLight(1, false);
			number.ChangeLight(2, true);
			number.ChangeLight(3, false);
			number.ChangeLight(4, false);
			number.ChangeLight(5, true);
			number.ChangeLight(6, false);
		}
		else if (value == 2) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, false);
			number.ChangeLight(2, true);
			number.ChangeLight(3, true);
			number.ChangeLight(4, true);
			number.ChangeLight(5, false);
			number.ChangeLight(6, true);
		}
		else if (value == 3) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, false);
			number.ChangeLight(2, true);
			number.ChangeLight(3, true);
			number.ChangeLight(4, false);
			number.ChangeLight(5, true);
			number.ChangeLight(6, true);
		}
		else if (value == 4) {
			number.ChangeLight(0, false);
			number.ChangeLight(1, true);
			number.ChangeLight(2, true);
			number.ChangeLight(3, true);
			number.ChangeLight(4, false);
			number.ChangeLight(5, true);
			number.ChangeLight(6, false);
		}
		else if (value == 5) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, true);
			number.ChangeLight(2, false);
			number.ChangeLight(3, true);
			number.ChangeLight(4, false);
			number.ChangeLight(5, true);
			number.ChangeLight(6, true);
		}
		else if (value == 6) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, true);
			number.ChangeLight(2, false);
			number.ChangeLight(3, true);
			number.ChangeLight(4, true);
			number.ChangeLight(5, true);
			number.ChangeLight(6, true);
		}
		else if (value == 7) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, false);
			number.ChangeLight(2, true);
			number.ChangeLight(3, false);
			number.ChangeLight(4, false);
			number.ChangeLight(5, true);
			number.ChangeLight(6, false);
		}
		else if (value == 8) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, true);
			number.ChangeLight(2, true);
			number.ChangeLight(3, true);
			number.ChangeLight(4, true);
			number.ChangeLight(5, true);
			number.ChangeLight(6, true);
		}
		else if (value == 9) {
			number.ChangeLight(0, true);
			number.ChangeLight(1, true);
			number.ChangeLight(2, true);
			number.ChangeLight(3, true);
			number.ChangeLight(4, false);
			number.ChangeLight(5, true);
			number.ChangeLight(6, true);
		}
		else if(value == 10) { // complete turn off
			number.ChangeLight(0, false);
			number.ChangeLight(1, false);
			number.ChangeLight(2, false);
			number.ChangeLight(3, false);
			number.ChangeLight(4, false);
			number.ChangeLight(5, false);
			number.ChangeLight(6, false);
		}
	}

	void SetNumbers() {
		if(value > 999) {
			SetNumberByValue(m_CounterNumbers[0], 9);
			SetNumberByValue(m_CounterNumbers[1], 9);
			SetNumberByValue(m_CounterNumbers[2], 9);
		}
		else if(value > 99) {
			int firstDigit = value / 100;
			int secondDigit = (value - 100 * firstDigit) / 10;
			int thirdDigit = value % 10;

			SetNumberByValue(m_CounterNumbers[0], firstDigit);
			SetNumberByValue(m_CounterNumbers[1], secondDigit);
			SetNumberByValue(m_CounterNumbers[2], thirdDigit);
		}
		else if(value > 9) {
			int secondDigit = value / 10;
			int thirdDigit = value % 10;

			SetNumberByValue(m_CounterNumbers[0], 10);
			SetNumberByValue(m_CounterNumbers[1], secondDigit);
			SetNumberByValue(m_CounterNumbers[2], thirdDigit);
		}
		else if(value > 0) {
			SetNumberByValue(m_CounterNumbers[0], 10);
			SetNumberByValue(m_CounterNumbers[1], 10);
			SetNumberByValue(m_CounterNumbers[2], value);
		}
		else {
			SetNumberByValue(m_CounterNumbers[0], 10);
			SetNumberByValue(m_CounterNumbers[1], 10);
			SetNumberByValue(m_CounterNumbers[2], 0);
		}


		/*
		if (value > 999) {
			SetNumberByValue(m_CounterNumbers[0], 9);
			SetNumberByValue(m_CounterNumbers[1], 9);
			SetNumberByValue(m_CounterNumbers[2], 9);
		}
		else {
			int firstDigit = value / 100;
			int secondDigit = (value - firstDigit * 100) / 10;
			int thirdDigit = value - firstDigit * 100 - secondDigit * 10;

			SetNumberByValue(m_CounterNumbers[0], firstDigit);
			SetNumberByValue(m_CounterNumbers[1], secondDigit);
			SetNumberByValue(m_CounterNumbers[2], thirdDigit);
		}
		*/
	}

	private void Start() {
		m_CounterNumbersObj = new GameObject[3];
		m_CounterNumbers = new CounterNumber[3];

		m_CounterNumbersObj[0] = Instantiate(counterNumberPrefab, new Vector3(gameObject.transform.position.x - 0.06f, gameObject.transform.position.y, -1f), Quaternion.identity, gameObject.transform);
		m_CounterNumbersObj[0].name = "CounterNumberLeft";
		m_CounterNumbers[0] = m_CounterNumbersObj[0].GetComponent<CounterNumber>();

		m_CounterNumbersObj[1] = Instantiate(counterNumberPrefab, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1f), Quaternion.identity, gameObject.transform);
		m_CounterNumbersObj[1].name = "CounterNumberCenter";
		m_CounterNumbers[1] = m_CounterNumbersObj[1].GetComponent<CounterNumber>();

		m_CounterNumbersObj[2] = Instantiate(counterNumberPrefab, new Vector3(gameObject.transform.position.x + 0.06f, gameObject.transform.position.y, -1f), Quaternion.identity, gameObject.transform);
		m_CounterNumbersObj[2].name = "CounterNumberRight";
		m_CounterNumbers[2] = m_CounterNumbersObj[2].GetComponent<CounterNumber>();
	}

	void Update() {
		SetNumbers();
    }
}
