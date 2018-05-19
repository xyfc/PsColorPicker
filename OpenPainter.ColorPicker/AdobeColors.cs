// AdobeColors.cs
// 
// Copyright (c) 2007-2010, OpenPainter.org, and based on the work of
//               2005 Danny Blanchard (scrabcakes@gmail.com)
// 
// The contents of this file are subject to the Mozilla Public License
// Version 1.1 (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
// 
// http://www.mozilla.org/MPL/
// 
// Software distributed under the License is distributed on an "AS IS"
// basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
// the License for the specific language governing rights and limitations
// under the License.
// 
// The Original Code is OpenPainter.
// 
// The Initial Developer of the Original Code is OpenPainter.org.
// All Rights Reserved.

//---------------------------------------------------------------------------
// Project:           Adobe Color Picker Clone 1
// Filename:          AdobeColors.cs
// Original Author:   Danny Blanchard
//                    - scrabcakes@gmail.com
// Updates:
//  3/28/2005 - Initial Version : Danny Blanchard
//       2010 - Updated by OpenPainter.org
//---------------------------------------------------------------------------

using System;
using System.Drawing;

namespace OpenPainter.ColorPicker
{
	/// <summary>
	/// Summary description for AdobeColors.
	/// </summary>
	public class AdobeColors
	{
		#region Constructors / Destructors

		public AdobeColors()
		{
		}


		#endregion

		#region Public Methods

		/// <summary> 
		/// Sets the absolute brightness of a colour 
		/// </summary> 
		/// <param name="c">Original colour</param> 
		/// <param name="brightness">The luminance level to impose</param> 
		/// <returns>an adjusted colour</returns> 
		public static Color SetBrightness(Color c, double brightness)
		{
			HSB hsl = RGB_to_HSB(c);
			hsl.B = brightness;
			return HSB_to_RGB(hsl);
		}


		/// <summary> 
		/// Modifies an existing brightness level 
		/// </summary> 
		/// <remarks> 
		/// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tnan 1 
		/// </remarks> 
		/// <param name="c">The original colour</param> 
		/// <param name="brightness">The luminance delta</param> 
		/// <returns>An adjusted colour</returns> 
		public static Color ModifyBrightness(Color c, double brightness)
		{
			HSB hsl = RGB_to_HSB(c);
			hsl.B *= brightness;
			return HSB_to_RGB(hsl);
		}


		/// <summary> 
		/// Sets the absolute saturation level 
		/// </summary> 
		/// <remarks>Accepted values 0-1</remarks> 
		/// <param name="c">An original colour</param> 
		/// <param name="Saturation">The saturation value to impose</param> 
		/// <returns>An adjusted colour</returns> 
		public static Color SetSaturation(Color c, double Saturation)
		{
			HSB hsl = RGB_to_HSB(c);
			hsl.S = Saturation;
			return HSB_to_RGB(hsl);
		}


		/// <summary> 
		/// Modifies an existing Saturation level 
		/// </summary> 
		/// <remarks> 
		/// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tnan 1 
		/// </remarks> 
		/// <param name="c">The original colour</param> 
		/// <param name="Saturation">The saturation delta</param> 
		/// <returns>An adjusted colour</returns> 
		public static Color ModifySaturation(Color c, double Saturation)
		{
			HSB hsl = RGB_to_HSB(c);
			hsl.S *= Saturation;
			return HSB_to_RGB(hsl);
		}


		/// <summary> 
		/// Sets the absolute Hue level 
		/// </summary> 
		/// <remarks>Accepted values 0-1</remarks> 
		/// <param name="c">An original colour</param> 
		/// <param name="Hue">The Hue value to impose</param> 
		/// <returns>An adjusted colour</returns> 
		public static Color SetHue(Color c, double Hue)
		{
			HSB hsl = RGB_to_HSB(c);
			hsl.H = Hue;
			return HSB_to_RGB(hsl);
		}


		/// <summary> 
		/// Modifies an existing Hue level 
		/// </summary> 
		/// <remarks> 
		/// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tnan 1 
		/// </remarks> 
		/// <param name="c">The original colour</param> 
		/// <param name="Hue">The Hue delta</param> 
		/// <returns>An adjusted colour</returns> 
		public static Color ModifyHue(Color c, double Hue)
		{
			HSB hsl = RGB_to_HSB(c);
			hsl.H *= Hue;
			return HSB_to_RGB(hsl);
		}


		/// <summary> 
		/// Converts a colour from HSL to RGB 
		/// </summary> 
		/// <remarks>Adapted from the algoritm in Foley and Van-Dam</remarks> 
		/// <param name="hsb">The HSL value</param> 
		/// <returns>A Color structure containing the equivalent RGB values</returns> 
		public static Color HSB_to_RGB(HSB hsb)
		{
			int Max, Mid, Min;
			double q;

			Max = (int)Math.Round(hsb.B * 255);
			Min = (int)Math.Round((1.0 - hsb.S) * (hsb.B / 1.0) * 255);
			q = (double)(Max - Min) / 255;

			if (hsb.H >= 0 && hsb.H <= (double)1 / 6)
			{
				Mid = (int)Math.Round(((hsb.H - 0) * q) * 1530 + Min);
				return Color.FromArgb(Max, Mid, Min);
			}
			else if (hsb.H <= (double)1 / 3)
			{
				Mid = (int)Math.Round(-((hsb.H - (double)1 / 6) * q) * 1530 + Max);
				return Color.FromArgb(Mid, Max, Min);
			}
			else if (hsb.H <= 0.5)
			{
				Mid = (int)Math.Round(((hsb.H - (double)1 / 3) * q) * 1530 + Min);
				return Color.FromArgb(Min, Max, Mid);
			}
			else if (hsb.H <= (double)2 / 3)
			{
				Mid = (int)Math.Round(-((hsb.H - 0.5) * q) * 1530 + Max);
				return Color.FromArgb(Min, Mid, Max);
			}
			else if (hsb.H <= (double)5 / 6)
			{
				Mid = (int)Math.Round(((hsb.H - (double)2 / 3) * q) * 1530 + Min);
				return Color.FromArgb(Mid, Min, Max);
			}
			else if (hsb.H <= 1.0)
			{
				Mid = (int)Math.Round(-((hsb.H - (double)5 / 6) * q) * 1530 + Max);
				return Color.FromArgb(Max, Min, Mid);
			}
			else return Color.FromArgb(0, 0, 0);
		}


		/// <summary> 
		/// Converts RGB to HSL 
		/// </summary> 
		/// <remarks>Takes advantage of whats already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods</remarks> 
		/// <param name="c">A Color to convert</param> 
		/// <returns>An HSL value</returns> 
		public static HSB RGB_to_HSB(Color c)
		{
			HSB hsl = new HSB();

			int Max, Min, Diff, Sum;

			//	Of our RGB values, assign the highest value to Max, and the Smallest to Min
			if (c.R > c.G) { Max = c.R; Min = c.G; }
			else { Max = c.G; Min = c.R; }
			if (c.B > Max) Max = c.B;
			else if (c.B < Min) Min = c.B;

			Diff = Max - Min;
			Sum = Max + Min;

			//	Luminance - a.k.a. Brightness - Adobe photoshop uses the logic that the
			//	site VBspeed regards (regarded) as too primitive = superior decides the 
			//	level of brightness.
			hsl.B = (double)Max / 255;

			//	Saturation
			if (Max == 0) hsl.S = 0;    //	Protecting from the impossible operation of division by zero.
			else hsl.S = (double)Diff / Max;    //	The logic of Adobe Photoshops is this simple.

			//	Hue		R is situated at the angel of 360 eller noll degrees; 
			//			G vid 120 degrees
			//			B vid 240 degrees
			double q;
			if (Diff == 0) q = 0; // Protecting from the impossible operation of division by zero.
			else q = (double)60 / Diff;

			if (Max == c.R)
			{
				if (c.G < c.B) hsl.H = (double)(360 + q * (c.G - c.B)) / 360;
				else hsl.H = (double)(q * (c.G - c.B)) / 360;
			}
			else if (Max == c.G) hsl.H = (double)(120 + q * (c.B - c.R)) / 360;
			else if (Max == c.B) hsl.H = (double)(240 + q * (c.R - c.G)) / 360;
			else hsl.H = 0.0;

			return hsl;
		}


		/// <summary>
		/// Converts RGB to CMYK
		/// </summary>
		/// <param name="c">A color to convert.</param>
		/// <returns>A CMYK object</returns>
		public static CMYK RGB_to_CMYK(Color c)
		{
			CMYK _cmyk = new CMYK();
			double low = 1.0;

			_cmyk.C = (double)(255 - c.R) / 255;
			if (low > _cmyk.C)
				low = _cmyk.C;

			_cmyk.M = (double)(255 - c.G) / 255;
			if (low > _cmyk.M)
				low = _cmyk.M;

			_cmyk.Y = (double)(255 - c.B) / 255;
			if (low > _cmyk.Y)
				low = _cmyk.Y;

			if (low > 0.0)
			{
				_cmyk.K = low;
			}

			return _cmyk;
		}


		/// <summary>
		/// Converts CMYK to RGB
		/// </summary>
		/// <param name="cmyk">A color to convert</param>
		/// <returns>A Color object</returns>
		public static Color CmykToRgb(CMYK cmyk)
		{
			int r = (int)Math.Round(255 * (1 - cmyk.C));
			int g = (int)Math.Round(255 * (1 - cmyk.M));
			int b = (int)Math.Round(255 * (1 - cmyk.Y));

			return Color.FromArgb(r, g, b);
		}

		/// <summary>
		/// Get nearest web safe color based on the given RGB color.
		/// </summary>
		/// <param name="color"></param>
		/// <returns></returns>
		public static Color GetNearestWebSafeColor(Color color)
		{
			int r = (int)(Math.Round(color.R / 255.0 * 5) / 5 * 255);
			int g = (int)(Math.Round(color.G / 255.0 * 5) / 5 * 255);
			int b = (int)(Math.Round(color.B / 255.0 * 5) / 5 * 255);

			return Color.FromArgb(r, g, b);
		}

		#endregion

		#region Public Classes

		public class HSB
		{
			#region Class Variables

			public HSB()
			{
				_h = 0;
				_s = 0;
				_b = 0;
			}

			public HSB(double h, double s, double b)
			{
				_h = h;
				_s = s;
				_b = b;
			}

			double _h;
			double _s;
			double _b;

			#endregion

			#region Public Methods

			public double H
			{
				get { return _h; }
				set
				{
					_h = value;
					_h = _h > 1 ? 1 : _h < 0 ? 0 : _h;
				}
			}


			public double S
			{
				get { return _s; }
				set
				{
					_s = value;
					_s = _s > 1 ? 1 : _s < 0 ? 0 : _s;
				}
			}


			public double B
			{
				get { return _b; }
				set
				{
					_b = value;
					_b = _b > 1 ? 1 : _b < 0 ? 0 : _b;
				}
			}


			#endregion
		}

		public class CMYK
		{
			#region Class Variables

			public CMYK()
			{
				_c = 0;
				_m = 0;
				_y = 0;
				_k = 0;
			}


			double _c;
			double _m;
			double _y;
			double _k;

			#endregion

			#region Public Methods

			public double C
			{
				get { return _c; }
				set
				{
					_c = value;
					_c = _c > 1 ? 1 : _c < 0 ? 0 : _c;
				}
			}


			public double M
			{
				get { return _m; }
				set
				{
					_m = value;
					_m = _m > 1 ? 1 : _m < 0 ? 0 : _m;
				}
			}


			public double Y
			{
				get { return _y; }
				set
				{
					_y = value;
					_y = _y > 1 ? 1 : _y < 0 ? 0 : _y;
				}
			}


			public double K
			{
				get { return _k; }
				set
				{
					_k = value;
					_k = _k > 1 ? 1 : _k < 0 ? 0 : _k;
				}
			}


			#endregion
		}

		#endregion
	}
}
