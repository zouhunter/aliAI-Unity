using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AliDashScopeSDK;
using UFrame.LitUI;
using UnityEngine.UI;

using System;

public class TestEntryBehavirour : UIView
{
    [SerializeField]
    protected Button m_SendButton;

    [SerializeField]
    protected Button m_ClearBtn;
    [SerializeField]
    protected InputField m_InputField;

    [SerializeField]
    protected VerticalLayoutGroup m_Content;

    [SerializeField]
    protected TestDialogItem m_DialogItem;

    [SerializeField]
    protected ScrollRect m_Scroll_View;


    private List<DialogueHistoryEntity> historys;

    private Stack<TestDialogItem> m_pool;

    private List<TestDialogItem> m_resultList;

    private DashScopeService m_service;
    // Start is called before the first frame update
    void Start()
    {
        var apiKeyAsset = Resources.Load<TextAsset>("apikey");
        m_service = new DashScopeService(apiKeyAsset.text);
        m_DialogItem.gameObject.SetActive(false);
        ResetState();
        m_SendButton.onClick.AddListener(DoSend);
        m_ClearBtn.onClick.AddListener(ResetState);
    }

    private void DoSend()
    {
        var inputLine = m_InputField.text.Trim();
        if (string.IsNullOrEmpty(inputLine))
            return;
        var entity = new DialogueHistoryEntity() { user = inputLine, bot = "" };
        var dialogItem = AddShowResult(entity);
        StartCoroutine(m_service.CallAigcText(historys, inputLine, OnRecevie, dialogItem));
        m_resultList.Add(dialogItem);
    }

    private void OnRecevie(string result, object content)
    {
        var rspobj = JsonUtility.FromJson<AigcTextResponseData>(result);
        if (rspobj != null)
        {
            var item = (TestDialogItem)content;
            item.SetResult(rspobj.output.text);
            historys.Add(item.Entity);
        }
        else
        {
            Debug.LogError("on receive:" + result);
        }
    }

    private TestDialogItem AddShowResult(DialogueHistoryEntity entity)
    {
        TestDialogItem dialogItem = null;
        if (m_pool.Count > 0)
        {
            dialogItem = m_pool.Pop();
        }
        if (dialogItem == null)
        {
            dialogItem = Instantiate(m_DialogItem);
            dialogItem.transform.SetParent(m_DialogItem.transform.parent, false);
        }
        dialogItem.gameObject.SetActive(true);
        dialogItem.SetInfo(entity);
        return dialogItem;
    }

    private void ResetState()
    {
        if (historys == null)
            historys = new List<DialogueHistoryEntity>();
        else
            historys.Clear();

        if (m_pool == null)
            m_pool = new Stack<TestDialogItem>();

        if (m_resultList == null)
            m_resultList = new List<TestDialogItem>();

        foreach (var item in m_resultList)
        {
            item.gameObject.SetActive(false);
            m_pool.Push(item);
        }
        m_resultList.Clear();
        m_InputField.text = "Ask one question?";
    }
}
