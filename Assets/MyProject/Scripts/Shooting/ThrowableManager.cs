using System.Collections.Generic;
using UnityEngine;

public class ThrowableManager : MonoBehaviour
{
    [SerializeField] private List<ThrowableSettings> throwableList;
    private int _currentIndex;
    private ThrowableSettings _currentThrowable;

    private void Awake()
    {
        if (throwableList.Count > 0)
            _currentThrowable = throwableList[0];
    }

    public ThrowableSettings GetCurrentThrowable() => _currentThrowable;

    public void SwitchNext()
    {
        if (throwableList.Count == 0) return;
        _currentIndex = (_currentIndex + 1) % throwableList.Count;
        _currentThrowable = throwableList[_currentIndex];
        Debug.Log($"Switched throwable to: {_currentThrowable.throwableName}");
    }

    public void SwitchPrevious()
    {
        if (throwableList.Count == 0) return;
        _currentIndex = (_currentIndex - 1 + throwableList.Count) % throwableList.Count;
        _currentThrowable = throwableList[_currentIndex];
        Debug.Log($"Switched throwable to: {_currentThrowable.throwableName}");
    }
}
