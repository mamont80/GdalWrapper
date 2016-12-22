using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Scanex.Gdal;

namespace Scanex.Gdal
{
    public static class MarshalUtils
    {
        public static Encoding DefaultEncoding = Encoding.UTF8;

        internal static byte[] StringToUtf8Bytes(string str)
        {
            if (str == null)
                return null;

            int bytecount = System.Text.Encoding.UTF8.GetMaxByteCount(str.Length);
            byte[] bytes = new byte[bytecount + 1];
            System.Text.Encoding.UTF8.GetBytes(str, 0, str.Length, bytes, 0);
            return bytes;
        }


        public static string PtrToStringEncoding(IntPtr pNativeData, Encoding enc)
        {
            if (pNativeData == IntPtr.Zero)
                return null;

            int length = Marshal.PtrToStringAnsi(pNativeData).Length;
            byte[] strbuf = new byte[length];
            Marshal.Copy(pNativeData, strbuf, 0, length);

            string text;
            if (enc != null)
                text = enc.GetString(strbuf);
            else text = Encoding.Default.GetString(strbuf);
            return text;
        }

        public class StringListExport : IDisposable
        {
            public readonly IntPtr[] Pointer;

            public StringListExport(string[] ar, Encoding encoding):this(ar)
            {

            }

            public StringListExport(string[] ar)
            {
                if (ar == null)
                {
                    Pointer = null;
                    return;
                }
                Pointer = new IntPtr[ar.Length + 1];
                for (int cx = 0; cx < ar.Length; cx++)
                {
                    Pointer[cx] = System.Runtime.InteropServices.Marshal.StringToHGlobalAnsi(ar[cx]);
                }
                Pointer[ar.Length] = IntPtr.Zero;
            }

            public virtual void Dispose()
            {
                if (Pointer != null)
                {
                    for (int cx = 0; cx < Pointer.Length - 1; cx++)
                    {
                        System.Runtime.InteropServices.Marshal.FreeHGlobal(Pointer[cx]);
                    }
                }
                GC.SuppressFinalize(this);
            }
        }

        public class BytesExport : IDisposable
        {
            public IntPtr Pointer;
            public int Length;
            public BytesExport(byte[] bytes)
            {
                if (bytes == null)
                {
                    Pointer = IntPtr.Zero;
                    return;
                }
                Length = bytes.Length;
                Pointer = Marshal.AllocHGlobal(Length);
                Marshal.Copy(bytes, 0, Pointer, Length);
            }

            public void Dispose()
            {
                if (Pointer != IntPtr.Zero) Marshal.FreeHGlobal(Pointer);
                GC.SuppressFinalize(this);
            }
        }

        public class DoublesExport : IDisposable
        {
            public IntPtr Pointer;
            public int Length;
            public DoublesExport(double[] bytes)
            {
                if (bytes == null)
                {
                    Pointer = IntPtr.Zero;
                    return;
                }
                Length = bytes.Length;
                Pointer = Marshal.AllocHGlobal(Length);
                Marshal.Copy(bytes, 0, Pointer, Length);
            }

            public void Dispose()
            {
                if (Pointer != IntPtr.Zero) Marshal.FreeHGlobal(Pointer);
                GC.SuppressFinalize(this);
            }
        }

        public class Int64Export : IDisposable
        {
            public IntPtr Pointer;
            public int Length;
            public Int64Export(Int64[] bytes)
            {
                if (bytes == null)
                {
                    Pointer = IntPtr.Zero;
                    return;
                }
                Length = bytes.Length;
                Pointer = Marshal.AllocHGlobal(Length);
                Marshal.Copy(bytes, 0, Pointer, Length);
            }

            public void Dispose()
            {
                if(Pointer != IntPtr.Zero) Marshal.FreeHGlobal(Pointer);
                GC.SuppressFinalize(this);
            }
        }

        public class IntExport : IDisposable
        {
            public IntPtr Pointer;
            public int Length;
            public IntExport(int[] bytes)
            {
                if (bytes == null)
                {
                    Pointer = IntPtr.Zero;
                    return;
                }
                Length = bytes.Length;
                Pointer = Marshal.AllocHGlobal(Length);
                Marshal.Copy(bytes, 0, Pointer, Length);
            }

            public void Dispose()
            {
                if (Pointer != IntPtr.Zero) Marshal.FreeHGlobal(Pointer);
                GC.SuppressFinalize(this);
            }
        }

        public class StringExport : IDisposable
        {

            public IntPtr Pointer;
            public int Length;

            public StringExport(string value) : this(value, DefaultEncoding)
            {
            }

            public StringExport(string value, Encoding encoding)
            {
                if (encoding == null) encoding = DefaultEncoding;
                if (value == null)
                {
                    Length = 0;
                    Pointer = IntPtr.Zero;
                    return;
                }
                int bytecount = encoding.GetMaxByteCount(value.Length)+1;
                byte[] buf = new byte[bytecount];
                int cnt = encoding.GetBytes(value, 0, value.Length, buf, 0);
                buf[cnt] = 0;
                Length = cnt + 1;
                Pointer = Marshal.AllocHGlobal(Length);
                Marshal.Copy(buf, 0, Pointer, Length);
            }

            public void Dispose()
            {
                if (Pointer != IntPtr.Zero) Marshal.FreeHGlobal(Pointer);
                GC.SuppressFinalize(this);
            }
        }




















    }
}
