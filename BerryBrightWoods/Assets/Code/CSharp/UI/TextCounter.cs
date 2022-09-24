using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextCounter : MonoBehaviour
{
    [SerializeField] string additionalText = "";
    [SerializeField] int maxCount = 1;
    [Header("References")]
    [SerializeField] TMPro.TextMeshProUGUI text = null;

    public int count = 0;
    // Update is called once per frame
    void Update()
    {
        ResolveText();
    }
    public float GetPercentage()
    {
        Debug.Assert(maxCount != 0, $"{name}'s max count is 0 when calling GetPercentage!");
        return (float)(count / maxCount);
    }

    private void OnDrawGizmos()
    {
        ResolveText();
    }

    void ResolveText()
    {
        if (text)
            text.text = $"{additionalText} {count} / {maxCount}";
    }
}
