using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SkiaSharp;

namespace MAUIImageBenchmarks;

public partial class DeviceDetailsModel : ObservableObject
{
    WeakReference<DeviceDetails> _weakPage;
    
    public string Details { get; private set; } = string.Empty;

    public DeviceDetailsModel(DeviceDetails page)
    {
        _weakPage = new WeakReference<DeviceDetails>(page);
        
        //AdvSimd.Arm64.IsSupported, and System.Numerics.Vector.IsHardwareAccelerated.
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("DeviceInfo");
        stringBuilder.AppendLine($"DeviceType: {DeviceInfo.DeviceType}");
        stringBuilder.AppendLine($"Idiom: {DeviceInfo.Idiom}");
        stringBuilder.AppendLine($"Manufacturer: {DeviceInfo.Manufacturer}");
        stringBuilder.AppendLine($"Model: {DeviceInfo.Model}");
        stringBuilder.AppendLine($"Name: {DeviceInfo.Name}");
        stringBuilder.AppendLine($"Platform: {DeviceInfo.Platform}");
        stringBuilder.AppendLine($"VersionString: {DeviceInfo.VersionString}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Environment");
        stringBuilder.AppendLine($"Is64BitOperatingSystem: {Environment.Is64BitOperatingSystem}");
        stringBuilder.AppendLine($"Is64BitProcess: {Environment.Is64BitProcess}");
        stringBuilder.AppendLine($"IsPrivilegedProcess: {Environment.IsPrivilegedProcess}");
        stringBuilder.AppendLine($"OSVersion: {Environment.OSVersion}");
        stringBuilder.AppendLine($"ProcessorCount: {Environment.ProcessorCount}");
        stringBuilder.AppendLine($"Version: {Environment.Version}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("RuntimeInformation");
        stringBuilder.AppendLine($"FrameworkDescription: {RuntimeInformation.FrameworkDescription}");
        stringBuilder.AppendLine($"OSArchitecture: {RuntimeInformation.OSArchitecture}");
        stringBuilder.AppendLine($"OSDescription: {RuntimeInformation.OSDescription}");
        stringBuilder.AppendLine($"ProcessArchitecture: {RuntimeInformation.ProcessArchitecture}");
        stringBuilder.AppendLine($"RuntimeIdentifier: {RuntimeInformation.RuntimeIdentifier}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Intrinsics");
        stringBuilder.AppendLine($"System.Numerics.Vector.IsHardwareAccelerated: {System.Numerics.Vector.IsHardwareAccelerated}");
        stringBuilder.AppendLine($"Vector64.IsHardwareAccelerated: {System.Runtime.Intrinsics.Vector64.IsHardwareAccelerated}");
        stringBuilder.AppendLine($"Vector128.IsHardwareAccelerated: {System.Runtime.Intrinsics.Vector128.IsHardwareAccelerated}");
        stringBuilder.AppendLine($"Vector256.IsHardwareAccelerated: {System.Runtime.Intrinsics.Vector256.IsHardwareAccelerated}");
        stringBuilder.AppendLine($"Vector512.IsHardwareAccelerated: {System.Runtime.Intrinsics.Vector512.IsHardwareAccelerated}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Intrinsics.Arm");
        stringBuilder.AppendLine($"AdvSimd.IsSupported: {System.Runtime.Intrinsics.Arm.AdvSimd.IsSupported}");
        stringBuilder.AppendLine($"AdvSimd.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.AdvSimd.Arm64.IsSupported}");
        stringBuilder.AppendLine($"Aes.IsSupported: {System.Runtime.Intrinsics.Arm.Aes.IsSupported}");
        stringBuilder.AppendLine($"Ae.Arm64s.IsSupported: {System.Runtime.Intrinsics.Arm.Aes.Arm64.IsSupported}");
        stringBuilder.AppendLine($"ArmBase.IsSupported: {System.Runtime.Intrinsics.Arm.ArmBase.IsSupported}");
        stringBuilder.AppendLine($"ArmBase.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.ArmBase.Arm64.IsSupported}");
        stringBuilder.AppendLine($"Crc32.IsSupported: {System.Runtime.Intrinsics.Arm.Crc32.IsSupported}");
        stringBuilder.AppendLine($"Crc32.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.Crc32.Arm64.IsSupported}");
        stringBuilder.AppendLine($"Dp.IsSupported: {System.Runtime.Intrinsics.Arm.Dp.IsSupported}");
        stringBuilder.AppendLine($"Dp.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.Dp.Arm64.IsSupported}");
        stringBuilder.AppendLine($"Rdm.IsSupported: {System.Runtime.Intrinsics.Arm.Rdm.IsSupported}");
        stringBuilder.AppendLine($"Rdm.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.Rdm.Arm64.IsSupported}");
        stringBuilder.AppendLine($"Sha1.IsSupported: {System.Runtime.Intrinsics.Arm.Sha1.IsSupported}");
        stringBuilder.AppendLine($"Sha1.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.Sha1.Arm64.IsSupported}");
        stringBuilder.AppendLine($"Sha256.IsSupported: {System.Runtime.Intrinsics.Arm.Sha256.IsSupported}");
        stringBuilder.AppendLine($"Sha256.Arm64.IsSupported: {System.Runtime.Intrinsics.Arm.Sha256.Arm64.IsSupported}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Intrinsics.X86");
        stringBuilder.AppendLine($"Aes.IsSupported: {System.Runtime.Intrinsics.X86.Aes.IsSupported}");
        stringBuilder.AppendLine($"Aes.X64.IsSupported: {System.Runtime.Intrinsics.X86.Aes.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx.IsSupported: {System.Runtime.Intrinsics.X86.Avx.IsSupported}");
        stringBuilder.AppendLine($"Avx.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx2.IsSupported: {System.Runtime.Intrinsics.X86.Avx2.IsSupported}");
        stringBuilder.AppendLine($"Avx2.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx2.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx10v1.IsSupported: {System.Runtime.Intrinsics.X86.Avx10v1.IsSupported}");
        stringBuilder.AppendLine($"Avx10v1.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx10v1.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx512BW.IsSupported: {System.Runtime.Intrinsics.X86.Avx512BW.IsSupported}");
        stringBuilder.AppendLine($"Avx512BW.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx512BW.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx512CD.IsSupported: {System.Runtime.Intrinsics.X86.Avx512CD.IsSupported}");
        stringBuilder.AppendLine($"Avx512CD.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx512CD.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx512DQ.IsSupported: {System.Runtime.Intrinsics.X86.Avx512DQ.IsSupported}");
        stringBuilder.AppendLine($"Avx512DQ.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx512DQ.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx512F.IsSupported: {System.Runtime.Intrinsics.X86.Avx512F.IsSupported}");
        stringBuilder.AppendLine($"Avx512F.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx512F.X64.IsSupported}");
        stringBuilder.AppendLine($"Avx512Vbmi.IsSupported: {System.Runtime.Intrinsics.X86.Avx512Vbmi.IsSupported}");
        stringBuilder.AppendLine($"Avx512Vbmi.X64.IsSupported: {System.Runtime.Intrinsics.X86.Avx512Vbmi.X64.IsSupported}");
        stringBuilder.AppendLine($"AvxVnni.IsSupported: {System.Runtime.Intrinsics.X86.AvxVnni.IsSupported}");
        stringBuilder.AppendLine($"AvxVnni.X64.IsSupported: {System.Runtime.Intrinsics.X86.AvxVnni.X64.IsSupported}");
        stringBuilder.AppendLine($"Bmi1.IsSupported: {System.Runtime.Intrinsics.X86.Bmi1.IsSupported}");
        stringBuilder.AppendLine($"Bmi1.X64.IsSupported: {System.Runtime.Intrinsics.X86.Bmi1.X64.IsSupported}");
        stringBuilder.AppendLine($"Bmi2.IsSupported: {System.Runtime.Intrinsics.X86.Bmi2.IsSupported}");
        stringBuilder.AppendLine($"Bmi2.X64.IsSupported: {System.Runtime.Intrinsics.X86.Bmi2.X64.IsSupported}");
        stringBuilder.AppendLine($"Fma.IsSupported: {System.Runtime.Intrinsics.X86.Fma.IsSupported}");
        stringBuilder.AppendLine($"Fma.X64.IsSupported: {System.Runtime.Intrinsics.X86.Fma.X64.IsSupported}");
        stringBuilder.AppendLine($"Lzcnt.IsSupported: {System.Runtime.Intrinsics.X86.Lzcnt.IsSupported}");
        stringBuilder.AppendLine($"Lzcnt.X64.IsSupported: {System.Runtime.Intrinsics.X86.Lzcnt.X64.IsSupported}");
        stringBuilder.AppendLine($"Pclmulqdq.IsSupported: {System.Runtime.Intrinsics.X86.Pclmulqdq.IsSupported}");
        stringBuilder.AppendLine($"Pclmulqdq.X64.IsSupported: {System.Runtime.Intrinsics.X86.Pclmulqdq.X64.IsSupported}");
        stringBuilder.AppendLine($"Popcnt.IsSupported: {System.Runtime.Intrinsics.X86.Popcnt.IsSupported}");
        stringBuilder.AppendLine($"Popcnt.X64.IsSupported: {System.Runtime.Intrinsics.X86.Popcnt.X64.IsSupported}");
        stringBuilder.AppendLine($"Sse.IsSupported: {System.Runtime.Intrinsics.X86.Sse.IsSupported}");
        stringBuilder.AppendLine($"Sse.X64.IsSupported: {System.Runtime.Intrinsics.X86.Sse.X64.IsSupported}");
        stringBuilder.AppendLine($"Sse2.IsSupported: {System.Runtime.Intrinsics.X86.Sse2.IsSupported}");
        stringBuilder.AppendLine($"Sse2.X64.IsSupported: {System.Runtime.Intrinsics.X86.Sse2.X64.IsSupported}");
        stringBuilder.AppendLine($"Sse3.IsSupported: {System.Runtime.Intrinsics.X86.Sse3.IsSupported}");
        stringBuilder.AppendLine($"Sse3.X64.IsSupported: {System.Runtime.Intrinsics.X86.Sse3.X64.IsSupported}");
        stringBuilder.AppendLine($"Sse41.IsSupported: {System.Runtime.Intrinsics.X86.Sse41.IsSupported}");
        stringBuilder.AppendLine($"Sse41.X64.IsSupported: {System.Runtime.Intrinsics.X86.Sse41.X64.IsSupported}");
        stringBuilder.AppendLine($"Sse42.IsSupported: {System.Runtime.Intrinsics.X86.Sse42.IsSupported}");
        stringBuilder.AppendLine($"Sse42.X64.IsSupported: {System.Runtime.Intrinsics.X86.Sse42.X64.IsSupported}");
        stringBuilder.AppendLine($"Ssse3.IsSupported: {System.Runtime.Intrinsics.X86.Ssse3.IsSupported}");
        stringBuilder.AppendLine($"Ssse3.X64.IsSupported: {System.Runtime.Intrinsics.X86.Ssse3.X64.IsSupported}");
        stringBuilder.AppendLine($"X86Base.IsSupported: {System.Runtime.Intrinsics.X86.X86Base.IsSupported}");
        stringBuilder.AppendLine($"X86Base.X64.IsSupported: {System.Runtime.Intrinsics.X86.X86Base.X64.IsSupported}");
        stringBuilder.AppendLine($"X86Serialize.IsSupported: {System.Runtime.Intrinsics.X86.X86Serialize.IsSupported}");
        stringBuilder.AppendLine($"X86Serialize.X64.IsSupported: {System.Runtime.Intrinsics.X86.X86Serialize.X64.IsSupported}");
        stringBuilder.AppendLine();
        stringBuilder.AppendLine("Intrinsics.Wasm");
        stringBuilder.AppendLine($"PackedSimd.IsSupported: {System.Runtime.Intrinsics.Wasm.PackedSimd.IsSupported}");
        stringBuilder.AppendLine();
        
        #if __ANDROID__
        stringBuilder.AppendLine("Android");
        try
        {
            stringBuilder.AppendLine("CPU Info: /proc/cpuinfo");

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
#pragma warning disable CA1416
                var cpuInfoPath = Java.Nio.FileNio.Paths.Get("/proc/cpuinfo");
                if (cpuInfoPath is not null)
                {
                    var cpuInfoBytes = Java.Nio.FileNio.Files.ReadAllBytes(cpuInfoPath);
                    if (cpuInfoBytes is not null)
                    {
                        var cpuInfoString = new Java.Lang.String(cpuInfoBytes);
                        stringBuilder.AppendLine(cpuInfoString.ToString());
                    }
                }
#pragma warning restore CA1416
            }
        }
        catch (Exception err)
        {
            stringBuilder.AppendLine($"Error: Unable to fetch CPU info ({err.Message})");
        }
        
        try
        {
            stringBuilder.AppendLine("CPU Info: /proc/gpuinfo");
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
#pragma warning disable CA1416
                var gpuInfoPath = Java.Nio.FileNio.Paths.Get("/proc/gpuinfo");
                if (gpuInfoPath is not null)
                {
                    var gpuInfoBytes = Java.Nio.FileNio.Files.ReadAllBytes(gpuInfoPath);
                    if (gpuInfoBytes is not null)
                    {
                        var gpuInfoString = new Java.Lang.String(gpuInfoBytes);
                        stringBuilder.AppendLine(gpuInfoString.ToString());
                    }
                }
#pragma warning restore CA1416
            }
        }
        catch (Exception err)
        {
            stringBuilder.AppendLine($"Error: Unable to fetch GPU info ({err.Message})");
        }
        
        
        var runtime = Java.Lang.Runtime.GetRuntime();
        if (runtime is not null)
        {
            stringBuilder.AppendLine($"Runtime.AvailableProcessors: {runtime.AvailableProcessors()}");
            stringBuilder.AppendLine($"Runtime.TotalMemory: {runtime.TotalMemory()}");
        }
        else
        {
            stringBuilder.AppendLine("Error: Unable to call Java.Lang.Runtime.GetRuntime()");
        }

        /*
        using (var grContext = GRContext.CreateGl())
        {
            if (grContext is not null)
            {
                stringBuilder.AppendLine($"GPU Vendor: {Android.Opengl.GLES20.GlGetString(Android.Opengl.GLES20.GlVendor)}");
                stringBuilder.AppendLine($"GPU Renderer: {Android.Opengl.GLES20.GlGetString(Android.Opengl.GLES20.GlRenderer)}");
                stringBuilder.AppendLine($"GPU Version: {Android.Opengl.GLES20.GlGetString(Android.Opengl.GLES20.GlVersion)}");
            }
        }
        */

        /*
        var glSurfaceView = new Android.Opengl.GLSurfaceView(Platform.CurrentActivity);
        glSurfaceView.SetEGLContextClientVersion(2);
        glSurfaceView.SetRenderer(new CustomGlRenderer());

        var test = page.Handler.PlatformView;
        Debugger.Break();
        */
        
        /*
        stringBuilder.AppendLine($"GPU Vendor: {Android.Opengl.GLES20.GlGetString(Android.Opengl.GLES20.GlVendor)}");
        stringBuilder.AppendLine($"GPU Renderer: {Android.Opengl.GLES20.GlGetString(Android.Opengl.GLES20.GlRenderer)}");
        stringBuilder.AppendLine($"GPU Version: {Android.Opengl.GLES20.GlGetString(Android.Opengl.GLES20.GlVersion)}");
        */
        
        
        stringBuilder.AppendLine($"Build.Board: {Android.OS.Build.Board}");
        stringBuilder.AppendLine($"Build.Bootloader: {Android.OS.Build.Bootloader}");
        stringBuilder.AppendLine($"Build.Brand: {Android.OS.Build.Brand}");
        stringBuilder.AppendLine($"Build.Device: {Android.OS.Build.Device}");
        stringBuilder.AppendLine($"Build.Display: {Android.OS.Build.Display}");
        stringBuilder.AppendLine($"Build.Hardware: {Android.OS.Build.Hardware}");
        stringBuilder.AppendLine($"Build.Host: {Android.OS.Build.Host}");
        stringBuilder.AppendLine($"Build.Id: {Android.OS.Build.Id}");
        stringBuilder.AppendLine($"Build.Manufacturer: {Android.OS.Build.Manufacturer}");
        stringBuilder.AppendLine($"Build.Model: {Android.OS.Build.Model}");
        stringBuilder.AppendLine($"Build.OdmSku: {Android.OS.Build.OdmSku}");
        stringBuilder.AppendLine($"Build.Product: {Android.OS.Build.Product}");
        if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.S)
        {
            stringBuilder.AppendLine($"Build.Sku: {Android.OS.Build.Sku}");
            stringBuilder.AppendLine($"Build.SocManufacturer: {Android.OS.Build.SocManufacturer}");
            stringBuilder.AppendLine($"Build.SocModel: {Android.OS.Build.SocModel}");
        }
        stringBuilder.AppendLine($"Build.SupportedAbis: {String.Join(", ", Android.OS.Build.SupportedAbis ?? Array.Empty<string>())}");
        stringBuilder.AppendLine($"Build.Tags: {Android.OS.Build.Tags}");
        stringBuilder.AppendLine($"Build.Time: {Android.OS.Build.Time}");
        stringBuilder.AppendLine($"Build.Type: {Android.OS.Build.Type}");
        #endif
        
        Details = stringBuilder.ToString();
    }

    [RelayCommand]
    async Task ShareDetailsAsync()
    {
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Text = Details,
            Title = "Share details"
        });
    }
}