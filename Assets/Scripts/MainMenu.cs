using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _askInput;
    private float _timer = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        
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