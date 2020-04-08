#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System.Collections.Generic;
using System.Drawing;

namespace VPKSoft.SpellCheckUtility.WinForms.UtilityClasses
{
    /// <summary>
    /// A class to help draw a zig zag line with <see cref="System.Drawing"/> name space.
    /// </summary>
    public class ZigZagLine
    {
        /// <summary>
        /// Gets points to draw a zig zag line.
        /// </summary>
        /// <param name="x">The X coordinate of the drawing area.</param>
        /// <param name="y">The Y coordinate of the drawing area.</param>
        /// <param name="width">The width of the line.</param>
        /// <param name="heightInPixels">The height in pixels for the line.</param>
        /// <param name="zigZagHalfWidth">The zig zag half width in pixels for the line.</param>
        /// <returns>An array of <see cref="Point"/> to be used with <see cref="Graphics.DrawLines(Pen, Point[])"/> instance method.</returns>
        public Point[] GetZigZagLinePoints(int x, int y, int width, int heightInPixels = 3, int zigZagHalfWidth = 3)
        {
            var result = new List<Point>();
            var down = true;

            for (int i = x; i <= x + width; i += zigZagHalfWidth, down = !down)
            {
                result.Add(new Point { X = i, Y = down ? y : y + heightInPixels});
            }

            return result.ToArray();
        }
    }
}
