using System;
using System.Runtime.InteropServices;

internal class DiscordRPC
{
    internal struct EventHandlers
    {
        internal ReadyCallback readyCallback;
        internal DisconnectedCallback disconnectedCallback;
        internal ErrorCallback errorCallback;
        internal JoinCallback joinCallback;
        internal SpectateCallback spectateCallback;
        internal RequestCallback requestCallback;
    }

    [Serializable]
    internal struct RichPresence
    {
        internal string state;
        internal string details;
        internal long startTimestamp;
        internal long endTimestamp;
        internal string largeImageKey;
        internal string largeImageText;
        internal string smallImageKey;
        internal string smallImageText;
        internal string partyId;
        internal int partySize;
        internal int partyMax;
        internal string matchSecret;
        internal string joinSecret;
        internal string spectateSecret;
        internal bool instance;
        internal string buttons;
    }

    [Serializable]
    internal struct JoinRequest
    {
#pragma warning disable CS0649
        internal string userId;
#pragma warning restore CS0649
#pragma warning disable CS0649
        internal string username;
#pragma warning restore CS0649
#pragma warning disable CS0649
        internal string discriminator;
#pragma warning restore CS0649
#pragma warning disable CS0649
        internal string avatar;
#pragma warning restore CS0649
    }

    internal enum Reply
    {
        No,
        Yes,
        Ignore
    }


    [DllImport("UserLibs/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "Discord_Initialize")]
    protected internal static extern void Initialize(string applicationId, ref EventHandlers handlers,
        bool autoRegister, string optionalSteamId);

    [DllImport("UserLibs/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "Discord_Shutdown")]
    protected internal static extern void Shutdown();

    [DllImport("UserLibs/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "Discord_RunCallbacks")]
    protected internal static extern void RunCallbacks();

    [DllImport("UserLibs/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "Discord_UpdatePresence")]
    protected internal static extern void UpdatePresence(ref RichPresence presence);

    [DllImport("UserLibs/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl,
        EntryPoint = "Discord_ClearPresence")]
    protected internal static extern void ClearPresence();

    [DllImport("UserLibs/discord-rpc.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "Discord_Respond")]
    protected internal static extern void Respond(string userId, Reply reply);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void ReadyCallback();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void DisconnectedCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void ErrorCallback(int errorCode, string message);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void JoinCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void SpectateCallback(string secret);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    protected internal delegate void RequestCallback(ref JoinRequest request);
}