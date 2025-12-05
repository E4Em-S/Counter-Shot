using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class camerashake : MonoBehaviour
{
    public static camerashake instance;
    private void Awake() => instance = this;

    void onshake(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }

    public static void shake(float duration, float strength) => instance.onshake(duration, strength);
}
