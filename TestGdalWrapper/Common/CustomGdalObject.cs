using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Scanex.Gdal
{
    public abstract class CustomGdalObject
    {
        protected HandleRef swigCPtr;
        protected bool swigCMemOwn;
        protected object swigParentRef;

        protected static object ThisOwn_true() { return null; }
        protected object ThisOwn_false() { return this; }

        public IntPtr Handle {
            get { return swigCPtr.Handle; }
        }

        protected static HandleRef getCPtr(CustomGdalObject obj)
        {
            return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
        }
        protected static IntPtr getCPtrAndDisown(CustomGdalObject obj, object parent)
        {
            if (obj != null)
            {
                obj.swigCMemOwn = false;
                obj.swigParentRef = parent;
                return obj.Handle;
            }
            else
            {
                return IntPtr.Zero;
            }
        }

        internal void NewOwner(object newOwner)
        {
            swigCMemOwn = false;
            swigParentRef = newOwner;
        }

        protected static HandleRef getCPtrAndSetReference(CustomGdalObject obj, object parent)
        {
            if (obj != null)
            {
                obj.swigParentRef = parent;
                return obj.swigCPtr;
            }
            else
            {
                return new HandleRef(null, IntPtr.Zero);
            }
        }

        ~CustomGdalObject()
        {
            Dispose();
        }

        public virtual void Dispose()
        {
            lock (this)
            {
                if (swigCPtr.Handle != IntPtr.Zero && swigCMemOwn)
                {
                    swigCMemOwn = false;
                    DestroyNativeObj();
                }
                swigCPtr = new HandleRef(null, IntPtr.Zero);
                swigParentRef = null;
                GC.SuppressFinalize(this);
            }
        }

        protected abstract void DestroyNativeObj();

        protected void Init(IntPtr cPtr, bool cMemoryOwn, object parent)
        {
            swigCMemOwn = cMemoryOwn;
            swigParentRef = parent;
            swigCPtr = new HandleRef(this, cPtr);
        }
    }
}
