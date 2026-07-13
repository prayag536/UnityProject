using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
public class Gm : MonoBehaviour
{
    public List<GameObject> panels;

    public void pannels(string name)
    {
        SoundManager.Instance.ButttonClick();
        var button = panels.Find(X => X.name == name);
        if (button != null)
        {
            button.SetActive(true);
            foreach (GameObject go in panels)
            {
                if (go != button)
                {
                    go.SetActive(false);
                }
            }
        }
        else
        {
            Debug.Log("panel not found");
        }
    }

    private void OnEnable()
    {
        AdManager.InterstitialAction += PlayButton;
    }
    private void OnDisable()
    {
        AdManager.InterstitialAction -= PlayButton;
    }

    public void PlayButton()
    {
        StartCoroutine(ShowSplashThenLevel());
    }

    public void ExitYesButton()
    {
        Application.Quit();
    }

    public void ExitButton()
    {
        pannels("Exit Panel");
    }

    public void ExitNoButton()
    {
        pannels("Home Panel");
    }

    public void SettingButton()
    {
        pannels("Setting Panel");
    }

    public void SettingBackButton()
    {
        pannels("Home Panel");
    }

    public void LevelBackButton()
    {
        pannels("Home Panel");
    }

    public void level1Button()
    {
        SceneManager.LoadScene(1);
    }
    public void level2Button()
    {
        SceneManager.LoadScene(2);
    }
    public void level3Button()
    {
        SceneManager.LoadScene(3);
    }

    IEnumerator ShowSplashThenLevel()
    {
        SoundManager.Instance.LevelSound();
        pannels("SplashScreen");
        Show();
        yield return new WaitForSeconds(0.1f);

        AdManager.Instance.ShowInterstitialAd();

        pannels("Level Panel");
        Show();
    }
    public CanvasGroup splashCanvasGroup; // Use CanvasGroup for fading

    public void Show()
    {
        gameObject.SetActive(true);
        splashCanvasGroup.alpha = 0;
        splashCanvasGroup.DOFade(1, 1f); // Fade in over 1 second
    }

}
