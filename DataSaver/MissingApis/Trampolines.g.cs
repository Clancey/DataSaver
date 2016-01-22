//
// Auto-generated from generator.cs, do not edit
//
// We keep references to objects, so warning 414 is expected

#pragma warning disable 414

using System;
using System.Drawing;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Foundation;
using ObjCRuntime;
namespace ObjCRuntime {
	
	[CompilerGenerated]
	static partial class Trampolines {
		
		[DllImport ("/usr/lib/libobjc.dylib")]
		static extern IntPtr _Block_copy (IntPtr ptr);
		
		[DllImport ("/usr/lib/libobjc.dylib")]
		static extern void _Block_release (IntPtr ptr);
		
		[UnmanagedFunctionPointerAttribute (CallingConvention.Cdecl)]
		internal delegate void DNSUserAutomatorTaskCompletionHandler (IntPtr block, IntPtr result, IntPtr error);
		
		//
		// This class bridges native block invocations that call into C#
		//
		static internal class SDNSUserAutomatorTaskCompletionHandler {
			static internal readonly DNSUserAutomatorTaskCompletionHandler Handler = Invoke;
			
			[MonoPInvokeCallback (typeof (DNSUserAutomatorTaskCompletionHandler))]
			static unsafe void Invoke (IntPtr block, IntPtr result, IntPtr error) {
				var descriptor = (BlockLiteral *) block;
				var del = (global::Foundation.NSUserAutomatorTaskCompletionHandler) (descriptor->Target);
				if (del != null)
					del ( Runtime.GetNSObject<NSObject> (result),  Runtime.GetNSObject<NSError> (error));
			}
		} /* class SDNSUserAutomatorTaskCompletionHandler */
		
		internal class NIDNSUserAutomatorTaskCompletionHandler {
			IntPtr blockPtr;
			DNSUserAutomatorTaskCompletionHandler invoker;
			
			[Preserve (Conditional=true)]
			public unsafe NIDNSUserAutomatorTaskCompletionHandler (BlockLiteral *block)
			{
				blockPtr = _Block_copy ((IntPtr) block);
				invoker = block->GetDelegateForBlock<DNSUserAutomatorTaskCompletionHandler> ();
			}
			
			[Preserve (Conditional=true)]
			~NIDNSUserAutomatorTaskCompletionHandler ()
			{
				_Block_release (blockPtr);
			}
			
			[Preserve (Conditional=true)]
			public unsafe static global::Foundation.NSUserAutomatorTaskCompletionHandler Create (IntPtr block)
			{
				if (block == IntPtr.Zero)
					return null;
				if (BlockLiteral.IsManagedBlock (block)) {
					var existing_delegate = ((BlockLiteral *) block)->Target as global::Foundation.NSUserAutomatorTaskCompletionHandler;
					if (existing_delegate != null)
						return existing_delegate;
				}
				return new NIDNSUserAutomatorTaskCompletionHandler ((BlockLiteral *) block).Invoke;
			}
			
			[Preserve (Conditional=true)]
			unsafe void Invoke (NSObject result, NSError error)
			{
				invoker (blockPtr, result == null ? IntPtr.Zero : result.Handle, error == null ? IntPtr.Zero : error.Handle);
			}
		} /* class NIDNSUserAutomatorTaskCompletionHandler */
	}
}
