using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.Text;

namespace MediaSoupWrapper
{
    public enum DataState
    {
        kConnecting,
        kOpen,
        kClosing,
        kClosed,
    }

    public class MediaSoup
    {
#if UNITY_ANDROID
        public const string DllName = "mediasoupclient.dll";
#elif UNITY_IOS
        public const string DllName = "mediasoupclient.dll";
#else
        public const string DllName = "mediasoupclient.dll";
#endif
        public static StringBuilder stringBuilder;

        public static void Init(int capacity = 0)
        {
            stringBuilder = new StringBuilder(capacity);
            Initialize();
        }

        public static string GetVersion()
        {
            if (stringBuilder == null || stringBuilder.Capacity == 0)
            {
                Debug.LogWarning("StringBuilder is null! Set capacity for buffer");
                return "";
            }

            Version(stringBuilder, stringBuilder.Capacity);
            string versionText = stringBuilder.ToString();
            stringBuilder.Clear();
            return versionText;
        }

        [DllImport(DllName)]
        public static extern void Initialize();

        [DllImport(DllName)]
        public static extern IntPtr MakeDevice();

        [DllImport(DllName)]
        public static extern void DeleteDevice(IntPtr device);

        [DllImport(DllName)]
        public static extern IntPtr GetRtpCapabilities(IntPtr device);

        [DllImport(DllName)]
        public static extern IntPtr GetSctpCapabilities(IntPtr device);

        [DllImport(DllName)]
        public static extern void GetRtpCapabilitiesByString(IntPtr device, StringBuilder container, int length);

        [DllImport(DllName)]
        public static extern void GetSctpCapabilitiesByString(IntPtr device, StringBuilder container, int length);

        [DllImport(DllName)]
        public static extern bool CanProduce(IntPtr device, StringBuilder type, int typeLength);

        [DllImport(DllName)]
        public static extern bool IsLoaded(IntPtr device);

        [DllImport(DllName)]
        public static extern void Load(IntPtr device, IntPtr rtpCapabilities, IntPtr peerConnectionsOptions = default);

        [DllImport(DllName)]
        public static extern void LoadByGetRtp(IntPtr device, StringBuilder rtpCapabilities, int rtpLength ,IntPtr peerConnectionsOptions = default);

        [DllImport(DllName)]
        public static extern void CleanUp();

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void Version(StringBuilder builder, int capacity);

        [DllImport(DllName)]
        public static extern IntPtr CreateSendTransport(
            IntPtr device,
            IntPtr listener,
            StringBuilder id,
            int idLength,
            IntPtr iceParameters,
            IntPtr iceCandidates,
            IntPtr dtlsParameters, 
            IntPtr sctpParameters, 
            IntPtr peerConnectionOptions,
            IntPtr appData);
        
        [DllImport(DllName)]
        public static extern IntPtr CreateRecvTransport(
            IntPtr device, 
            IntPtr listener, 
            StringBuilder id,
            int idLength,
            IntPtr iceParameters,
            IntPtr iceCandidates,
            IntPtr dtlsParameters, 
            IntPtr sctpParameters, 
            IntPtr peerConnectionOptions, 
            IntPtr appData);

        [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void GetJsonString(IntPtr jsonObject, StringBuilder text, int size);

        [DllImport(DllName)]
        public static extern IntPtr MakeJsonObject(StringBuilder data, int dataSize);

        [DllImport(DllName)]
        public static extern void DeleteJsonObject(IntPtr jsonPtr);

        #region TEST
        [DllImport(DllName)]
        public static extern IntPtr TestId();

        [DllImport(DllName)]
        public static extern int TestEnumInput(DataState state);

        [DllImport(DllName, CallingConvention = CallingConvention.StdCall)]
        public static extern void TestGetJsonString(StringBuilder builder, int capacity);

        [DllImport(DllName)]
        public static extern uint TestUint8(int testCase);

        [DllImport(DllName)]
        public static extern IntPtr MakeBroadcaster();

        [DllImport(DllName)]
        public static extern void SaveSendTransport(IntPtr broadcaster, IntPtr sendTransport);

        [DllImport(DllName)]
        public static extern void SaveRecvTransport(IntPtr broadcaster, IntPtr recvTransport);
        #endregion
    }
}