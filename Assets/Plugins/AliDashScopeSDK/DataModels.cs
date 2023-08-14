using System.Collections.Generic;

namespace AliDashScopeSDK
{
    /// <summary>
    /// 文本聊天返回实体
    /// </summary>
    [System.Serializable]
    public class AigcTextResponseData
    {
        public OutputData output;
        public UsageData usage;
        public string request_id;
    }

    [System.Serializable]
    public class OutputData
    {
        public string finish_reason;
        public string text;
    }

    [System.Serializable]
    public class UsageData
    {
        public int output_tokens;
        public int input_tokens;
    }

    /// <summary>
    /// 聊天对话数据实体
    /// </summary>
    [System.Serializable]
    public class DialogueHistoryEntity
    {
        /// <summary>
        /// 用户的发言
        /// </summary>
        public string user;
        /// <summary>
        /// 机器人的发言
        /// </summary>
        public string bot;
    }

    [System.Serializable]
    public struct DashScopeRequestInfo
    {
        public string model;
        public string bot;
        public RequestInput input;
        public Parameters parameters;
    }

    [System.Serializable]
    public struct RequestInput
    {
        public string prompt;
        public List<DialogueHistoryEntity> history;
    }

    [System.Serializable]
    public class Parameters
    {
        public float top_p = 0.8f;
        public float top_k = 50;
        public int seed = 65535;
        public bool enable_search;
    }
}
