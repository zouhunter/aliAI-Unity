using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UFrame.AliDashScopeSDK;

public class NewTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void NewTestScriptSimplePasses()
    {

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        List<DialogueHistoryEntity> his = new List<DialogueHistoryEntity>();
        DashScopeService service = new DashScopeService("sk-a9aa16baa7d74e63bac4c9ba36fb6b0a");
        var inputLine = "������һ��,unity3d������õ�Monobehaviour,�书���ǿ���ͨ������asdf�ĸ�������������ƶ�";
        yield return null;
        service.CallAigcText(his, $"{inputLine}", (res) =>
        {
            var rspobj = JsonUtility.FromJson<AigcTextResponseData>(res);
            if (rspobj != null)
            {
                his.Add(new DialogueHistoryEntity() { bot = rspobj.output.text, user = inputLine });
                Debug.Log("ai:" + rspobj.output.text);
            }
        });
    }
}
