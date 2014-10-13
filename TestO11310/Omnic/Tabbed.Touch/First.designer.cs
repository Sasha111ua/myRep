// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace Tabbed.Touch
{
	[Register ("First")]
	partial class First
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton But { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableView Tabl { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (But != null) {
				But.Dispose ();
				But = null;
			}
			if (Tabl != null) {
				Tabl.Dispose ();
				Tabl = null;
			}
		}
	}
}
