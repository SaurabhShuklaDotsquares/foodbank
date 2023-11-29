
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//The MIT License (MIT)

//Copyright (c) 2014 Craig Moore - Deadline Automation Limited

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

namespace FB.ExportReportLabel
{
    /// <summary>
    /// Represents the layout of a sheet of labels such as Avery L7163.
    /// </summary>
    /// <remarks>All dimensions in Millimeters</remarks>
    public class LabelFormat
    {
        #region Properties
       
        
        public double TopMargin { get; set; }
        /// <summary>
        /// Margin between left of page and left of first label in millimeters
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LeftMargin { get; set; }
        /// <summary>
        /// Width of individual label in millimeters
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LabelWidth { get; set; }
        /// <summary>
        /// Height of individual label in millimeters
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LabelHeight { get; set; }
        /// <summary>
        /// Padding on the left of an individual label, creates space between label edge and start of content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LabelPaddingLeft { get; set; }
        /// <summary>
        /// Padding on the Right of an individual label, creates space between label edge and end of content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LabelPaddingRight { get; set; }
        /// <summary>
        /// Padding on the top of an individual label, creates space between label edge and start of content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LabelPaddingTop { get; set; }
        /// <summary>
        /// Padding on the Bottom of an individual label, creates space between label edge and end of content
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double LabelPaddingBottom { get; set; }
        /// <summary>
        /// Distance between top of one label and top of label below it in millimeters
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double VerticalPitch { get; set; }
        /// <summary>
        /// Distance between left of one label and left of label to the right of it in millimeters
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double HorizontalPitch { get; set; }
        /// <summary>
        /// Number of labels going across the page
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int ColumnCount { get; set; }
        /// <summary>
        /// Number of labels going down the page
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int RowCount { get; set; }

        /// <summary>
        /// Instantiate a new label sheet format definition
        /// </summary>
        /// <remarks></remarks> 
        #endregion

        public LabelFormat()
        {
        }

        /// <summary>
        /// Instantiate a new label sheet format definition
        /// </summary>
        /// <param name="Id">Numerical Id of the label format</param>
        /// <param name="Name">Name of the label format (e.g. Avery L7163)</param>
        /// <param name="Description">Description of label format (e.g. A4 Sheet of 99.1 x 38.1mm address labels)</param>
        /// <param name="PageWidth">Width of page in millimeters</param>
        /// <param name="PageHeight">Height of page in millimeters</param>
        /// <param name="TopMargin">Margin between top of page and top of first label in millimeters</param>
        /// <param name="LeftMargin">Margin between left of page and left of first label in millimeters</param>
        /// <param name="LabelWidth">Width of individual label in millimeters</param>
        /// <param name="LabelHeight">Height of individual label in millimeters</param>
        /// <param name="VerticalPitch">Distance between top of one label and top of label below it in millimeters</param>
        /// <param name="HorizontalPitch">Distance between left of one label and left of label to the right of it in millimeters</param>
        /// <param name="ColumnCount">Number of labels going across the page</param>
        /// <param name="RowCount">Number of labels going down the page</param>
        /// <param name="LabelPaddingLeft">Padding on the left of an individual label, creates space between label edge and start of content</param>
        /// <param name="LabelPaddingRight">Padding on the Right of an individual label, creates space between label edge and end of content</param>
        /// <param name="LabelPaddingTop">Padding on the top of an individual label, creates space between label edge and start of content</param>
        /// <param name="LabelPaddingBottom">Padding on the Bottom of an individual label, creates space between label edge and end of content</param>
        /// <remarks></remarks>
        public LabelFormat(double TopMargin, double LeftMargin, double LabelWidth, double LabelHeight, double VerticalPitch,
        double HorizontalPitch, int ColumnCount, int RowCount, double LabelPaddingLeft = 0.0, double LabelPaddingRight = 0.0, double LabelPaddingTop = 0.0, double LabelPaddingBottom = 0.0)
        {        
            this.TopMargin = TopMargin;
            this.LeftMargin = LeftMargin;
            this.LabelWidth = LabelWidth;
            this.LabelHeight = LabelHeight;
            this.VerticalPitch = VerticalPitch;
            this.HorizontalPitch = HorizontalPitch;
            this.ColumnCount = ColumnCount;
            this.RowCount = RowCount;
            this.LabelPaddingLeft = LabelPaddingLeft;
            this.LabelPaddingRight = LabelPaddingRight;
            this.LabelPaddingTop = LabelPaddingTop;
            this.LabelPaddingBottom = LabelPaddingBottom;
        }

    }
}
