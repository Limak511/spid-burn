using TMPro;
using UnityEngine;

public class AttachedSpidersCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attachedSpidersCountText;

    public void UpdateAttachedSpidersCount(int attachedSpidersCount)
    {
        Debug.Log($"Run listener: {attachedSpidersCount}");
        _attachedSpidersCountText.text = attachedSpidersCount.ToString();
    }
}
