using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIManager
{
    private static UIManager instance;

    private Dictionary<UIPanelType, string> panelPathDict;
    private Dictionary<UIPanelType, BasePanel> panelDict;
    private Stack<BasePanel> panelStack;

    private Transform canvasTransform;
    private Transform CanvasTransform
    {
        get {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").transform;
            }
            return canvasTransform;
        }
    }

    public static UIManager Instance {  
        get { 
            if(instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    /// <summary>
    /// 用类包裹UIPanelInfoList
    /// </summary>
    [Serializable]
    class UIPanelList
    {
        public List<UIPanelInfo> infoList;
    }

    private UIManager()
    {
        ParseUIPanelTypeJson();
    }
    
    /// <summary>
    /// 
    /// </summary>
    private void ParseUIPanelTypeJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();
        TextAsset textAsset = Resources.Load<TextAsset>("UIPanelType");
        UIPanelList infoList = JsonUtility.FromJson<UIPanelList>(textAsset.text);
        foreach (UIPanelInfo info in infoList.infoList) {
            panelPathDict.Add(info.panelType, info.path);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public BasePanel GetPanel(UIPanelType type)
    {
        if(panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        //BasePanel panel;
        //panelDict.TryGetValue(type, out panel);
        BasePanel panel = panelDict.TryGet(type);

        if (panel == null) {
            //string path;
            //panelPathDict.TryGetValue(type, out path);
            string path = panelPathDict.TryGet(type);
            GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
            instPanel.transform.SetParent(CanvasTransform, false);
            panelDict.Add(type, instPanel.GetComponent<BasePanel>());
            return instPanel.GetComponent<BasePanel>();
        }
        else
        {
            return panel;
        }
    }

    public void PushPanel(UIPanelType panelType)
    {
        if (panelStack == null)
            panelStack = new Stack<BasePanel>();
        if (panelStack.Count > 0) {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        panelStack.Push(panel);
    }

    public void PopPanel()
    {
        if( panelStack == null)
            panelStack = new Stack<BasePanel>();
        if (panelStack.Count <= 0)
            return;

        BasePanel _topPanel = panelStack.Pop();
        _topPanel.OnExit();
        BasePanel _topPanel2 = panelStack.Peek();
        _topPanel2.OnResume();

    }


    public void Test()
    {
        string path;
        panelPathDict.TryGetValue(UIPanelType.Knapsack, out path);
        Debug.Log(path);
    }
}
