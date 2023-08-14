using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AliDashScopeSDK;

public class TestDialogItem : MonoBehaviour
{
    public Text m_title;
    public Text m_info;

    public DialogueHistoryEntity Entity { get; internal set; }

    public void SetInfo(DialogueHistoryEntity entity)
    {
        Entity = entity;
        m_title.text = entity.user + "?";
        m_info.text = entity.bot;
        m_info.gameObject.SetActive(false);
    }
    public void SetResult(string text)
    {
        Entity.bot = text;
        m_info.text = text;
        m_info.gameObject.SetActive(true);
    }
    public void CopyClipBold()
    {
        UnityEngine.GUIUtility.systemCopyBuffer = m_info.text;
    }
}
