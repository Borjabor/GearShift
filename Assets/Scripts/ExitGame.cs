using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class ExitGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _askInput;
    private float _timer = 0f;

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Application.Quit();
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