using UnityEngine;
using UnityEngine.Events;


// показалось, что можно легко совместить простой коммандер с простым эникейщиком
// Потому что нет смысла делать отдельный эникейщик, т.к. он всё равно будет коммандером.
// а мой keyboardist не умеет в эникей, и ломать его я не хочу
// Добавляю важную фичу - охрану от выключенного объекта. Это позволит делать условное срабатывание.

public interface IDoable {
		public void Do ();
		public void Reset ();
	}

public class Commander : MonoBehaviour, IDoable {

	[Tooltip("заблокироваться сразу после Do")]
	public bool block = false; // заблокироваться сразу после Do
	private bool blocked = false;
	public UnityEvent command;
	public bool anyKey = false;
	public bool doChildren = false; // отработать всех активных потомков первого уровня
	public bool doOnEnable = false; // иногда нужно срабатывать на старте
    public bool autoDestroy = false; // странная логика с blocked, чтоб её не ломать, просто удалю после Do

	void OnEnable () {
		if (doOnEnable) Do();
	}

	public void Do () {
		if (isActiveAndEnabled) command.Invoke();
		if (doChildren) {
			foreach (IDoable c in GetComponentsInChildren<IDoable>()) { // только активные
				if (c != this) c.Do(); // охрана от этого коммандера!
			}
		}
        if (autoDestroy) Destroy(gameObject);
	}

	public void Reset () {
		blocked = false;
	}

	bool AnyKey () {
	//return Keyboard.current.anyKey.isPressed || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed || Mouse.current.middleButton.isPressed;
	return Input.anyKey || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
	}

	void Update () {
		if (!blocked && anyKey && AnyKey()) {
			Do();
			if (block) blocked = true;
		}
	}

}

