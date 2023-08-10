﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UFrame.AliDashScopeSDK
{
    /// <summary>
    /// 阿里通义千问API服务
    /// </summary>
    public class DashScopeService
    {
        /// <summary>
        /// 返回DashScopeService实例
        /// </summary>
        /// <param name="apiKey"></param>
        public DashScopeService(string apiKey)
        {
            if (!String.IsNullOrEmpty(apiKey))
            {
                DashScopeConfig.SetMyKey(apiKey);
            }
        }

        /// <summary>
        /// 调用通义千问7B模型的文本对话接口
        /// </summary>
        /// <param name="histoy">对话历史</param>
        /// <param name="inputPrompt">提示词</param>
        public void CallAigcText(List<DialogueHistoryEntity> histoy, string inputPrompt,System.Action<string> callback)
        {
            HttpWebRequest req = WebRequest.CreateHttp(DashScopeConfig.AigcTextApiUrl);
            req.Method = "POST";
            req.ContentType = "application/json";
            req.Headers["Authorization"] = $"Bearer {DashScopeConfig.Key}";
            req.ContinueTimeout = 10 * 1000;
            req.Timeout = 10 * 1000;
            req.AllowWriteStreamBuffering = true;
            var requestData = new
            {
                model = "qwen-v1",
                input = new
                {
                    prompt = inputPrompt,
                    history = histoy
                },
                parameters = new { }
            };
            var requestDataJson = LitJson.JsonMapper.ToJson(requestData);
            UnityEngine.Debug.Log(requestDataJson);
            byte[] datas = Encoding.UTF8.GetBytes(requestDataJson);
            req.ContentLength = datas.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(datas, 0, datas.Length);
                reqStream.Close();
            }
            req.BeginGetResponse((c)=> {
                var response = req.EndGetResponse(c);
                string textResult = null;
                using (Stream receiveStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        textResult = readStream.ReadToEnd();
                        response.Close();
                        readStream.Close();
                    }
                }
                callback?.Invoke(textResult);
            }, this);
        }

        /// <summary>
        /// 调用文本向量API
        /// </summary>
        /// <param name="inputText">文本数组</param>
        /// <returns></returns>
        public async Task<string> CallTextEmbedding(string[] inputText)
        {
            string result = "";

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {DashScopeConfig.Key}");
                httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

                var requestData = new
                {
                    model = "text-embedding-v1",
                    input = new
                    {
                        texts = inputText
                    },
                    parameters = new
                    {
                        text_type = "query"
                    }
                };

                var requestDataJson = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
                var content = new StringContent(requestDataJson, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(DashScopeConfig.EmbeddingApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new Exception(($"请求失败：{response.StatusCode}"));
                }
            }

            return result;
        }

        /// <summary>
        /// 计算两个向量的相似度
        /// </summary>
        /// <param name="vectorA">向量A</param>
        /// <param name="vectorB">向量B</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public float CalculateCosineSimilarity(float[] vectorA, float[] vectorB)
        {
            if (vectorA.Length != vectorB.Length)
                throw new ArgumentException("向量长度不一致");

            var dotProduct = 0.0;
            var normA = 0.0;
            var normB = 0.0;

            for (int i = 0; i < vectorA.Length; i++)
            {
                dotProduct += vectorA[i] * vectorB[i];
                normA += Math.Pow(vectorA[i], 2);
                normB += Math.Pow(vectorB[i], 2);
            }

            //余弦相似度公式计算两个向量之间的相似度值
            var similarity = (float)(dotProduct / (Math.Sqrt(normA) * Math.Sqrt(normB)));

            return similarity;
        }
    }
}
