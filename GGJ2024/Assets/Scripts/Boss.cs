using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject _angryBoss;
    [SerializeField] GameObject _smileBoss;
    [SerializeField] GameObject _MidTrans;
    [SerializeField] Slider _laughMeter;

    private void Update()
    {
        if (_laughMeter.value < 65)
        {
            _angryBoss.SetActive(true);
            _smileBoss.SetActive(false);
        }
        else if (_laughMeter.value > 65 && _laughMeter.value < 85)
        {
            _angryBoss.SetActive(false);
            _MidTrans.SetActive(true);
        }
        else
        {
            _angryBoss.SetActive(false);
            _smileBoss.SetActive(true);
        }
    }
}
