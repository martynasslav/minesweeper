using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterNumber : MonoBehaviour {
	SpriteRenderer[] m_Lights;

	public void ChangeLight(int pos, bool lit) {
		float alpha;

		if(lit) {
			alpha = 255f;
		}
		else {
			alpha = 0f;
		}

		m_Lights[pos].color = new Color(255f, 255f, 255f, alpha);
	}

	void Start() {
		m_Lights = new SpriteRenderer[7];

		for (int i = 0; i < 7; i++) {
			Transform parentTransform = gameObject.transform;
			Transform childTransform = gameObject.transform.Find("CounterNumberLight" + (i + 1).ToString());
			GameObject light = childTransform.gameObject;

			m_Lights[i] = light.GetComponent<SpriteRenderer>();
		}
    }
}
