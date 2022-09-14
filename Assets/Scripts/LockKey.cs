using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockKey : MonoBehaviour
{
    public static int CollectedKeys = 0;
    public static int _totalKeys = 4;

    [SerializeField] TMP_FontAsset _fontCollectible;
    
    public TextMeshProUGUI TargetCount;

    private void Start()
    {
        SetCountText();
    }

    void FixedUpdate()
    {
        SetCountText();
    }

    void SetCountText()
    {
        TargetCount.font = _fontCollectible;
        TargetCount.text = "Keys: " + CollectedKeys.ToString() + " / " + _totalKeys;
    }
}