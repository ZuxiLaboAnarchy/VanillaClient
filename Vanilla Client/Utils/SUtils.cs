// /*
//  *
//  * VanillaClient - SUtils.cs
//  * Copyright 2023 - 2024 Zuxi and contributors
//  *
//  */

using UnityEngine;

namespace Vanilla.Utils
{
    internal class SerializationUtils
    {
        private static readonly Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter
            binaryFormatterIl2Cpp = new();

        private static readonly System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatterMono =
            new();

        internal static byte[] ToByteArray(Il2CppSystem.Object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var memoryStream = new Il2CppSystem.IO.MemoryStream();
            binaryFormatterIl2Cpp.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }

        internal static byte[] ToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var memoryStream = new System.IO.MemoryStream();
            binaryFormatterMono.Serialize(memoryStream, obj);
            return memoryStream.ToArray();
        }

        internal static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
            {
                return default;
            }

            using var serializationStream = new System.IO.MemoryStream(data);
            var obj = binaryFormatterMono.Deserialize(serializationStream);
            return (T)obj;
        }

        internal static T IL2CPPFromByteArray<T>(byte[] data)
        {
            if (data == null)
            {
                return default;
            }

            var serializationStream = new Il2CppSystem.IO.MemoryStream(data);
            object obj = binaryFormatterIl2Cpp.Deserialize(serializationStream);
            return (T)obj;
        }

        internal static T FromIL2CPPToManaged<T>(Il2CppSystem.Object obj)
        {
            return FromByteArray<T>(ToByteArray(obj));
        }

        internal static T FromManagedToIL2CPP<T>(object obj)
        {
            return IL2CPPFromByteArray<T>(ToByteArray(obj));
        }

        internal static byte ReadByte(ref byte[] buffer, int index)
        {
            if (index < 0)
            {
                return 0;
            }

            if (buffer == null)
            {
                return 0;
            }

            return buffer[index];
        }

        internal static byte[] ShortToBytes(short value)
        {
            return BitConverter.GetBytes(value);
        }

        internal static short ReadShort(ref byte[] buffer, int index)
        {
            if (index < 0)
            {
                return 0;
            }

            if (buffer == null)
            {
                return 0;
            }

            if (buffer.Length < index + 2)
            {
                return 0;
            }

            return BitConverter.ToInt16(buffer, index);
        }

        internal static byte[] IntToBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }

        internal static int ReadInt(ref byte[] buffer, int index)
        {
            if (index < 0)
            {
                return 0;
            }

            if (buffer == null)
            {
                return 0;
            }

            if (buffer.Length < index + 4)
            {
                return 0;
            }

            return BitConverter.ToInt32(buffer, index);
        }

        internal static byte[] FloatToBytes(float value)
        {
            return BitConverter.GetBytes(value);
        }

        internal static float ReadFloat(ref byte[] buffer, int index)
        {
            if (index < 0)
            {
                return 0f;
            }

            if (buffer == null)
            {
                return 0f;
            }

            if (buffer.Length < index + 4)
            {
                return 0f;
            }

            return BitConverter.ToSingle(buffer, index);
        }

        internal static byte[] Vector3ToBytes(Vector3 vector3)
        {
            var array = new byte[12];
            Buffer.BlockCopy(BitConverter.GetBytes(vector3.x), 0, array, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(vector3.y), 0, array, 4, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(vector3.z), 0, array, 8, 4);
            return array;
        }

        internal static Vector3 ReadVector3(ref byte[] buffer, int index)
        {
            if (index < 0)
            {
                return Vector3.zero;
            }

            if (buffer == null)
            {
                return Vector3.zero;
            }

            if (buffer.Length < index + 12)
            {
                return Vector3.zero;
            }

            var x = BitConverter.ToSingle(buffer, index);
            var y = BitConverter.ToSingle(buffer, index + 4);
            var z = BitConverter.ToSingle(buffer, index + 8);
            return new Vector3(x, y, z);
        }
    }
}
