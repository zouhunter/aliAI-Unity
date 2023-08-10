namespace UFrame.AliDashScopeSDK
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
}
