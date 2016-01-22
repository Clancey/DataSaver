using System;
using System.IO;
using NGraphics;
using System.Linq;
using CoreGraphics;

namespace AppKit
{
	public static class NGraphicsExtensions
	{
		static readonly IPlatform Platform = new ApplePlatform();
		static readonly double Scale = (double) Math.Max(NSScreen.Screens.Max(x => x.BackingScaleFactor),2)	;

		public static void LoadSvg(this NSImageView imageView, string svg)
		{
			if (string.IsNullOrWhiteSpace (svg))
				return;
			var s = imageView.Bounds.Size;
			LoadSvg(imageView, svg, new Size(s.Width, s.Height));
		}

		public static void LoadSvg(this NSImageView imageView, string svg, Size size , NSColor color = null)
		{
			var image = svg.LoadImageFromSvg(size,color);
			imageView.Image = image;
		}

		public static NSImage LoadImageFromSvg(this string svg, Size size,NSColor color = null)
		{
			try
			{
				var fileName = System.IO.Path.GetFileNameWithoutExtension(svg);
				using (var file = File.OpenText(svg))
				{
					var graphic = Graphic.LoadSvg(file);
					//Shame on Size not being Equatable ;)
					if (size.Width <= 0 || size.Height <= 0)
						size = graphic.Size;
					var gSize = graphic.Size;
					if (gSize.Width > size.Width || size.Height > gSize.Height)
					{
						var ratioX = size.Width/gSize.Width;
						var ratioY = size.Height/gSize.Height;
						var ratio = Math.Min(ratioY, ratioX);
						graphic.Size = size = new Size(gSize.Width*ratio, gSize.Height*ratio);
					}
					var c = Platform.CreateImageCanvas(size, Scale);
					graphic.Draw(c);
					var image = c.GetImage().GetNSImage();
					if(color != null)
					{
						image.LockFocus();
						color.Set();
						var rect = new CGRect(CGPoint.Empty, image.Size);
						NSGraphics.RectFill(rect, NSCompositingOperation.SourceAtop);
						image.UnlockFocus();
					}
					image.AccessibilityDescription = fileName;
					return image;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Console.WriteLine("Failed parsing: {0}", svg);
				throw;
			}
		}
	}
}