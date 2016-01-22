//
// Auto-generated from generator.cs, do not edit
//
// We keep references to objects, so warning 414 is expected

#pragma warning disable 414

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Foundation;
using ObjCRuntime;

namespace Foundation {
	[Register("NSUserAutomatorTask", true)]
	public partial class NSUserAutomatorTask : NSUserScriptTask {
		
		[CompilerGenerated]
		static readonly IntPtr class_ptr = Class.GetHandle ("NSUserAutomatorTask");
		
		public override IntPtr ClassHandle { get { return class_ptr; } }
		
		[CompilerGenerated]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		[Export ("init")]
		public NSUserAutomatorTask () : base (NSObjectFlag.Empty)
		{
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
			if (IsDirectBinding) {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSend (this.Handle, global::ObjCRuntime.Selector.GetHandle ("init")), "init");
			} else {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper (this.SuperHandle, global::ObjCRuntime.Selector.GetHandle ("init")), "init");
			}
		}

		[CompilerGenerated]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		protected NSUserAutomatorTask (NSObjectFlag t) : base (t)
		{
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
		}

		[CompilerGenerated]
		[EditorBrowsable (EditorBrowsableState.Advanced)]
		protected internal NSUserAutomatorTask (IntPtr handle) : base (handle)
		{
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
		}

		[Export ("initWithURL:error:")]
		[CompilerGenerated]
		public NSUserAutomatorTask (NSUrl url, NSError error)
			: base (NSObjectFlag.Empty)
		{
			if (url == null)
				throw new ArgumentNullException ("url");
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
			if (IsDirectBinding) {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSend_IntPtr_IntPtr (this.Handle, Selector.GetHandle ("initWithURL:error:"), url.Handle, error == null ? IntPtr.Zero : error.Handle), "initWithURL:error:");
			} else {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper_IntPtr_IntPtr (this.SuperHandle, Selector.GetHandle ("initWithURL:error:"), url.Handle, error == null ? IntPtr.Zero : error.Handle), "initWithURL:error:");
			}
		}
		
		[Export ("executeWithInput:completionHandler:")]
		[CompilerGenerated]
		public unsafe virtual void Execute (NSObject coding, [BlockProxy (typeof (ObjCRuntime.Trampolines.NIDNSUserAutomatorTaskCompletionHandler))]NSUserAutomatorTaskCompletionHandler callback)
		{
			BlockLiteral *block_ptr_callback;
			BlockLiteral block_callback;
			if (callback == null){
				block_ptr_callback = null;
			} else {
				block_callback = new BlockLiteral ();
				block_ptr_callback = &block_callback;
				block_callback.SetupBlock (Trampolines.SDNSUserAutomatorTaskCompletionHandler.Handler, callback);
			}
			
			if (IsDirectBinding) {
				global::ApiDefinition.Messaging.void_objc_msgSend_IntPtr_IntPtr (this.Handle, Selector.GetHandle ("executeWithInput:completionHandler:"), coding == null ? IntPtr.Zero : coding.Handle, (IntPtr) block_ptr_callback);
			} else {
				global::ApiDefinition.Messaging.void_objc_msgSendSuper_IntPtr_IntPtr (this.SuperHandle, Selector.GetHandle ("executeWithInput:completionHandler:"), coding == null ? IntPtr.Zero : coding.Handle, (IntPtr) block_ptr_callback);
			}
			if (block_ptr_callback != null)
				block_ptr_callback->CleanupBlock ();
			
		}
		
	} /* class NSUserAutomatorTask */
}
