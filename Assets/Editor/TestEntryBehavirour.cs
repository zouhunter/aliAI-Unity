//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UFrame.AliDashScopeSDK;

//public class TestEntryBehavirour : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
//        List<DialogueHistoryEntity> his = new List<DialogueHistoryEntity>();
//        DashScopeService service = new DashScopeService("sk-a9aa16baa7d74e63bac4c9ba36fb6b0a");
//        var inputLine = "请生成一段,unity3d程序可用的Monobehaviour,其功能是可以通过输入asdf四个键，控制相机移动";
//        var rsp = service.CallAigcText(his, $"{inputLine}");
//        var rspobj = JsonUtility.FromJson<AigcTextResponseData>(rsp.Result);
//        if (rspobj != null)
//        {
//            his.Add(new DialogueHistoryEntity() { bot = rspobj.output.text, user = inputLine });
//            print("ai:" + rspobj.output.text);
//        }
//    }

//}
