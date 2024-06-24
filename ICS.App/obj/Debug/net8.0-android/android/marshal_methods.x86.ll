; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [352 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [698 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 26230656, ; 3: Microsoft.Extensions.DependencyModel => 0x1903f80 => 191
	i32 32687329, ; 4: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 269
	i32 34715100, ; 5: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 303
	i32 34839235, ; 6: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 38948123, ; 7: ar\Microsoft.Maui.Controls.resources.dll => 0x2524d1b => 311
	i32 39485524, ; 8: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42244203, ; 9: he\Microsoft.Maui.Controls.resources.dll => 0x284986b => 320
	i32 42639949, ; 10: System.Threading.Thread => 0x28aa24d => 145
	i32 65960268, ; 11: Microsoft.Win32.SystemEvents.dll => 0x3ee794c => 215
	i32 66541672, ; 12: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 13: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 344
	i32 68219467, ; 14: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 15: Microsoft.Maui.Graphics.dll => 0x44bb714 => 213
	i32 82292897, ; 16: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 83839681, ; 17: ms\Microsoft.Maui.Controls.resources.dll => 0x4ff4ac1 => 328
	i32 101534019, ; 18: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 287
	i32 117431740, ; 19: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120558881, ; 20: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 287
	i32 122350210, ; 21: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 22: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 307
	i32 136584136, ; 23: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0x8241bc8 => 343
	i32 140062828, ; 24: sk\Microsoft.Maui.Controls.resources.dll => 0x859306c => 336
	i32 142721839, ; 25: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 26: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 27: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 28: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 243
	i32 176265551, ; 29: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 30: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 289
	i32 184328833, ; 31: System.ValueTuple.dll => 0xafca281 => 151
	i32 205061960, ; 32: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 33: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 241
	i32 220171995, ; 34: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 263
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 246
	i32 276479776, ; 41: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 42: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 265
	i32 280482487, ; 43: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 262
	i32 291076382, ; 44: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 45: System.Net.Ping.dll => 0x11d123fd => 69
	i32 317674968, ; 46: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 341
	i32 318968648, ; 47: Xamarin.AndroidX.Activity.dll => 0x13031348 => 232
	i32 321597661, ; 48: System.Numerics => 0x132b30dd => 83
	i32 321963286, ; 49: fr\Microsoft.Maui.Controls.resources.dll => 0x1330c516 => 319
	i32 330147069, ; 50: Microsoft.SqlServer.Server => 0x13ada4fd => 214
	i32 342366114, ; 51: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 264
	i32 347068432, ; 52: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 218
	i32 360082299, ; 53: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 54: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 55: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 56: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 57: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 58: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 59: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 60: _Microsoft.Android.Resource.Designer => 0x17969339 => 348
	i32 403441872, ; 61: WindowsBase => 0x180c08d0 => 165
	i32 409257351, ; 62: tr\Microsoft.Maui.Controls.resources.dll => 0x1864c587 => 339
	i32 441335492, ; 63: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 247
	i32 442565967, ; 64: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 65: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 260
	i32 451504562, ; 66: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 67: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 68: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 69: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 70: System.dll => 0x1bff388e => 164
	i32 476646585, ; 71: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 262
	i32 485463106, ; 72: Microsoft.IdentityModel.Abstractions => 0x1cef9442 => 202
	i32 486930444, ; 73: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 275
	i32 489220957, ; 74: es\Microsoft.Maui.Controls.resources.dll => 0x1d28eb5d => 317
	i32 498788369, ; 75: System.ObjectModel => 0x1dbae811 => 84
	i32 513247710, ; 76: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 199
	i32 526420162, ; 77: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 78: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 307
	i32 530272170, ; 79: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 538707440, ; 80: th\Microsoft.Maui.Controls.resources.dll => 0x201c05f0 => 338
	i32 539058512, ; 81: Microsoft.Extensions.Logging => 0x20216150 => 192
	i32 540030774, ; 82: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 83: System.Runtime.Extensions => 0x2080b118 => 103
	i32 545795345, ; 84: Microsoft.Extensions.Logging.Configuration.dll => 0x20882d11 => 194
	i32 546455878, ; 85: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 548916678, ; 86: Microsoft.Bcl.AsyncInterfaces => 0x20b7cdc6 => 176
	i32 549171840, ; 87: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 88: Jsr305Binding => 0x213954e7 => 300
	i32 569601784, ; 89: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 298
	i32 577335427, ; 90: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 601371474, ; 91: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 92: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 93: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 627609679, ; 94: Xamarin.AndroidX.CustomView => 0x2568904f => 252
	i32 627931235, ; 95: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 330
	i32 639843206, ; 96: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 258
	i32 643868501, ; 97: System.Net => 0x2660a755 => 81
	i32 662205335, ; 98: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 99: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 294
	i32 666292255, ; 100: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 239
	i32 672442732, ; 101: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 102: System.Net.Security => 0x28bdabca => 73
	i32 690569205, ; 103: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 104: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 309
	i32 692151471, ; 105: Microsoft.Extensions.Logging.Console.dll => 0x294164af => 195
	i32 693804605, ; 106: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 107: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 108: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 304
	i32 700358131, ; 109: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 720511267, ; 110: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 308
	i32 722857257, ; 111: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 731701662, ; 112: Microsoft.Extensions.Options.ConfigurationExtensions => 0x2b9ce19e => 198
	i32 735137430, ; 113: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 742136276, ; 114: ICS.Common => 0x2c3c19d4 => 346
	i32 748832960, ; 115: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 216
	i32 752232764, ; 116: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 117: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 229
	i32 759454413, ; 118: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 119: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 120: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 121: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 336
	i32 789151979, ; 122: Microsoft.Extensions.Options => 0x2f0980eb => 197
	i32 790371945, ; 123: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 253
	i32 804715423, ; 124: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 125: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 267
	i32 809851609, ; 126: System.Drawing.Common.dll => 0x30455ad9 => 221
	i32 823281589, ; 127: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 128: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 129: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 130: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 131: Xamarin.AndroidX.Print => 0x3246f6cd => 280
	i32 869139383, ; 132: hi\Microsoft.Maui.Controls.resources.dll => 0x33ce03b7 => 321
	i32 873119928, ; 133: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 134: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 135: System.Net.Http.Json => 0x3463c971 => 63
	i32 880668424, ; 136: ru\Microsoft.Maui.Controls.resources.dll => 0x347def08 => 335
	i32 883087626, ; 137: ICS.Common.dll => 0x34a2d90a => 346
	i32 904024072, ; 138: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 139: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 918734561, ; 140: pt-BR\Microsoft.Maui.Controls.resources.dll => 0x36c2c6e1 => 332
	i32 928116545, ; 141: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 303
	i32 952186615, ; 142: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 956575887, ; 143: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 308
	i32 961460050, ; 144: it\Microsoft.Maui.Controls.resources.dll => 0x394eb752 => 325
	i32 966729478, ; 145: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 301
	i32 967690846, ; 146: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 264
	i32 975236339, ; 147: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 148: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 149: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 150: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 990727110, ; 151: ro\Microsoft.Maui.Controls.resources.dll => 0x3b0d4bc6 => 334
	i32 992768348, ; 152: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 153: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 154: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 155: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 284
	i32 1019214401, ; 156: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 157: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 190
	i32 1031528504, ; 158: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 302
	i32 1035622896, ; 159: ICS.DAL => 0x3dba59f0 => 347
	i32 1035644815, ; 160: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 237
	i32 1036536393, ; 161: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1043950537, ; 162: de\Microsoft.Maui.Controls.resources.dll => 0x3e396bc9 => 315
	i32 1044663988, ; 163: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1052210849, ; 164: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 271
	i32 1062017875, ; 165: Microsoft.Identity.Client.Extensions.Msal => 0x3f4d1b53 => 201
	i32 1067306892, ; 166: GoogleGson => 0x3f9dcf8c => 175
	i32 1082857460, ; 167: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 168: Xamarin.Kotlin.StdLib => 0x409e66d8 => 305
	i32 1098259244, ; 169: System => 0x41761b2c => 164
	i32 1108272742, ; 170: sv\Microsoft.Maui.Controls.resources.dll => 0x420ee666 => 337
	i32 1117529484, ; 171: pl\Microsoft.Maui.Controls.resources.dll => 0x429c258c => 331
	i32 1118262833, ; 172: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 327
	i32 1121599056, ; 173: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 270
	i32 1127624469, ; 174: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 196
	i32 1138436374, ; 175: Microsoft.Data.SqlClient.dll => 0x43db2916 => 177
	i32 1144741499, ; 176: ICS.BL => 0x443b5e7b => 345
	i32 1145483052, ; 177: System.Windows.Extensions.dll => 0x4446af2c => 227
	i32 1149092582, ; 178: Xamarin.AndroidX.Window => 0x447dc2e6 => 297
	i32 1157931901, ; 179: Microsoft.EntityFrameworkCore.Abstractions => 0x4504a37d => 180
	i32 1168523401, ; 180: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 333
	i32 1170634674, ; 181: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 182: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 293
	i32 1178241025, ; 183: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 278
	i32 1202000627, ; 184: Microsoft.EntityFrameworkCore.Abstractions.dll => 0x47a512f3 => 180
	i32 1204270330, ; 185: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 239
	i32 1204575371, ; 186: Microsoft.Extensions.Caching.Memory.dll => 0x47cc5c8b => 185
	i32 1208641965, ; 187: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1219128291, ; 188: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1243150071, ; 189: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 298
	i32 1253011324, ; 190: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 191: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 313
	i32 1264511973, ; 192: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 288
	i32 1267360935, ; 193: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 292
	i32 1273260888, ; 194: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 244
	i32 1275534314, ; 195: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 309
	i32 1278448581, ; 196: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 236
	i32 1292207520, ; 197: SQLitePCLRaw.core.dll => 0x4d0585a0 => 217
	i32 1293217323, ; 198: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 255
	i32 1308624726, ; 199: hr\Microsoft.Maui.Controls.resources.dll => 0x4e000756 => 322
	i32 1309188875, ; 200: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 201: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 297
	i32 1324164729, ; 202: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 203: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1336711579, ; 204: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x4fac999b => 342
	i32 1364015309, ; 205: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 206: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 343
	i32 1376866003, ; 207: Xamarin.AndroidX.SavedState => 0x52114ed3 => 284
	i32 1379779777, ; 208: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 209: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 210: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 248
	i32 1408764838, ; 211: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 212: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 213: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1430672901, ; 214: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 311
	i32 1434145427, ; 215: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 216: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 301
	i32 1439761251, ; 217: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 218: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 219: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 220: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 221: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1460893475, ; 222: System.IdentityModel.Tokens.Jwt => 0x57137723 => 222
	i32 1461004990, ; 223: es\Microsoft.Maui.Controls.resources => 0x57152abe => 317
	i32 1461234159, ; 224: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 225: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 226: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 227: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 238
	i32 1470490898, ; 228: Microsoft.Extensions.Primitives => 0x57a5e912 => 199
	i32 1479771757, ; 229: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 230: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 231: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 232: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 285
	i32 1490351284, ; 233: Microsoft.Data.Sqlite.dll => 0x58d4f4b4 => 178
	i32 1498168481, ; 234: Microsoft.IdentityModel.JsonWebTokens.dll => 0x594c3ca1 => 203
	i32 1526286932, ; 235: vi\Microsoft.Maui.Controls.resources.dll => 0x5af94a54 => 341
	i32 1536373174, ; 236: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 237: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 238: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 239: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1565310744, ; 240: System.Runtime.Caching => 0x5d4cbf18 => 224
	i32 1565862583, ; 241: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 242: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 243: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 244: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582305585, ; 245: Azure.Identity => 0x5e501131 => 174
	i32 1582372066, ; 246: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 254
	i32 1592978981, ; 247: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 248: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 302
	i32 1601112923, ; 249: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 250: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 251: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 252: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 274
	i32 1622358360, ; 253: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 254: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 296
	i32 1628113371, ; 255: Microsoft.IdentityModel.Protocols.OpenIdConnect => 0x610b09db => 206
	i32 1635184631, ; 256: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 258
	i32 1636350590, ; 257: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 251
	i32 1639515021, ; 258: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 259: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 260: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1649592141, ; 261: ICS.App => 0x6252c74d => 0
	i32 1657153582, ; 262: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 263: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 290
	i32 1658251792, ; 264: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 299
	i32 1670060433, ; 265: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 246
	i32 1675553242, ; 266: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 267: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 268: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 269: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1688112883, ; 270: Microsoft.Data.Sqlite => 0x649e8ef3 => 178
	i32 1689493916, ; 271: Microsoft.EntityFrameworkCore.dll => 0x64b3a19c => 179
	i32 1691477237, ; 272: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 273: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 274: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 306
	i32 1701541528, ; 275: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 276: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 218
	i32 1720223769, ; 277: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 267
	i32 1726116996, ; 278: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 279: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 280: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 242
	i32 1743415430, ; 281: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 312
	i32 1744735666, ; 282: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 283: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 284: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 285: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 286: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 287: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 288: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 289
	i32 1770582343, ; 289: Microsoft.Extensions.Logging.dll => 0x6988f147 => 192
	i32 1776026572, ; 290: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 291: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 292: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1782862114, ; 293: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 328
	i32 1788241197, ; 294: Xamarin.AndroidX.Fragment => 0x6a96652d => 260
	i32 1793755602, ; 295: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 320
	i32 1794500907, ; 296: Microsoft.Identity.Client.dll => 0x6af5e92b => 200
	i32 1796167890, ; 297: Microsoft.Bcl.AsyncInterfaces.dll => 0x6b0f58d2 => 176
	i32 1808609942, ; 298: Xamarin.AndroidX.Loader => 0x6bcd3296 => 274
	i32 1813058853, ; 299: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 305
	i32 1813201214, ; 300: Xamarin.Google.Android.Material => 0x6c13413e => 299
	i32 1818569960, ; 301: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 279
	i32 1818787751, ; 302: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 303: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 304: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1828688058, ; 305: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 193
	i32 1829150748, ; 306: System.Windows.Extensions => 0x6d06a01c => 227
	i32 1847515442, ; 307: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 229
	i32 1853025655, ; 308: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 337
	i32 1858542181, ; 309: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 310: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1871986876, ; 311: Microsoft.IdentityModel.Protocols.OpenIdConnect.dll => 0x6f9440bc => 206
	i32 1875935024, ; 312: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 319
	i32 1879696579, ; 313: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 314: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 240
	i32 1886040351, ; 315: Microsoft.EntityFrameworkCore.Sqlite.dll => 0x706ab11f => 182
	i32 1888955245, ; 316: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 317: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1893218855, ; 318: cs\Microsoft.Maui.Controls.resources.dll => 0x70d83a27 => 313
	i32 1898237753, ; 319: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 320: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 321: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 322: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1949967253, ; 323: ICS.DAL.dll => 0x743a2395 => 347
	i32 1953182387, ; 324: id\Microsoft.Maui.Controls.resources.dll => 0x746b32b3 => 324
	i32 1956758971, ; 325: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 326: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 286
	i32 1968388702, ; 327: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 186
	i32 1983156543, ; 328: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 306
	i32 1985761444, ; 329: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 231
	i32 1986222447, ; 330: Microsoft.IdentityModel.Tokens.dll => 0x7663596f => 207
	i32 2003115576, ; 331: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 316
	i32 2011961780, ; 332: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2014489277, ; 333: Microsoft.EntityFrameworkCore.Sqlite => 0x7812aabd => 182
	i32 2019465201, ; 334: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 271
	i32 2031763787, ; 335: Xamarin.Android.Glide => 0x791a414b => 228
	i32 2040764568, ; 336: Microsoft.Identity.Client.Extensions.Msal.dll => 0x79a39898 => 201
	i32 2045470958, ; 337: System.Private.Xml => 0x79eb68ee => 88
	i32 2048278909, ; 338: Microsoft.Extensions.Configuration.Binder.dll => 0x7a16417d => 188
	i32 2055257422, ; 339: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 266
	i32 2060060697, ; 340: System.Windows.dll => 0x7aca0819 => 154
	i32 2066184531, ; 341: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 315
	i32 2070888862, ; 342: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 343: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 344: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2103459038, ; 345: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 219
	i32 2127167465, ; 346: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 347: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 348: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 349: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 350: Microsoft.Maui => 0x80bd55ad => 211
	i32 2169148018, ; 351: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 323
	i32 2181898931, ; 352: Microsoft.Extensions.Options.dll => 0x820d22b3 => 197
	i32 2192057212, ; 353: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 193
	i32 2193016926, ; 354: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2197979891, ; 355: Microsoft.Extensions.DependencyModel.dll => 0x830282f3 => 191
	i32 2201107256, ; 356: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 310
	i32 2201231467, ; 357: System.Net.Http => 0x8334206b => 64
	i32 2207618523, ; 358: it\Microsoft.Maui.Controls.resources => 0x839595db => 325
	i32 2217644978, ; 359: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 293
	i32 2222056684, ; 360: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2244775296, ; 361: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 275
	i32 2252106437, ; 362: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2252897993, ; 363: Microsoft.EntityFrameworkCore => 0x86487ec9 => 179
	i32 2253551641, ; 364: Microsoft.IdentityModel.Protocols => 0x86527819 => 205
	i32 2256313426, ; 365: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 366: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 367: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 187
	i32 2267999099, ; 368: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 230
	i32 2279755925, ; 369: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 282
	i32 2293034957, ; 370: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 371: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 372: System.Net.Mail => 0x88ffe49e => 66
	i32 2303942373, ; 373: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 329
	i32 2305521784, ; 374: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 375: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 234
	i32 2320631194, ; 376: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 377: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 378: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 379: System.Net.Primitives => 0x8c40e0db => 70
	i32 2366048013, ; 380: hu\Microsoft.Maui.Controls.resources.dll => 0x8d07070d => 323
	i32 2368005991, ; 381: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2369706906, ; 382: Microsoft.IdentityModel.Logging => 0x8d3edb9a => 204
	i32 2371007202, ; 383: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 186
	i32 2378619854, ; 384: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 385: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2395872292, ; 386: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 324
	i32 2401565422, ; 387: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 388: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 257
	i32 2421380589, ; 389: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2423080555, ; 390: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 244
	i32 2427813419, ; 391: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 321
	i32 2435356389, ; 392: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 393: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 394: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 395: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 396: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465273461, ; 397: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 216
	i32 2465532216, ; 398: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 247
	i32 2471841756, ; 399: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 400: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 401: Microsoft.Maui.Controls => 0x93dba8a1 => 209
	i32 2483903535, ; 402: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 403: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 404: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 405: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2503351294, ; 406: ko\Microsoft.Maui.Controls.resources.dll => 0x95361bfe => 327
	i32 2505896520, ; 407: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 269
	i32 2522472828, ; 408: Xamarin.Android.Glide.dll => 0x9659e17c => 228
	i32 2538310050, ; 409: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2550873716, ; 410: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 322
	i32 2562349572, ; 411: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 412: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2576534780, ; 413: ja\Microsoft.Maui.Controls.resources.dll => 0x9992ccfc => 326
	i32 2581783588, ; 414: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 270
	i32 2581819634, ; 415: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 292
	i32 2585220780, ; 416: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 417: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 418: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2593496499, ; 419: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 331
	i32 2605712449, ; 420: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 310
	i32 2615233544, ; 421: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 261
	i32 2616218305, ; 422: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 196
	i32 2617129537, ; 423: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 424: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 425: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 251
	i32 2624644809, ; 426: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 256
	i32 2626831493, ; 427: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 326
	i32 2627185994, ; 428: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2628210652, ; 429: System.Memory.Data => 0x9ca74fdc => 223
	i32 2629843544, ; 430: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 431: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 265
	i32 2634653062, ; 432: Microsoft.EntityFrameworkCore.Relational.dll => 0x9d099d86 => 181
	i32 2640290731, ; 433: Microsoft.IdentityModel.Logging.dll => 0x9d5fa3ab => 204
	i32 2640706905, ; 434: Azure.Core => 0x9d65fd59 => 173
	i32 2660759594, ; 435: System.Security.Cryptography.ProtectedData.dll => 0x9e97f82a => 225
	i32 2663391936, ; 436: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 230
	i32 2663698177, ; 437: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 438: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 439: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 440: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2677098746, ; 441: Azure.Identity.dll => 0x9f9148fa => 174
	i32 2686887180, ; 442: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 443: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 444: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 290
	i32 2715334215, ; 445: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 446: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 447: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 448: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 449: Xamarin.AndroidX.Activity => 0xa2e0939b => 232
	i32 2735172069, ; 450: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 451: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 238
	i32 2740051746, ; 452: Microsoft.Identity.Client => 0xa351df22 => 200
	i32 2740698338, ; 453: ca\Microsoft.Maui.Controls.resources.dll => 0xa35bbce2 => 312
	i32 2740948882, ; 454: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 455: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2752995522, ; 456: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 332
	i32 2755098380, ; 457: Microsoft.SqlServer.Server.dll => 0xa437770c => 214
	i32 2755643133, ; 458: Microsoft.EntityFrameworkCore.SqlServer => 0xa43fc6fd => 183
	i32 2758225723, ; 459: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 210
	i32 2764765095, ; 460: Microsoft.Maui.dll => 0xa4caf7a7 => 211
	i32 2765824710, ; 461: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 462: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 304
	i32 2778768386, ; 463: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 295
	i32 2779977773, ; 464: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 283
	i32 2785988530, ; 465: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 338
	i32 2788224221, ; 466: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 261
	i32 2795666278, ; 467: Microsoft.Win32.SystemEvents => 0xa6a27b66 => 215
	i32 2801831435, ; 468: Microsoft.Maui.Graphics => 0xa7008e0b => 213
	i32 2803228030, ; 469: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2810250172, ; 470: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 248
	i32 2819470561, ; 471: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 472: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 473: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 283
	i32 2824502124, ; 474: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2838993487, ; 475: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 272
	i32 2841355853, ; 476: System.Security.Permissions => 0xa95ba64d => 226
	i32 2847789619, ; 477: Microsoft.EntityFrameworkCore.Relational => 0xa9bdd233 => 181
	i32 2849599387, ; 478: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 479: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 295
	i32 2855708567, ; 480: Xamarin.AndroidX.Transition => 0xaa36a797 => 291
	i32 2861098320, ; 481: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 482: Microsoft.Maui.Essentials => 0xaa8a4878 => 212
	i32 2863888375, ; 483: ICS.App.dll => 0xaab377f7 => 0
	i32 2867946736, ; 484: System.Security.Cryptography.ProtectedData => 0xaaf164f0 => 225
	i32 2870099610, ; 485: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 233
	i32 2875164099, ; 486: Jsr305Binding.dll => 0xab5f85c3 => 300
	i32 2875220617, ; 487: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2884993177, ; 488: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 259
	i32 2887636118, ; 489: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 490: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 491: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 492: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 493: mscorlib.dll => 0xad2a79b6 => 166
	i32 2908030532, ; 494: ICS.BL.dll => 0xad550644 => 345
	i32 2909740682, ; 495: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2916838712, ; 496: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 296
	i32 2919462931, ; 497: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 498: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 235
	i32 2936416060, ; 499: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 500: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 501: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2944313911, ; 502: System.Configuration.ConfigurationManager.dll => 0xaf7eaa37 => 220
	i32 2959614098, ; 503: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 504: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2971004615, ; 505: Microsoft.Extensions.Options.ConfigurationExtensions.dll => 0xb115eec7 => 198
	i32 2972252294, ; 506: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 507: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 255
	i32 2987532451, ; 508: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 286
	i32 2996846495, ; 509: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 268
	i32 3012788804, ; 510: System.Configuration.ConfigurationManager => 0xb3938244 => 220
	i32 3016983068, ; 511: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 288
	i32 3023353419, ; 512: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 513: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 263
	i32 3033605958, ; 514: System.Memory.Data.dll => 0xb4d12746 => 223
	i32 3038032645, ; 515: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 348
	i32 3053864966, ; 516: fi\Microsoft.Maui.Controls.resources.dll => 0xb6064806 => 318
	i32 3056245963, ; 517: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 285
	i32 3057625584, ; 518: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 276
	i32 3059408633, ; 519: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 520: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3069363400, ; 521: Microsoft.Extensions.Caching.Abstractions.dll => 0xb6f2c4c8 => 184
	i32 3075834255, ; 522: System.Threading.Tasks => 0xb755818f => 144
	i32 3084678329, ; 523: Microsoft.IdentityModel.Tokens => 0xb7dc74b9 => 207
	i32 3090735792, ; 524: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 525: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 526: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3109243939, ; 527: Microsoft.Extensions.Logging.Configuration => 0xb9534c23 => 194
	i32 3111772706, ; 528: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 529: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 530: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 531: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 532: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 533: GoogleGson.dll => 0xbba64c02 => 175
	i32 3159123045, ; 534: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 535: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 536: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 277
	i32 3192346100, ; 537: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 538: System.Web => 0xbe592c0c => 153
	i32 3195844289, ; 539: Microsoft.Extensions.Caching.Abstractions => 0xbe7cb6c1 => 184
	i32 3204380047, ; 540: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 541: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 542: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 254
	i32 3213246214, ; 543: System.Security.Permissions.dll => 0xbf863f06 => 226
	i32 3220365878, ; 544: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 545: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 546: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 547: Xamarin.AndroidX.CardView => 0xc235e84d => 242
	i32 3265493905, ; 548: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 549: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 550: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 551: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 552: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 553: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 554: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 555: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 556: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 318
	i32 3312457198, ; 557: Microsoft.IdentityModel.JsonWebTokens => 0xc57015ee => 203
	i32 3316684772, ; 558: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 559: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 252
	i32 3317144872, ; 560: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 561: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 240
	i32 3345895724, ; 562: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 281
	i32 3346324047, ; 563: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 278
	i32 3357674450, ; 564: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 335
	i32 3358260929, ; 565: System.Text.Json => 0xc82afec1 => 137
	i32 3360279109, ; 566: SQLitePCLRaw.core => 0xc849ca45 => 217
	i32 3362336904, ; 567: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 233
	i32 3362522851, ; 568: Xamarin.AndroidX.Core => 0xc86c06e3 => 249
	i32 3366347497, ; 569: Java.Interop => 0xc8a662e9 => 168
	i32 3374879918, ; 570: Microsoft.IdentityModel.Protocols.dll => 0xc92894ae => 205
	i32 3374999561, ; 571: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 282
	i32 3381016424, ; 572: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 314
	i32 3395150330, ; 573: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 574: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 575: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 253
	i32 3421170118, ; 576: Microsoft.Extensions.Configuration.Binder => 0xcbeae9c6 => 188
	i32 3428513518, ; 577: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 189
	i32 3429136800, ; 578: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 579: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 580: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 256
	i32 3445260447, ; 581: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 582: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 208
	i32 3458724246, ; 583: pt\Microsoft.Maui.Controls.resources.dll => 0xce27f196 => 333
	i32 3471940407, ; 584: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 585: Mono.Android => 0xcf3163e6 => 171
	i32 3484440000, ; 586: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 334
	i32 3485117614, ; 587: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 588: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 589: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 245
	i32 3509114376, ; 590: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 591: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 592: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 593: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3545306353, ; 594: Microsoft.Data.SqlClient => 0xd35114f1 => 177
	i32 3560100363, ; 595: System.Threading.Timer => 0xd432d20b => 147
	i32 3561949811, ; 596: Azure.Core.dll => 0xd44f0a73 => 173
	i32 3570554715, ; 597: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3570608287, ; 598: System.Runtime.Caching.dll => 0xd4d3289f => 224
	i32 3580758918, ; 599: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 342
	i32 3592228221, ; 600: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0xd61d0d7d => 344
	i32 3597029428, ; 601: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 231
	i32 3598340787, ; 602: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 603: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 604: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 605: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 280
	i32 3633644679, ; 606: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 235
	i32 3638274909, ; 607: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 608: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 266
	i32 3643446276, ; 609: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 339
	i32 3643854240, ; 610: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 277
	i32 3645089577, ; 611: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 612: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 187
	i32 3660523487, ; 613: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 614: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 615: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 241
	i32 3684561358, ; 616: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 245
	i32 3689375977, ; 617: System.Drawing.Common => 0xdbe768e9 => 221
	i32 3700591436, ; 618: Microsoft.IdentityModel.Abstractions.dll => 0xdc928b4c => 202
	i32 3700866549, ; 619: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 620: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 250
	i32 3716563718, ; 621: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 622: Xamarin.AndroidX.Annotation => 0xdda814c6 => 234
	i32 3724971120, ; 623: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 276
	i32 3732100267, ; 624: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 625: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 626: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 627: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3751619990, ; 628: da\Microsoft.Maui.Controls.resources.dll => 0xdf9d2d96 => 314
	i32 3754567612, ; 629: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 219
	i32 3786282454, ; 630: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 243
	i32 3792276235, ; 631: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3800979733, ; 632: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 208
	i32 3802395368, ; 633: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 634: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 635: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 636: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 637: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 190
	i32 3844307129, ; 638: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 639: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 640: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 641: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 642: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3885497537, ; 643: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 644: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 291
	i32 3888767677, ; 645: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 281
	i32 3896106733, ; 646: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 647: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 249
	i32 3901907137, ; 648: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920221145, ; 649: nl\Microsoft.Maui.Controls.resources.dll => 0xe9a9d3d9 => 330
	i32 3920810846, ; 650: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 651: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 294
	i32 3928044579, ; 652: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 653: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 654: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 279
	i32 3945713374, ; 655: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 656: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 657: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 237
	i32 3959773229, ; 658: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 268
	i32 4003436829, ; 659: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 660: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 236
	i32 4025784931, ; 661: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 662: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 210
	i32 4054681211, ; 663: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 664: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 665: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4075152723, ; 666: Microsoft.Extensions.Logging.Console => 0xf2e5e553 => 195
	i32 4091086043, ; 667: el\Microsoft.Maui.Controls.resources.dll => 0xf3d904db => 316
	i32 4094352644, ; 668: Microsoft.Maui.Essentials.dll => 0xf40add04 => 212
	i32 4099507663, ; 669: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 670: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 671: Xamarin.AndroidX.Emoji2 => 0xf479582c => 257
	i32 4101842092, ; 672: Microsoft.Extensions.Caching.Memory => 0xf47d24ac => 185
	i32 4103439459, ; 673: uk\Microsoft.Maui.Controls.resources.dll => 0xf4958463 => 340
	i32 4126470640, ; 674: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 189
	i32 4127667938, ; 675: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 676: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 677: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4150914736, ; 678: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 340
	i32 4151237749, ; 679: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 680: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 681: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 682: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 683: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 684: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 273
	i32 4185676441, ; 685: System.Security => 0xf97c5a99 => 130
	i32 4194278001, ; 686: Microsoft.EntityFrameworkCore.SqlServer.dll => 0xf9ff9a71 => 183
	i32 4196529839, ; 687: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 688: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4249188766, ; 689: nb\Microsoft.Maui.Controls.resources.dll => 0xfd45799e => 329
	i32 4256097574, ; 690: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 250
	i32 4258378803, ; 691: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 272
	i32 4260525087, ; 692: System.Buffers => 0xfdf2741f => 7
	i32 4263231520, ; 693: System.IdentityModel.Tokens.Jwt.dll => 0xfe1bc020 => 222
	i32 4271975918, ; 694: Microsoft.Maui.Controls.dll => 0xfea12dee => 209
	i32 4274976490, ; 695: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959, ; 696: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 273
	i32 4294763496 ; 697: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 259
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [698 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 191, ; 3
	i32 269, ; 4
	i32 303, ; 5
	i32 48, ; 6
	i32 311, ; 7
	i32 80, ; 8
	i32 320, ; 9
	i32 145, ; 10
	i32 215, ; 11
	i32 30, ; 12
	i32 344, ; 13
	i32 124, ; 14
	i32 213, ; 15
	i32 102, ; 16
	i32 328, ; 17
	i32 287, ; 18
	i32 107, ; 19
	i32 287, ; 20
	i32 139, ; 21
	i32 307, ; 22
	i32 343, ; 23
	i32 336, ; 24
	i32 77, ; 25
	i32 124, ; 26
	i32 13, ; 27
	i32 243, ; 28
	i32 132, ; 29
	i32 289, ; 30
	i32 151, ; 31
	i32 18, ; 32
	i32 241, ; 33
	i32 26, ; 34
	i32 263, ; 35
	i32 1, ; 36
	i32 59, ; 37
	i32 42, ; 38
	i32 91, ; 39
	i32 246, ; 40
	i32 147, ; 41
	i32 265, ; 42
	i32 262, ; 43
	i32 54, ; 44
	i32 69, ; 45
	i32 341, ; 46
	i32 232, ; 47
	i32 83, ; 48
	i32 319, ; 49
	i32 214, ; 50
	i32 264, ; 51
	i32 218, ; 52
	i32 131, ; 53
	i32 55, ; 54
	i32 149, ; 55
	i32 74, ; 56
	i32 145, ; 57
	i32 62, ; 58
	i32 146, ; 59
	i32 348, ; 60
	i32 165, ; 61
	i32 339, ; 62
	i32 247, ; 63
	i32 12, ; 64
	i32 260, ; 65
	i32 125, ; 66
	i32 152, ; 67
	i32 113, ; 68
	i32 166, ; 69
	i32 164, ; 70
	i32 262, ; 71
	i32 202, ; 72
	i32 275, ; 73
	i32 317, ; 74
	i32 84, ; 75
	i32 199, ; 76
	i32 150, ; 77
	i32 307, ; 78
	i32 60, ; 79
	i32 338, ; 80
	i32 192, ; 81
	i32 51, ; 82
	i32 103, ; 83
	i32 194, ; 84
	i32 114, ; 85
	i32 176, ; 86
	i32 40, ; 87
	i32 300, ; 88
	i32 298, ; 89
	i32 120, ; 90
	i32 52, ; 91
	i32 44, ; 92
	i32 119, ; 93
	i32 252, ; 94
	i32 330, ; 95
	i32 258, ; 96
	i32 81, ; 97
	i32 136, ; 98
	i32 294, ; 99
	i32 239, ; 100
	i32 8, ; 101
	i32 73, ; 102
	i32 155, ; 103
	i32 309, ; 104
	i32 195, ; 105
	i32 154, ; 106
	i32 92, ; 107
	i32 304, ; 108
	i32 45, ; 109
	i32 308, ; 110
	i32 109, ; 111
	i32 198, ; 112
	i32 129, ; 113
	i32 346, ; 114
	i32 216, ; 115
	i32 25, ; 116
	i32 229, ; 117
	i32 72, ; 118
	i32 55, ; 119
	i32 46, ; 120
	i32 336, ; 121
	i32 197, ; 122
	i32 253, ; 123
	i32 22, ; 124
	i32 267, ; 125
	i32 221, ; 126
	i32 86, ; 127
	i32 43, ; 128
	i32 160, ; 129
	i32 71, ; 130
	i32 280, ; 131
	i32 321, ; 132
	i32 3, ; 133
	i32 42, ; 134
	i32 63, ; 135
	i32 335, ; 136
	i32 346, ; 137
	i32 16, ; 138
	i32 53, ; 139
	i32 332, ; 140
	i32 303, ; 141
	i32 105, ; 142
	i32 308, ; 143
	i32 325, ; 144
	i32 301, ; 145
	i32 264, ; 146
	i32 34, ; 147
	i32 158, ; 148
	i32 85, ; 149
	i32 32, ; 150
	i32 334, ; 151
	i32 12, ; 152
	i32 51, ; 153
	i32 56, ; 154
	i32 284, ; 155
	i32 36, ; 156
	i32 190, ; 157
	i32 302, ; 158
	i32 347, ; 159
	i32 237, ; 160
	i32 35, ; 161
	i32 315, ; 162
	i32 58, ; 163
	i32 271, ; 164
	i32 201, ; 165
	i32 175, ; 166
	i32 17, ; 167
	i32 305, ; 168
	i32 164, ; 169
	i32 337, ; 170
	i32 331, ; 171
	i32 327, ; 172
	i32 270, ; 173
	i32 196, ; 174
	i32 177, ; 175
	i32 345, ; 176
	i32 227, ; 177
	i32 297, ; 178
	i32 180, ; 179
	i32 333, ; 180
	i32 153, ; 181
	i32 293, ; 182
	i32 278, ; 183
	i32 180, ; 184
	i32 239, ; 185
	i32 185, ; 186
	i32 29, ; 187
	i32 52, ; 188
	i32 298, ; 189
	i32 5, ; 190
	i32 313, ; 191
	i32 288, ; 192
	i32 292, ; 193
	i32 244, ; 194
	i32 309, ; 195
	i32 236, ; 196
	i32 217, ; 197
	i32 255, ; 198
	i32 322, ; 199
	i32 85, ; 200
	i32 297, ; 201
	i32 61, ; 202
	i32 112, ; 203
	i32 342, ; 204
	i32 57, ; 205
	i32 343, ; 206
	i32 284, ; 207
	i32 99, ; 208
	i32 19, ; 209
	i32 248, ; 210
	i32 111, ; 211
	i32 101, ; 212
	i32 102, ; 213
	i32 311, ; 214
	i32 104, ; 215
	i32 301, ; 216
	i32 71, ; 217
	i32 38, ; 218
	i32 32, ; 219
	i32 103, ; 220
	i32 73, ; 221
	i32 222, ; 222
	i32 317, ; 223
	i32 9, ; 224
	i32 123, ; 225
	i32 46, ; 226
	i32 238, ; 227
	i32 199, ; 228
	i32 9, ; 229
	i32 43, ; 230
	i32 4, ; 231
	i32 285, ; 232
	i32 178, ; 233
	i32 203, ; 234
	i32 341, ; 235
	i32 31, ; 236
	i32 138, ; 237
	i32 92, ; 238
	i32 93, ; 239
	i32 224, ; 240
	i32 49, ; 241
	i32 141, ; 242
	i32 112, ; 243
	i32 140, ; 244
	i32 174, ; 245
	i32 254, ; 246
	i32 115, ; 247
	i32 302, ; 248
	i32 157, ; 249
	i32 76, ; 250
	i32 79, ; 251
	i32 274, ; 252
	i32 37, ; 253
	i32 296, ; 254
	i32 206, ; 255
	i32 258, ; 256
	i32 251, ; 257
	i32 64, ; 258
	i32 138, ; 259
	i32 15, ; 260
	i32 0, ; 261
	i32 116, ; 262
	i32 290, ; 263
	i32 299, ; 264
	i32 246, ; 265
	i32 48, ; 266
	i32 70, ; 267
	i32 80, ; 268
	i32 126, ; 269
	i32 178, ; 270
	i32 179, ; 271
	i32 94, ; 272
	i32 121, ; 273
	i32 306, ; 274
	i32 26, ; 275
	i32 218, ; 276
	i32 267, ; 277
	i32 97, ; 278
	i32 28, ; 279
	i32 242, ; 280
	i32 312, ; 281
	i32 149, ; 282
	i32 169, ; 283
	i32 4, ; 284
	i32 98, ; 285
	i32 33, ; 286
	i32 93, ; 287
	i32 289, ; 288
	i32 192, ; 289
	i32 21, ; 290
	i32 41, ; 291
	i32 170, ; 292
	i32 328, ; 293
	i32 260, ; 294
	i32 320, ; 295
	i32 200, ; 296
	i32 176, ; 297
	i32 274, ; 298
	i32 305, ; 299
	i32 299, ; 300
	i32 279, ; 301
	i32 2, ; 302
	i32 134, ; 303
	i32 111, ; 304
	i32 193, ; 305
	i32 227, ; 306
	i32 229, ; 307
	i32 337, ; 308
	i32 58, ; 309
	i32 95, ; 310
	i32 206, ; 311
	i32 319, ; 312
	i32 39, ; 313
	i32 240, ; 314
	i32 182, ; 315
	i32 25, ; 316
	i32 94, ; 317
	i32 313, ; 318
	i32 89, ; 319
	i32 99, ; 320
	i32 10, ; 321
	i32 87, ; 322
	i32 347, ; 323
	i32 324, ; 324
	i32 100, ; 325
	i32 286, ; 326
	i32 186, ; 327
	i32 306, ; 328
	i32 231, ; 329
	i32 207, ; 330
	i32 316, ; 331
	i32 7, ; 332
	i32 182, ; 333
	i32 271, ; 334
	i32 228, ; 335
	i32 201, ; 336
	i32 88, ; 337
	i32 188, ; 338
	i32 266, ; 339
	i32 154, ; 340
	i32 315, ; 341
	i32 33, ; 342
	i32 116, ; 343
	i32 82, ; 344
	i32 219, ; 345
	i32 20, ; 346
	i32 11, ; 347
	i32 162, ; 348
	i32 3, ; 349
	i32 211, ; 350
	i32 323, ; 351
	i32 197, ; 352
	i32 193, ; 353
	i32 84, ; 354
	i32 191, ; 355
	i32 310, ; 356
	i32 64, ; 357
	i32 325, ; 358
	i32 293, ; 359
	i32 143, ; 360
	i32 275, ; 361
	i32 157, ; 362
	i32 179, ; 363
	i32 205, ; 364
	i32 41, ; 365
	i32 117, ; 366
	i32 187, ; 367
	i32 230, ; 368
	i32 282, ; 369
	i32 131, ; 370
	i32 75, ; 371
	i32 66, ; 372
	i32 329, ; 373
	i32 172, ; 374
	i32 234, ; 375
	i32 143, ; 376
	i32 106, ; 377
	i32 151, ; 378
	i32 70, ; 379
	i32 323, ; 380
	i32 156, ; 381
	i32 204, ; 382
	i32 186, ; 383
	i32 121, ; 384
	i32 127, ; 385
	i32 324, ; 386
	i32 152, ; 387
	i32 257, ; 388
	i32 141, ; 389
	i32 244, ; 390
	i32 321, ; 391
	i32 20, ; 392
	i32 14, ; 393
	i32 135, ; 394
	i32 75, ; 395
	i32 59, ; 396
	i32 216, ; 397
	i32 247, ; 398
	i32 167, ; 399
	i32 168, ; 400
	i32 209, ; 401
	i32 15, ; 402
	i32 74, ; 403
	i32 6, ; 404
	i32 23, ; 405
	i32 327, ; 406
	i32 269, ; 407
	i32 228, ; 408
	i32 91, ; 409
	i32 322, ; 410
	i32 1, ; 411
	i32 136, ; 412
	i32 326, ; 413
	i32 270, ; 414
	i32 292, ; 415
	i32 134, ; 416
	i32 69, ; 417
	i32 146, ; 418
	i32 331, ; 419
	i32 310, ; 420
	i32 261, ; 421
	i32 196, ; 422
	i32 88, ; 423
	i32 96, ; 424
	i32 251, ; 425
	i32 256, ; 426
	i32 326, ; 427
	i32 31, ; 428
	i32 223, ; 429
	i32 45, ; 430
	i32 265, ; 431
	i32 181, ; 432
	i32 204, ; 433
	i32 173, ; 434
	i32 225, ; 435
	i32 230, ; 436
	i32 109, ; 437
	i32 158, ; 438
	i32 35, ; 439
	i32 22, ; 440
	i32 174, ; 441
	i32 114, ; 442
	i32 57, ; 443
	i32 290, ; 444
	i32 144, ; 445
	i32 118, ; 446
	i32 120, ; 447
	i32 110, ; 448
	i32 232, ; 449
	i32 139, ; 450
	i32 238, ; 451
	i32 200, ; 452
	i32 312, ; 453
	i32 54, ; 454
	i32 105, ; 455
	i32 332, ; 456
	i32 214, ; 457
	i32 183, ; 458
	i32 210, ; 459
	i32 211, ; 460
	i32 133, ; 461
	i32 304, ; 462
	i32 295, ; 463
	i32 283, ; 464
	i32 338, ; 465
	i32 261, ; 466
	i32 215, ; 467
	i32 213, ; 468
	i32 159, ; 469
	i32 248, ; 470
	i32 163, ; 471
	i32 132, ; 472
	i32 283, ; 473
	i32 161, ; 474
	i32 272, ; 475
	i32 226, ; 476
	i32 181, ; 477
	i32 140, ; 478
	i32 295, ; 479
	i32 291, ; 480
	i32 169, ; 481
	i32 212, ; 482
	i32 0, ; 483
	i32 225, ; 484
	i32 233, ; 485
	i32 300, ; 486
	i32 40, ; 487
	i32 259, ; 488
	i32 81, ; 489
	i32 56, ; 490
	i32 37, ; 491
	i32 97, ; 492
	i32 166, ; 493
	i32 345, ; 494
	i32 172, ; 495
	i32 296, ; 496
	i32 82, ; 497
	i32 235, ; 498
	i32 98, ; 499
	i32 30, ; 500
	i32 159, ; 501
	i32 220, ; 502
	i32 18, ; 503
	i32 127, ; 504
	i32 198, ; 505
	i32 119, ; 506
	i32 255, ; 507
	i32 286, ; 508
	i32 268, ; 509
	i32 220, ; 510
	i32 288, ; 511
	i32 165, ; 512
	i32 263, ; 513
	i32 223, ; 514
	i32 348, ; 515
	i32 318, ; 516
	i32 285, ; 517
	i32 276, ; 518
	i32 170, ; 519
	i32 16, ; 520
	i32 184, ; 521
	i32 144, ; 522
	i32 207, ; 523
	i32 125, ; 524
	i32 118, ; 525
	i32 38, ; 526
	i32 194, ; 527
	i32 115, ; 528
	i32 47, ; 529
	i32 142, ; 530
	i32 117, ; 531
	i32 34, ; 532
	i32 175, ; 533
	i32 95, ; 534
	i32 53, ; 535
	i32 277, ; 536
	i32 129, ; 537
	i32 153, ; 538
	i32 184, ; 539
	i32 24, ; 540
	i32 161, ; 541
	i32 254, ; 542
	i32 226, ; 543
	i32 148, ; 544
	i32 104, ; 545
	i32 89, ; 546
	i32 242, ; 547
	i32 60, ; 548
	i32 142, ; 549
	i32 100, ; 550
	i32 5, ; 551
	i32 13, ; 552
	i32 122, ; 553
	i32 135, ; 554
	i32 28, ; 555
	i32 318, ; 556
	i32 203, ; 557
	i32 72, ; 558
	i32 252, ; 559
	i32 24, ; 560
	i32 240, ; 561
	i32 281, ; 562
	i32 278, ; 563
	i32 335, ; 564
	i32 137, ; 565
	i32 217, ; 566
	i32 233, ; 567
	i32 249, ; 568
	i32 168, ; 569
	i32 205, ; 570
	i32 282, ; 571
	i32 314, ; 572
	i32 101, ; 573
	i32 123, ; 574
	i32 253, ; 575
	i32 188, ; 576
	i32 189, ; 577
	i32 163, ; 578
	i32 167, ; 579
	i32 256, ; 580
	i32 39, ; 581
	i32 208, ; 582
	i32 333, ; 583
	i32 17, ; 584
	i32 171, ; 585
	i32 334, ; 586
	i32 137, ; 587
	i32 150, ; 588
	i32 245, ; 589
	i32 155, ; 590
	i32 130, ; 591
	i32 19, ; 592
	i32 65, ; 593
	i32 177, ; 594
	i32 147, ; 595
	i32 173, ; 596
	i32 47, ; 597
	i32 224, ; 598
	i32 342, ; 599
	i32 344, ; 600
	i32 231, ; 601
	i32 79, ; 602
	i32 61, ; 603
	i32 106, ; 604
	i32 280, ; 605
	i32 235, ; 606
	i32 49, ; 607
	i32 266, ; 608
	i32 339, ; 609
	i32 277, ; 610
	i32 14, ; 611
	i32 187, ; 612
	i32 68, ; 613
	i32 171, ; 614
	i32 241, ; 615
	i32 245, ; 616
	i32 221, ; 617
	i32 202, ; 618
	i32 78, ; 619
	i32 250, ; 620
	i32 108, ; 621
	i32 234, ; 622
	i32 276, ; 623
	i32 67, ; 624
	i32 63, ; 625
	i32 27, ; 626
	i32 160, ; 627
	i32 314, ; 628
	i32 219, ; 629
	i32 243, ; 630
	i32 10, ; 631
	i32 208, ; 632
	i32 11, ; 633
	i32 78, ; 634
	i32 126, ; 635
	i32 83, ; 636
	i32 190, ; 637
	i32 66, ; 638
	i32 107, ; 639
	i32 65, ; 640
	i32 128, ; 641
	i32 122, ; 642
	i32 77, ; 643
	i32 291, ; 644
	i32 281, ; 645
	i32 8, ; 646
	i32 249, ; 647
	i32 2, ; 648
	i32 330, ; 649
	i32 44, ; 650
	i32 294, ; 651
	i32 156, ; 652
	i32 128, ; 653
	i32 279, ; 654
	i32 23, ; 655
	i32 133, ; 656
	i32 237, ; 657
	i32 268, ; 658
	i32 29, ; 659
	i32 236, ; 660
	i32 62, ; 661
	i32 210, ; 662
	i32 90, ; 663
	i32 87, ; 664
	i32 148, ; 665
	i32 195, ; 666
	i32 316, ; 667
	i32 212, ; 668
	i32 36, ; 669
	i32 86, ; 670
	i32 257, ; 671
	i32 185, ; 672
	i32 340, ; 673
	i32 189, ; 674
	i32 50, ; 675
	i32 6, ; 676
	i32 90, ; 677
	i32 340, ; 678
	i32 21, ; 679
	i32 162, ; 680
	i32 96, ; 681
	i32 50, ; 682
	i32 113, ; 683
	i32 273, ; 684
	i32 130, ; 685
	i32 183, ; 686
	i32 76, ; 687
	i32 27, ; 688
	i32 329, ; 689
	i32 250, ; 690
	i32 272, ; 691
	i32 7, ; 692
	i32 222, ; 693
	i32 209, ; 694
	i32 110, ; 695
	i32 273, ; 696
	i32 259 ; 697
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ f68622cf6b97fa23cc3d3105a52ef5b2e31d52a1"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
