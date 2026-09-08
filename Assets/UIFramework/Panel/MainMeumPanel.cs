
using UnityEngine;

public class MainPanel : BasePanel
{ 
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void OnPause()
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        _canvasGroup.blocksRaycasts = true;
    }

    public void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);
    }

}
