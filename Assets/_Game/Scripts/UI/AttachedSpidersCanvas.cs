using TMPro;
using UnityEngine;

public class AttachedSpidersCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _attachedSpidersTextObject;
    [SerializeField] private string _attachedSpidersText;

    private void Awake()
    {
        UpdateAttachedSpidersCount(0);
    }

    public void UpdateAttachedSpidersCount(int attachedSpidersCount)
    {
        _attachedSpidersTextObject.text = $"{_attachedSpidersText}{attachedSpidersCount}";
    }

    private void OnValidate()
    {
        _attachedSpidersTextObject.text = $"{_attachedSpidersText}";
    }
}
