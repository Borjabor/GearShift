using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private GameObject _keysInPlace;
    private float _waitTime = 3f;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private bool _isUnlocking;
    private float _currentZoom;
    private float _targetZoom = 4f;
    private float _timeElapsed;

    private void Awake()
    {
        _keysInPlace.SetActive(false);
        _isUnlocking = false;
        _currentZoom = 8;
        _timeElapsed = 0;
    }

    private void FixedUpdate()
    {
        if (_isUnlocking && _timeElapsed < _waitTime)
        {
            _virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(_currentZoom, _targetZoom, _timeElapsed/_waitTime);
            _timeElapsed += Time.deltaTime;

        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && LockKey.CollectedKeys == LockKey.TotalKeys)
        {
            _isUnlocking = true;
            LoadNextLevel();
        }
    }
    
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void ReloadCurrentLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        _keysInPlace.SetActive(true);
        yield return new WaitForSeconds(_waitTime);
        SceneManager.LoadScene(levelIndex);

    }
}
