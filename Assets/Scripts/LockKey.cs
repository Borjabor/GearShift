using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockKey : MonoBehaviour
{
    public static int CollectedKeys = 0;
    public static int TotalKeys = 4;

    [SerializeField] TMP_FontAsset _fontCollectible;
    
    public TextMeshProUGUI TargetCount;

    private void Start()
    {
        CollectedKeys = 0;
        SetCountText();
    }

    void FixedUpdate()
    {
        SetCountText();
    }

    void SetCountText()
    {
        TargetCount.font = _fontCollectible;
        TargetCount.text = "Keys: " + CollectedKeys.ToString() + " / " + TotalKeys;
    }
}