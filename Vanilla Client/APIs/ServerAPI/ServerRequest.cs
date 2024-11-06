// /*
//  *
//  * VanillaClient - ServerRequest.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  * https://zuxi.dev
//  *
//  */

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Vanilla.Config;

namespace Vanilla.APIs.ServerAPI
{
    internal class Server
    {
        protected internal static object SendPostRequestInternal(string EndPoint,
            Dictionary<string, string> SendData = null, Action<bool, string> onFinished = null)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    #region Setup Request Headers & Add Required Values

                    httpClient.DefaultRequestHeaders.Add("User-Agent", SendServerConfig.UA);
                    httpClient.DefaultRequestHeaders.Add("Client-Version", SendServerConfig.Version);
                    httpClient.DefaultRequestHeaders.Add("Client-Agent", SendServerConfig.CA);
                    httpClient.Timeout = TimeSpan.FromMinutes(3);
                    if (SendData == null)
                    {
                        SendData = new Dictionary<string, string>();
                    }

                    //  SendData.Add("Key", GetKey());
                    //  SendData.Add("HWID", GetHWID());
                    //  SendData.Add("CTC", GetCurrentTimeInEpoch().ToString());
                    SendData.Add("api_key", GetInstance().ApiKey);

                    #endregion

                    #region Send Post Request Get Responce Values

                    var PostURI = SendServerConfig.APIBaseEndpoint + EndPoint;
                    var async = httpClient.PostAsync(PostURI, new FormUrlEncodedContent(SendData));
                    async.Wait();
                    var responce = async.Result;
                    var statusCode = responce.StatusCode;

                    #endregion

                    #region If Request is Ok Should We Parse it and How

                    if (statusCode == HttpStatusCode.OK)
                    {
                        httpClient.Dispose();
                        var StringSetup = responce.Content.ReadAsStringAsync();
                        // Dev("ServerAPI", StringSetup.Result);
                        if (StringSetup.Result.Contains("Token"))
                        {
                            ServerResponceHandler.HandlePostRequest(StringSetup.Result);
                        }

                        if (onFinished != null)
                        {
                            onFinished(arg2: StringSetup.Result, arg1: false);
                        }


                        return StringSetup.Result;
                    }

                    #endregion

                    #region If It Is a Bad request Tell The User with the Server Responce

                    else
                    {
                        //Console.ForegroundColor = ConsoleColor.Red;
                        var DES = responce.Content.ReadAsStringAsync();


                        if (DES.Result.Contains("<"))
                        {
                            if (onFinished != null)
                            {
                                onFinished(arg2: null, arg1: true);
                            }

                            Log("Server API", "Server Down Possibly Report To Cypher", ConsoleColor.Red);
                            Dev("Server API", DES.Result);
                            return null;
                        }

                        if (onFinished != null)
                        {
                            onFinished(arg2: DES.Result, arg1: true);
                        }


                        /// var Responce = Json.Decode<Dictionary<string, string>>(DES.Result);
                        // Responce.TryGetValue("message", out var message);
                        Log("Server API",
                            $"Failed to Send Request to {EndPoint} Server responded with (Error: {DES.Result})",
                            ConsoleColor.Red);
                        var Responce = JsonConvert.DeserializeObject<ServerResponce>(DES.Result);
                        Log("Server API",
                            $"Failed to Send Request to {EndPoint} Server responded with (Error: {Responce.message})",
                            ConsoleColor.Red);
                        responce.Dispose();
                        httpClient.Dispose();
                    }

                    httpClient.Dispose();
                    return null;

                    #endregion
                }
            }
            catch (Exception e)
            {
                ExceptionHandler("Server API", e, EndPoint);
                if (onFinished != null)
                {
                    onFinished(arg2: null, arg1: true);
                }

                return null;
            }
        }

        protected internal class SendServerConfig
        {
            protected internal static string APIBaseEndpoint = "https://anarchy.zuxi.dev/api/";
            protected internal static readonly string Version = "V" + "1";
            protected internal static readonly string UA = "VanillaClient" + Version;
            protected internal static readonly string CA = "VanillaClientServerAPI" + Version;
        }
    }
}
