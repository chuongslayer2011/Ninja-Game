using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    public void Pause()
    {
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
