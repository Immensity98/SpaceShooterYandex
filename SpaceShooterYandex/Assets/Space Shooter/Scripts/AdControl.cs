using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YG;

// контроль открытия рекламы. Особенность плагина YG в том, что если таймер рекламы ещё не истёк, то реклама просто не откроется и никто об этом не узнает.
// Поэтому время нужно контролировать самому, при этом диспетчируя вызов помимо рекламы.

public class AdControl : MonoBehaviour {

    bool allowFullscreenAd = false;
    public UnityEvent passOpenAd; // событие, которое запускается вместо рекламы, если её нельзя показывать.

    // а рекламу открываем, только если есть такая возможность
    public void OpenFullscreenAd () {
        if (allowFullscreenAd) {
            YandexGame.FullscreenShow();
            allowFullscreenAd = false;
        } else {
            passOpenAd.Invoke();
        }
    }

    // 2024-02-25 18:12:01 делать рекламу на перезапуске игры до проигрыша
    // это невероятная костыльная тупизна, не верю, что мне приходится это делать
    // нужно отловить момент, когда таймер закончился и открыть возможность для рекламы
    void Update () {
        if (!allowFullscreenAd && YandexGame.timerShowAd >= YandexGame.Instance.infoYG.fullscreenAdInterval) allowFullscreenAd = true;
    }


}
