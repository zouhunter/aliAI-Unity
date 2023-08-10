﻿namespace UFrame.AliDashScopeSDK
{
    /// <summary>
    /// 阿里DashScope 配置
    /// </summary>
    public static class DashScopeConfig
    {
        private static string _key;
        private static string _apiRoot;
        private static string _aigcTextApiUrl;
        private static string _embeddingApiUrl;

        static DashScopeConfig()
        {
            _key = "sk-???";
            _apiRoot = "https://dashscope.aliyuncs.com/api/v1/services/";                       
            _aigcTextApiUrl = $"{_apiRoot}aigc/text-generation/generation";
            _embeddingApiUrl = $"{_apiRoot}embeddings/text-embedding/text-embedding";
        }


        /// <summary>
        /// 设置你自己的API KEY
        /// </summary>
        /// <param name="newKey">API KEY</param>
        public static void SetMyKey(string newKey)
        {
            _key = newKey;
        }

        /// <summary>
        ///  阿里DashScope API KEY
        /// </summary>
        public static string Key { get { return _key; } }

        /// <summary>
        /// 文本生成及聊天生成 API URL
        /// </summary>
        public static string AigcTextApiUrl { get { return _aigcTextApiUrl; } }

        /// <summary>
        /// 文本向量 API URL
        /// </summary>
        public static string EmbeddingApiUrl { get { return _embeddingApiUrl; } }
    }
}