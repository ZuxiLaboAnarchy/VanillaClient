// /*
//  *
//  * VanillaClient - AudioHandler.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using System.Collections.Generic;
using UnityEngine;
using Vanilla.Wrappers;

namespace Vanilla.Utils
{
    internal class AudioHandler
    {
        internal class LoggedRelativeSum
        {
            internal int sum;

            internal float lastSeen;

            internal int totalDetections;
        }

        private static readonly Dictionary<int, List<LoggedRelativeSum>> potentialMaliciousVoicePackets = new();

        internal static void ClearLoggedVoicePackets()
        {
            potentialMaliciousVoicePackets.Clear();
        }

        internal static bool IsVoiceDataBad(int actorId, byte[] voiceData)
        {
            var player = PlayerWrapper.GetPlayerInformationByInstagatorID(actorId);

            if (voiceData.Length <= 8)
            {
                Log("Protections", player + "sent Bad Uspeak Data");
                LogToHud(player + "sent Bad Uspeak Data");
                return true;
            }

            var num = BitConverter.ToInt32(voiceData, 0);

            if (num != actorId)
            {
                return true;
            }

            var num2 = 0;
            for (var i = 8; i < voiceData.Length; i++)
            {
                num2 += voiceData[i];
            }

            if (potentialMaliciousVoicePackets.ContainsKey(actorId))
            {
                var flag = false;
                var flag2 = false;

                for (var j = 0; j < potentialMaliciousVoicePackets[actorId].Count; j++)
                {
                    var num3 = potentialMaliciousVoicePackets[actorId][j].sum - 64;
                    var num4 = potentialMaliciousVoicePackets[actorId][j].sum + 64;

                    if (num2 < num3 || num2 > num4)
                    {
                        continue;
                    }

                    flag = true;

                    if (Time.realtimeSinceStartup - potentialMaliciousVoicePackets[actorId][j].lastSeen < 1f)
                    {
                        if (potentialMaliciousVoicePackets[actorId][j].totalDetections >= 3)
                        {
                            flag2 = true;

                            if (j == potentialMaliciousVoicePackets[actorId].Count - 1)
                            {
                                potentialMaliciousVoicePackets[actorId].Add(new LoggedRelativeSum
                                {
                                    sum = potentialMaliciousVoicePackets[actorId][j].sum + 64,
                                    lastSeen = Time.realtimeSinceStartup,
                                    totalDetections = potentialMaliciousVoicePackets[actorId][j].totalDetections
                                });
                            }

                            var totalDetections = potentialMaliciousVoicePackets[actorId][j].totalDetections;
                            for (var k = 0; k < potentialMaliciousVoicePackets[actorId].Count; k++)
                            {
                                potentialMaliciousVoicePackets[actorId][k].lastSeen = Time.realtimeSinceStartup;
                                potentialMaliciousVoicePackets[actorId][k].totalDetections = totalDetections;
                            }
                        }
                        else
                        {
                            potentialMaliciousVoicePackets[actorId][j].lastSeen = Time.realtimeSinceStartup;
                            potentialMaliciousVoicePackets[actorId][j].totalDetections++;
                        }

                        break;
                    }
                    else
                    {
                        potentialMaliciousVoicePackets[actorId][j].lastSeen = Time.realtimeSinceStartup;
                        potentialMaliciousVoicePackets[actorId][j].totalDetections--;
                    }
                }

                if (!flag)
                {
                    potentialMaliciousVoicePackets[actorId].Add(new LoggedRelativeSum
                    {
                        sum = num2,
                        lastSeen = Time.realtimeSinceStartup,
                        totalDetections = 0
                    });
                }

                if (flag2)
                {
                    return true;
                }
            }
            else
            {
                potentialMaliciousVoicePackets.Add(actorId, new List<LoggedRelativeSum>
                {
                    new()
                    {
                        sum = num2,
                        lastSeen = Time.realtimeSinceStartup,
                        totalDetections = 0
                    }
                });
            }

            return false;
        }
    }
}
