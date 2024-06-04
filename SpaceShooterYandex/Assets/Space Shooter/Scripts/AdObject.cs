using UnityEngine;
using UnityEngine.Events;
using YG;

// 2024-02-25 21:05:10 Особый объект-слушатель, подписывается на события рекламы, может себя удалить для выключения контура управления

public class AdObject : MonoBehaviour {

		public UnityEvent onCloseFullAd; // костыль к YandexGame, отложенный слушатель 

		void OnEnable() {
			YandexGame.CloseFullAdEvent += CloseFullAd;
		}
	
		void OnDisable() {
			YandexGame.CloseFullAdEvent -= CloseFullAd;
		}

        public void SelfDestroy () {
            Destroy(gameObject);
        }

		void CloseFullAd () { 
            onCloseFullAd?.Invoke();
        }

}
