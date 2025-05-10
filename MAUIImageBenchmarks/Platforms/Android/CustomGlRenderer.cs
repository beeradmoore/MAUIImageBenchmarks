using System.Diagnostics;
using System.Text;
using Android.Opengl;
using Javax.Microedition.Khronos.Opengles;

namespace MAUIImageBenchmarks;

public class CustomGlRenderer : Java.Lang.Object, GLSurfaceView.IRenderer
{
    public void OnDrawFrame(IGL10? gl)
    {
        
    }

    public void OnSurfaceChanged(IGL10? gl, int width, int height)
    {
        
    }
    
    public void OnSurfaceCreated(IGL10? gl, Javax.Microedition.Khronos.Egl.EGLConfig? config)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"GPU Vendor: {gl.GlGetString(Android.Opengl.GLES20.GlVendor)}");
        stringBuilder.AppendLine($"GPU Renderer: {gl.GlGetString(Android.Opengl.GLES20.GlRenderer)}");
        stringBuilder.AppendLine($"GPU Version: {gl.GlGetString(Android.Opengl.GLES20.GlVersion)}");
        var dsa = stringBuilder.ToString();
        Debugger.Break();
    }
}