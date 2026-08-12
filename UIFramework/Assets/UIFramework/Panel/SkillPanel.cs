using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : BasePanel
{
    private CanvasGroup _canvasGroup;

    public void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnEnter()
    {
        if (_canvasGroup == null)
            _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        _canvasGroup.alpha = 0.0f;
        _canvasGroup.blocksRaycasts = false;
    }
}
