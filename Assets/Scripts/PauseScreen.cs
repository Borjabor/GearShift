using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseMenu;
    private bool _isPaused;
    private float _timer = 0f;

    [SerializeField]
    private TextMeshProUGUI _askInput;

    private void Awake()
    {
        PauseGame();
    }

    private void Update()
    {
        if (_isPaused && Input.GetButtonDown("Jump"))
        {
            ResumeGame();
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (_isPaused && touch.phase == TouchPhase.Began)
            {
                ResumeGame();
            }
        }

        //Debug.Log(_timer);

        if (_isPaused)
        {
            _timer += Time.unscaledDeltaTime;
            if (_timer >= 1f)
            {
                _askInput.enabled = false;
            }
            if (_timer >= 1.4f)
            {
                _askInput.enabled = true;
                _timer = 0f;
            }
        }
    }

    void PauseGame() {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _isPaused = true;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        Cursor.visible = false;
        _pauseMenu.SetActive(false);
        _isPaused = false;
    }



}