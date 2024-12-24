using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeatherStates
{
    Clear,
    Rain
}
public class Weather : MonoBehaviour
{
    [Range(0f, 1f)][SerializeField] float chanceToChangeWeather = 0.02f;
    WeatherStates currentWeatherState = WeatherStates.Clear;
    [SerializeField] ParticleSystem rainObject;
    public void RandomWeatherChangeCheck()
    {
        if (UnityEngine.Random.value < chanceToChangeWeather)
        {
            RandomWeatherChange();
        }
    }
    public void RandomWeatherChange()
    {
        WeatherStates newWeatherState = (WeatherStates)UnityEngine.Random.Range(0, Enum.GetNames(typeof(WeatherStates)).Length);
        ChangeWeather(newWeatherState);
    }
    private void ChangeWeather(WeatherStates newWeatherState)
    {
        currentWeatherState = newWeatherState;
        Debug.Log(currentWeatherState);
        UpdateWeather();
    }
    private void UpdateWeather()
    {
      switch(currentWeatherState)
        {
            case WeatherStates.Clear:
                rainObject.gameObject.SetActive(false); 
                break;
            case WeatherStates.Rain:
                rainObject.gameObject.SetActive(true); 
                break;
        }
    }
}