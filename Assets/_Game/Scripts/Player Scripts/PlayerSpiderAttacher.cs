using UnityEngine;

public class PlayerSpiderAttacher : MonoBehaviour
{
    [SerializeField] private GameObject[] _attachedSpiders;
    [SerializeField] private IntEvent _onSpiderAttached;
    private int _currentSpiderCount = 0;

    [SerializeField] private bool _canAttachSpidersDebug = true;

    private void Awake()
    {
        foreach (var spider in _attachedSpiders)
        {
            spider.SetActive(false);
        }
    }

    public void AttachSpider()
    {
        // When all spiders attached
        if (!CanAttachSpider())
        {
            // Spread cocoon on player
            Debug.Log("Spread cocoon");
            return;
        }

        // Attach
        _attachedSpiders[_currentSpiderCount].SetActive(true);
        _currentSpiderCount++;

        // Update UI
        _onSpiderAttached.Raise(_currentSpiderCount);
    }

    public bool CanAttachSpider()
    {
        return _currentSpiderCount < _attachedSpiders.Length && _canAttachSpidersDebug;
    }
}
