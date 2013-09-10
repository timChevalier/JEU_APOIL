/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.8
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */


using System;
using System.Runtime.InteropServices;

public class AkCallbackSerializer : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal AkCallbackSerializer(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(AkCallbackSerializer obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~AkCallbackSerializer() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          AkSoundEnginePINVOKE.CSharp_delete_AkCallbackSerializer(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public static AKRESULT Init(IntPtr in_pMemory, uint in_uSize) {
    AKRESULT ret = (AKRESULT)AkSoundEnginePINVOKE.CSharp_AkCallbackSerializer_Init(in_pMemory, in_uSize);

    return ret;
  }

  public static void Term() {
    AkSoundEnginePINVOKE.CSharp_AkCallbackSerializer_Term();

  }

  public static IntPtr Lock() { return AkSoundEnginePINVOKE.CSharp_AkCallbackSerializer_Lock(); }

  public static void SetLocalOutput(uint in_uErrorLevel) {
    AkSoundEnginePINVOKE.CSharp_AkCallbackSerializer_SetLocalOutput(in_uErrorLevel);

  }

  public static void Unlock() {
    AkSoundEnginePINVOKE.CSharp_AkCallbackSerializer_Unlock();

  }

  public AkCallbackSerializer() : this(AkSoundEnginePINVOKE.CSharp_new_AkCallbackSerializer(), true) {

  }

}
