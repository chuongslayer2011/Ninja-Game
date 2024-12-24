using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldTime
{
    public class WorldTime : MonoBehaviour
    {
        
        public event EventHandler<TimeSpan> WorldTimeChange;
        [SerializeField]
        private float _dayLength;
        private TimeSpan _currentTime;
        private float _minuteLength => _dayLength / worldTimeConstant.MinutesInDay;
        private void Start()
        {
            StartCoroutine(AddMinute());
        }
        private IEnumerator AddMinute() // tốn hiệu năng hơn invoke 
        {
            _currentTime += TimeSpan.FromMinutes(1);
            WorldTimeChange?.Invoke(this, _currentTime);
            yield return new WaitForSeconds(_minuteLength); 
            //yield return null; : chờ 1 frame
            StartCoroutine(AddMinute());
            //yield return new waitUntil(condition) -> chờ đến khi condition true;
        }
        
    }

}