using UnityEngine;

[System.Serializable]
public class SavedGame {

	[SerializeField]
	public int highscore; // рекорд, который идёт в лидерборд

	public SavedGame() {
        // пустой конструктор, на будущее
	}
}
