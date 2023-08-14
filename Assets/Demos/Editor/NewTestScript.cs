using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using AliDashScopeSDK;

public class NewTestScript
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [Test]
    public void TestDashScope()
    {
        List<DialogueHistoryEntity> his = new List<DialogueHistoryEntity>();
        var apiKeyAsset = Resources.Load<TextAsset>("apikey");
        DashScopeService service = new DashScopeService(apiKeyAsset.text);
        var inputLine = "请生成一段,unity3d程序可用的Monobehaviour,其功能是可以通过输入asdf四个键，控制相机移动";
        service.CallAigcTextAsync(his, $"{inputLine}", (res,c) =>
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
