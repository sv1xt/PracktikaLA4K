; ModuleID = 'obj\Debug\130\android\marshal_methods.x86.ll'
source_filename = "obj\Debug\130\android\marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [270 x i32] [
	i32 32687329, ; 0: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 77
	i32 34715100, ; 1: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 106
	i32 39109920, ; 2: Newtonsoft.Json.dll => 0x254c520 => 18
	i32 57263871, ; 3: Xamarin.Forms.Core.dll => 0x369c6ff => 101
	i32 95598293, ; 4: Supabase.dll => 0x5b2b6d5 => 20
	i32 101534019, ; 5: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 91
	i32 120558881, ; 6: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 91
	i32 122350210, ; 7: System.Threading.Channels.dll => 0x74aea82 => 37
	i32 162612358, ; 8: MimeMapping => 0x9b14486 => 14
	i32 165246403, ; 9: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 58
	i32 166922606, ; 10: Xamarin.Android.Support.Compat.dll => 0x9f3096e => 42
	i32 172012715, ; 11: FastAndroidCamera.dll => 0xa40b4ab => 5
	i32 182336117, ; 12: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 92
	i32 209399409, ; 13: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 56
	i32 219130465, ; 14: Xamarin.Android.Support.v4 => 0xd0faa61 => 47
	i32 220171995, ; 15: System.Diagnostics.Debug => 0xd1f8edb => 128
	i32 230216969, ; 16: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 72
	i32 230752869, ; 17: Microsoft.CSharp.dll => 0xdc10265 => 9
	i32 231814094, ; 18: System.Globalization => 0xdd133ce => 134
	i32 232815796, ; 19: System.Web.Services => 0xde07cb4 => 119
	i32 261689757, ; 20: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 61
	i32 278686392, ; 21: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 76
	i32 280482487, ; 22: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 70
	i32 318968648, ; 23: Xamarin.AndroidX.Activity.dll => 0x13031348 => 48
	i32 321597661, ; 24: System.Numerics => 0x132b30dd => 30
	i32 334355562, ; 25: ZXing.Net.Mobile.Forms.dll => 0x13eddc6a => 109
	i32 342366114, ; 26: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 74
	i32 385762202, ; 27: System.Memory.dll => 0x16fe439a => 121
	i32 389971796, ; 28: Xamarin.Android.Support.Core.UI => 0x173e7f54 => 43
	i32 441335492, ; 29: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 60
	i32 442521989, ; 30: Xamarin.Essentials => 0x1a605985 => 100
	i32 442565967, ; 31: System.Collections => 0x1a61054f => 126
	i32 450948140, ; 32: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 69
	i32 465846621, ; 33: mscorlib => 0x1bc4415d => 16
	i32 469710990, ; 34: System.dll => 0x1bff388e => 28
	i32 476646585, ; 35: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 70
	i32 485463106, ; 36: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 10
	i32 486930444, ; 37: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 81
	i32 498788369, ; 38: System.ObjectModel => 0x1dbae811 => 129
	i32 514659665, ; 39: Xamarin.Android.Support.Compat => 0x1ead1551 => 42
	i32 526420162, ; 40: System.Transactions.dll => 0x1f6088c2 => 113
	i32 545304856, ; 41: System.Runtime.Extensions => 0x2080b118 => 127
	i32 548916678, ; 42: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 8
	i32 577335427, ; 43: System.Security.Cryptography.Cng => 0x22697083 => 123
	i32 605376203, ; 44: System.IO.Compression.FileSystem => 0x24154ecb => 117
	i32 610194910, ; 45: System.Reactive.dll => 0x245ed5de => 32
	i32 627609679, ; 46: Xamarin.AndroidX.CustomView => 0x2568904f => 65
	i32 646852959, ; 47: MSM.Android => 0x268e315f => 0
	i32 662205335, ; 48: System.Text.Encodings.Web.dll => 0x27787397 => 35
	i32 663517072, ; 49: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 97
	i32 666292255, ; 50: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 53
	i32 690569205, ; 51: System.Xml.Linq.dll => 0x29293ff5 => 39
	i32 692692150, ; 52: Xamarin.Android.Support.Annotations => 0x2949a4b6 => 41
	i32 763346851, ; 53: Websocket.Client => 0x2d7fbfa3 => 40
	i32 772621961, ; 54: Supabase.Core.dll => 0x2e0d4689 => 19
	i32 775507847, ; 55: System.IO.Compression => 0x2e394f87 => 116
	i32 809851609, ; 56: System.Drawing.Common.dll => 0x30455ad9 => 115
	i32 843511501, ; 57: Xamarin.AndroidX.Print => 0x3246f6cd => 88
	i32 877678880, ; 58: System.Globalization.dll => 0x34505120 => 134
	i32 882883187, ; 59: Xamarin.Android.Support.v4.dll => 0x349fba73 => 47
	i32 920281169, ; 60: Supabase.Functions => 0x36da6051 => 21
	i32 928116545, ; 61: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 106
	i32 954320159, ; 62: ZXing.Net.Mobile.Forms => 0x38e1c51f => 109
	i32 955402788, ; 63: Newtonsoft.Json => 0x38f24a24 => 18
	i32 958213972, ; 64: Xamarin.Android.Support.Media.Compat => 0x391d2f54 => 46
	i32 967690846, ; 65: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 74
	i32 974778368, ; 66: FormsViewGroup.dll => 0x3a19f000 => 6
	i32 992768348, ; 67: System.Collections.dll => 0x3b2c715c => 126
	i32 1012816738, ; 68: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 90
	i32 1035644815, ; 69: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 52
	i32 1042160112, ; 70: Xamarin.Forms.Platform.dll => 0x3e1e19f0 => 103
	i32 1052210849, ; 71: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 78
	i32 1089187994, ; 72: Websocket.Client.dll => 0x40ebb09a => 40
	i32 1098259244, ; 73: System => 0x41761b2c => 28
	i32 1134191450, ; 74: ZXingNetMobile.dll => 0x439a635a => 111
	i32 1175144683, ; 75: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 95
	i32 1178241025, ; 76: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 85
	i32 1204270330, ; 77: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 53
	i32 1216849306, ; 78: Supabase.Realtime.dll => 0x4887a59a => 24
	i32 1219540809, ; 79: Supabase.Functions.dll => 0x48b0b749 => 21
	i32 1267360935, ; 80: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 96
	i32 1293217323, ; 81: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 67
	i32 1336984896, ; 82: Supabase.Core => 0x4fb0c540 => 19
	i32 1364015309, ; 83: System.IO => 0x514d38cd => 132
	i32 1365406463, ; 84: System.ServiceModel.Internals.dll => 0x516272ff => 120
	i32 1376866003, ; 85: Xamarin.AndroidX.SavedState => 0x52114ed3 => 90
	i32 1395857551, ; 86: Xamarin.AndroidX.Media.dll => 0x5333188f => 82
	i32 1406073936, ; 87: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 62
	i32 1411638395, ; 88: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 33
	i32 1445445088, ; 89: Xamarin.Android.Support.Fragment => 0x5627bde0 => 45
	i32 1457743152, ; 90: System.Runtime.Extensions.dll => 0x56e36530 => 127
	i32 1460219004, ; 91: Xamarin.Forms.Xaml => 0x57092c7c => 104
	i32 1460893475, ; 92: System.IdentityModel.Tokens.Jwt => 0x57137723 => 29
	i32 1462112819, ; 93: System.IO.Compression.dll => 0x57261233 => 116
	i32 1469204771, ; 94: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 51
	i32 1498168481, ; 95: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 11
	i32 1516168485, ; 96: Supabase.Gotrue => 0x5a5ee525 => 22
	i32 1543031311, ; 97: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 133
	i32 1571005899, ; 98: zxing.portable => 0x5da3a5cb => 110
	i32 1574652163, ; 99: Xamarin.Android.Support.Core.Utils.dll => 0x5ddb4903 => 44
	i32 1582372066, ; 100: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 66
	i32 1592978981, ; 101: System.Runtime.Serialization.dll => 0x5ef2ee25 => 4
	i32 1622152042, ; 102: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 80
	i32 1624863272, ; 103: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 99
	i32 1636350590, ; 104: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 64
	i32 1639515021, ; 105: System.Net.Http.dll => 0x61b9038d => 3
	i32 1639986890, ; 106: System.Text.RegularExpressions => 0x61c036ca => 133
	i32 1657153582, ; 107: System.Runtime => 0x62c6282e => 34
	i32 1658241508, ; 108: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 93
	i32 1658251792, ; 109: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 105
	i32 1670060433, ; 110: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 61
	i32 1701541528, ; 111: System.Diagnostics.Debug.dll => 0x656b7698 => 128
	i32 1729485958, ; 112: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 57
	i32 1766324549, ; 113: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 92
	i32 1776026572, ; 114: System.Core.dll => 0x69dc03cc => 27
	i32 1788241197, ; 115: Xamarin.AndroidX.Fragment => 0x6a96652d => 69
	i32 1796167890, ; 116: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 8
	i32 1808609942, ; 117: Xamarin.AndroidX.Loader => 0x6bcd3296 => 80
	i32 1813201214, ; 118: Xamarin.Google.Android.Material => 0x6c13413e => 105
	i32 1818569960, ; 119: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 86
	i32 1867746548, ; 120: Xamarin.Essentials.dll => 0x6f538cf4 => 100
	i32 1878053835, ; 121: Xamarin.Forms.Xaml.dll => 0x6ff0d3cb => 104
	i32 1885316902, ; 122: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 54
	i32 1904184254, ; 123: FastAndroidCamera => 0x717f8bbe => 5
	i32 1904755420, ; 124: System.Runtime.InteropServices.WindowsRuntime.dll => 0x718842dc => 2
	i32 1919157823, ; 125: Xamarin.AndroidX.MultiDex.dll => 0x7264063f => 83
	i32 1986222447, ; 126: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 13
	i32 2011961780, ; 127: System.Buffers.dll => 0x77ec19b4 => 26
	i32 2018526534, ; 128: Supabase.Gotrue.dll => 0x78504546 => 22
	i32 2019465201, ; 129: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 78
	i32 2055257422, ; 130: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 75
	i32 2079903147, ; 131: System.Runtime.dll => 0x7bf8cdab => 34
	i32 2090596640, ; 132: System.Numerics.Vectors => 0x7c9bf920 => 31
	i32 2097448633, ; 133: Xamarin.AndroidX.Legacy.Support.Core.UI => 0x7d0486b9 => 71
	i32 2126786730, ; 134: Xamarin.Forms.Platform.Android => 0x7ec430aa => 102
	i32 2128198166, ; 135: Supabase.Realtime => 0x7ed9ba16 => 24
	i32 2138252982, ; 136: Supabase => 0x7f7326b6 => 20
	i32 2166116741, ; 137: Xamarin.Android.Support.Core.Utils => 0x811c5185 => 44
	i32 2193016926, ; 138: System.ObjectModel.dll => 0x82b6c85e => 129
	i32 2201231467, ; 139: System.Net.Http => 0x8334206b => 3
	i32 2217644978, ; 140: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 95
	i32 2244775296, ; 141: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 81
	i32 2256548716, ; 142: Xamarin.AndroidX.MultiDex => 0x8680336c => 83
	i32 2261435625, ; 143: Xamarin.AndroidX.Legacy.Support.V4.dll => 0x86cac4e9 => 73
	i32 2279755925, ; 144: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 89
	i32 2315684594, ; 145: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 49
	i32 2329204181, ; 146: zxing.portable.dll => 0x8ad4d5d5 => 110
	i32 2330457430, ; 147: Xamarin.Android.Support.Core.UI.dll => 0x8ae7f556 => 43
	i32 2341995103, ; 148: ZXingNetMobile => 0x8b98025f => 111
	i32 2369706906, ; 149: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 12
	i32 2373288475, ; 150: Xamarin.Android.Support.Fragment.dll => 0x8d75821b => 45
	i32 2409053734, ; 151: Xamarin.AndroidX.Preference.dll => 0x8f973e26 => 87
	i32 2431243866, ; 152: ZXing.Net.Mobile.Core.dll => 0x90e9d65a => 107
	i32 2454642406, ; 153: System.Text.Encoding.dll => 0x924edee6 => 131
	i32 2465532216, ; 154: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 60
	i32 2471841756, ; 155: netstandard.dll => 0x93554fdc => 1
	i32 2475788418, ; 156: Java.Interop.dll => 0x93918882 => 7
	i32 2482213323, ; 157: ZXing.Net.Mobile.Forms.Android => 0x93f391cb => 108
	i32 2501346920, ; 158: System.Data.DataSetExtensions => 0x95178668 => 114
	i32 2505896520, ; 159: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 77
	i32 2562349572, ; 160: Microsoft.CSharp => 0x98ba5a04 => 9
	i32 2570120770, ; 161: System.Text.Encodings.Web => 0x9930ee42 => 35
	i32 2581819634, ; 162: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 96
	i32 2620871830, ; 163: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 64
	i32 2624644809, ; 164: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 68
	i32 2633051222, ; 165: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 76
	i32 2640290731, ; 166: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 12
	i32 2693849962, ; 167: System.IO.dll => 0xa090e36a => 132
	i32 2701096212, ; 168: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 93
	i32 2715334215, ; 169: System.Threading.Tasks.dll => 0xa1d8b647 => 125
	i32 2719963679, ; 170: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 123
	i32 2732626843, ; 171: Xamarin.AndroidX.Activity => 0xa2e0939b => 48
	i32 2735172069, ; 172: System.Threading.Channels => 0xa30769e5 => 37
	i32 2737747696, ; 173: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 51
	i32 2766581644, ; 174: Xamarin.Forms.Core => 0xa4e6af8c => 101
	i32 2778768386, ; 175: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 98
	i32 2810250172, ; 176: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 62
	i32 2819470561, ; 177: System.Xml.dll => 0xa80db4e1 => 38
	i32 2853208004, ; 178: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 98
	i32 2855708567, ; 179: Xamarin.AndroidX.Transition => 0xaa36a797 => 94
	i32 2903344695, ; 180: System.ComponentModel.Composition => 0xad0d8637 => 118
	i32 2905242038, ; 181: mscorlib.dll => 0xad2a79b6 => 16
	i32 2916838712, ; 182: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 99
	i32 2919462931, ; 183: System.Numerics.Vectors.dll => 0xae037813 => 31
	i32 2921128767, ; 184: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 50
	i32 2978675010, ; 185: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 67
	i32 3024354802, ; 186: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 72
	i32 3044182254, ; 187: FormsViewGroup => 0xb57288ee => 6
	i32 3057625584, ; 188: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 84
	i32 3075834255, ; 189: System.Threading.Tasks => 0xb755818f => 125
	i32 3084678329, ; 190: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 13
	i32 3092211740, ; 191: Xamarin.Android.Support.Media.Compat.dll => 0xb84f681c => 46
	i32 3099081453, ; 192: MimeMapping.dll => 0xb8b83aed => 14
	i32 3111772706, ; 193: System.Runtime.Serialization => 0xb979e222 => 4
	i32 3124832203, ; 194: System.Threading.Tasks.Extensions => 0xba4127cb => 122
	i32 3138360719, ; 195: Supabase.Postgrest.dll => 0xbb0f958f => 23
	i32 3204380047, ; 196: System.Data.dll => 0xbefef58f => 112
	i32 3211777861, ; 197: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 66
	i32 3220365878, ; 198: System.Threading => 0xbff2e236 => 130
	i32 3242291779, ; 199: MSM => 0xc1417243 => 17
	i32 3247949154, ; 200: Mono.Security => 0xc197c562 => 124
	i32 3258312781, ; 201: Xamarin.AndroidX.CardView => 0xc235e84d => 57
	i32 3265893370, ; 202: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 122
	i32 3267021929, ; 203: Xamarin.AndroidX.AsyncLayoutInflater => 0xc2bacc69 => 55
	i32 3299363146, ; 204: System.Text.Encoding => 0xc4a8494a => 131
	i32 3312457198, ; 205: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 11
	i32 3317135071, ; 206: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 65
	i32 3317144872, ; 207: System.Data => 0xc5b79d28 => 112
	i32 3340431453, ; 208: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 54
	i32 3346324047, ; 209: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 85
	i32 3353484488, ; 210: Xamarin.AndroidX.Legacy.Support.Core.UI.dll => 0xc7e21cc8 => 71
	i32 3358260929, ; 211: System.Text.Json => 0xc82afec1 => 36
	i32 3362522851, ; 212: Xamarin.AndroidX.Core => 0xc86c06e3 => 63
	i32 3366347497, ; 213: Java.Interop => 0xc8a662e9 => 7
	i32 3374999561, ; 214: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 89
	i32 3395150330, ; 215: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 33
	i32 3404865022, ; 216: System.ServiceModel.Internals => 0xcaf21dfe => 120
	i32 3429136800, ; 217: System.Xml => 0xcc6479a0 => 38
	i32 3430777524, ; 218: netstandard => 0xcc7d82b4 => 1
	i32 3439690031, ; 219: Xamarin.Android.Support.Annotations.dll => 0xcd05812f => 41
	i32 3441283291, ; 220: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 68
	i32 3476120550, ; 221: Mono.Android => 0xcf3163e6 => 15
	i32 3485117614, ; 222: System.Text.Json.dll => 0xcfbaacae => 36
	i32 3486566296, ; 223: System.Transactions => 0xcfd0c798 => 113
	i32 3493954962, ; 224: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 59
	i32 3501239056, ; 225: Xamarin.AndroidX.AsyncLayoutInflater.dll => 0xd0b0ab10 => 55
	i32 3509114376, ; 226: System.Xml.Linq => 0xd128d608 => 39
	i32 3536029504, ; 227: Xamarin.Forms.Platform.Android.dll => 0xd2c38740 => 102
	i32 3567349600, ; 228: System.ComponentModel.Composition.dll => 0xd4a16f60 => 118
	i32 3607666123, ; 229: Supabase.Postgrest => 0xd7089dcb => 23
	i32 3618140916, ; 230: Xamarin.AndroidX.Preference => 0xd7a872f4 => 87
	i32 3627220390, ; 231: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 88
	i32 3632359727, ; 232: Xamarin.Forms.Platform => 0xd881692f => 103
	i32 3633644679, ; 233: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 50
	i32 3641597786, ; 234: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 75
	i32 3672681054, ; 235: Mono.Android.dll => 0xdae8aa5e => 15
	i32 3676310014, ; 236: System.Web.Services.dll => 0xdb2009fe => 119
	i32 3682565725, ; 237: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 56
	i32 3684561358, ; 238: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 59
	i32 3684933406, ; 239: System.Runtime.InteropServices.WindowsRuntime => 0xdba39f1e => 2
	i32 3689375977, ; 240: System.Drawing.Common => 0xdbe768e9 => 115
	i32 3700591436, ; 241: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 10
	i32 3718780102, ; 242: Xamarin.AndroidX.Annotation => 0xdda814c6 => 49
	i32 3724971120, ; 243: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 84
	i32 3731644420, ; 244: System.Reactive => 0xde6c6004 => 32
	i32 3758932259, ; 245: Xamarin.AndroidX.Legacy.Support.V4 => 0xe00cc123 => 73
	i32 3779181567, ; 246: MSM.Android.dll => 0xe141bbff => 0
	i32 3786282454, ; 247: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 58
	i32 3822602673, ; 248: Xamarin.AndroidX.Media => 0xe3d849b1 => 82
	i32 3829621856, ; 249: System.Numerics.dll => 0xe4436460 => 30
	i32 3847036339, ; 250: ZXing.Net.Mobile.Forms.Android.dll => 0xe54d1db3 => 108
	i32 3885922214, ; 251: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 94
	i32 3896760992, ; 252: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 63
	i32 3906640997, ; 253: Supabase.Storage.dll => 0xe8da9c65 => 25
	i32 3920810846, ; 254: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 117
	i32 3921031405, ; 255: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 97
	i32 3931092270, ; 256: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 86
	i32 3945713374, ; 257: System.Data.DataSetExtensions.dll => 0xeb2ecede => 114
	i32 3955647286, ; 258: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 52
	i32 3980364293, ; 259: Supabase.Storage => 0xed3f8a05 => 25
	i32 4025784931, ; 260: System.Memory => 0xeff49a63 => 121
	i32 4028990382, ; 261: MSM.dll => 0xf02583ae => 17
	i32 4073602200, ; 262: System.Threading.dll => 0xf2ce3c98 => 130
	i32 4105002889, ; 263: Mono.Security.dll => 0xf4ad5f89 => 124
	i32 4151237749, ; 264: System.Core => 0xf76edc75 => 27
	i32 4182413190, ; 265: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 79
	i32 4186595366, ; 266: ZXing.Net.Mobile.Core => 0xf98a6026 => 107
	i32 4260525087, ; 267: System.Buffers => 0xfdf2741f => 26
	i32 4263231520, ; 268: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 29
	i32 4292120959 ; 269: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 79
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [270 x i32] [
	i32 77, i32 106, i32 18, i32 101, i32 20, i32 91, i32 91, i32 37, ; 0..7
	i32 14, i32 58, i32 42, i32 5, i32 92, i32 56, i32 47, i32 128, ; 8..15
	i32 72, i32 9, i32 134, i32 119, i32 61, i32 76, i32 70, i32 48, ; 16..23
	i32 30, i32 109, i32 74, i32 121, i32 43, i32 60, i32 100, i32 126, ; 24..31
	i32 69, i32 16, i32 28, i32 70, i32 10, i32 81, i32 129, i32 42, ; 32..39
	i32 113, i32 127, i32 8, i32 123, i32 117, i32 32, i32 65, i32 0, ; 40..47
	i32 35, i32 97, i32 53, i32 39, i32 41, i32 40, i32 19, i32 116, ; 48..55
	i32 115, i32 88, i32 134, i32 47, i32 21, i32 106, i32 109, i32 18, ; 56..63
	i32 46, i32 74, i32 6, i32 126, i32 90, i32 52, i32 103, i32 78, ; 64..71
	i32 40, i32 28, i32 111, i32 95, i32 85, i32 53, i32 24, i32 21, ; 72..79
	i32 96, i32 67, i32 19, i32 132, i32 120, i32 90, i32 82, i32 62, ; 80..87
	i32 33, i32 45, i32 127, i32 104, i32 29, i32 116, i32 51, i32 11, ; 88..95
	i32 22, i32 133, i32 110, i32 44, i32 66, i32 4, i32 80, i32 99, ; 96..103
	i32 64, i32 3, i32 133, i32 34, i32 93, i32 105, i32 61, i32 128, ; 104..111
	i32 57, i32 92, i32 27, i32 69, i32 8, i32 80, i32 105, i32 86, ; 112..119
	i32 100, i32 104, i32 54, i32 5, i32 2, i32 83, i32 13, i32 26, ; 120..127
	i32 22, i32 78, i32 75, i32 34, i32 31, i32 71, i32 102, i32 24, ; 128..135
	i32 20, i32 44, i32 129, i32 3, i32 95, i32 81, i32 83, i32 73, ; 136..143
	i32 89, i32 49, i32 110, i32 43, i32 111, i32 12, i32 45, i32 87, ; 144..151
	i32 107, i32 131, i32 60, i32 1, i32 7, i32 108, i32 114, i32 77, ; 152..159
	i32 9, i32 35, i32 96, i32 64, i32 68, i32 76, i32 12, i32 132, ; 160..167
	i32 93, i32 125, i32 123, i32 48, i32 37, i32 51, i32 101, i32 98, ; 168..175
	i32 62, i32 38, i32 98, i32 94, i32 118, i32 16, i32 99, i32 31, ; 176..183
	i32 50, i32 67, i32 72, i32 6, i32 84, i32 125, i32 13, i32 46, ; 184..191
	i32 14, i32 4, i32 122, i32 23, i32 112, i32 66, i32 130, i32 17, ; 192..199
	i32 124, i32 57, i32 122, i32 55, i32 131, i32 11, i32 65, i32 112, ; 200..207
	i32 54, i32 85, i32 71, i32 36, i32 63, i32 7, i32 89, i32 33, ; 208..215
	i32 120, i32 38, i32 1, i32 41, i32 68, i32 15, i32 36, i32 113, ; 216..223
	i32 59, i32 55, i32 39, i32 102, i32 118, i32 23, i32 87, i32 88, ; 224..231
	i32 103, i32 50, i32 75, i32 15, i32 119, i32 56, i32 59, i32 2, ; 232..239
	i32 115, i32 10, i32 49, i32 84, i32 32, i32 73, i32 0, i32 58, ; 240..247
	i32 82, i32 30, i32 108, i32 94, i32 63, i32 25, i32 117, i32 97, ; 248..255
	i32 86, i32 114, i32 52, i32 25, i32 121, i32 17, i32 130, i32 124, ; 256..263
	i32 27, i32 79, i32 107, i32 26, i32 29, i32 79 ; 264..269
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"NumRegisterParameters", i32 0}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
