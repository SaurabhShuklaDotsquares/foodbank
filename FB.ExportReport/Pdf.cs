using System;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Web;
using System.Threading;
using System.Collections.Generic;

namespace FB.ExportReport
{
    public class Pdf : IPdf
    {
        public Pdf()
        {
            // OutputResult = new PdfOutput() { OutputStream=new MemoryStream() };
        }

        public PdfDocument Document { get; set; }
        public PdfOutput OutputResult { get; set; }

        public PdfConvertEnvironment ConvertEnvironment { get; set; }

        //private static PdfConvertEnvironment _e;

        //private PdfConvertEnvironment Environment
        //{
        //    get
        //    {
        //        if (_e == null)
        //            _e = new PdfConvertEnvironment
        //            {
        //                TempFolderPath = Path.Combine(this.HostEnvironment.WebRootPath, "TempFolder"),
        //                WkHtmlToPdfPath = Path.Combine(this.HostEnvironment.WebRootPath, "TempFolder/wkhtmltopdf.exe"),
        //                Timeout = 60000
        //            };
        //        return _e;
        //    }
        //}

        public void GeneratePDF()
        {
            if (OutputResult == null)
            {
                OutputResult = new PdfOutput() { OutputStream = new MemoryStream() };
            }

            ConvertHtmlToPdf(Document, null, OutputResult);
        }

        public void GeneratePDF(PdfConvertEnvironment environment)
        {
            ConvertHtmlToPdf(Document, environment, OutputResult);
        }

        private void ConvertHtmlToPdf(PdfDocument document, PdfConvertEnvironment environment, PdfOutput woutput)
        {

            if (document == null)
                throw new PdfConvertException(
                    String.Format("You must supply a pdf document, it can't be null", document.Url)
                );

            if (document.Url == "-" && document.Html == null)
                throw new PdfConvertException(
                    String.Format("You must supply a HTML string, if you have enterd the url: {0}", document.Url)
                );

            if (environment == null)
                environment = ConvertEnvironment;

            String outputPdfFilePath;
            bool delete;
            if (woutput.OutputFilePath != null)
            {
                outputPdfFilePath = woutput.OutputFilePath;
                delete = false;
            }
            else
            {
                Guid pdfname = Guid.NewGuid();
                outputPdfFilePath = Path.Combine(environment.TempFolderPath, String.Format("{0}.pdf", pdfname));
                OutputResult.OutputFilePath = pdfname.ToString();
                delete = true;
            }

            if (!File.Exists(environment.WkHtmlToPdfPath))
                throw new PdfConvertException(String.Format("File '{0}' not found. Check if wkhtmltopdf application is installed.", environment.WkHtmlToPdfPath));

            StringBuilder paramsBuilder = new StringBuilder();

            paramsBuilder.Append("--enable-local-file-access ");

            if (!string.IsNullOrWhiteSpace(document.PageSize))
                paramsBuilder.Append("--page-size " + document.PageSize + " ");
            else
                paramsBuilder.Append("--page-size Letter ");

            if (!string.IsNullOrWhiteSpace(document.MarginTop))
                paramsBuilder.Append("--margin-top " + document.MarginTop + " ");
            else
                paramsBuilder.Append("--margin-top 5 ");

            if (!string.IsNullOrWhiteSpace(document.MarginBottom))
                paramsBuilder.Append("--margin-bottom " + document.MarginBottom + " ");

            if (!string.IsNullOrWhiteSpace(document.MarginLeft))
                paramsBuilder.Append("--margin-left " + document.MarginLeft + " ");

            if (!string.IsNullOrWhiteSpace(document.MarginRight))
                paramsBuilder.Append("--margin-right " + document.MarginRight + " ");

            if (document.IsLandScape)
                paramsBuilder.Append("--orientation Landscape ");

            if (!string.IsNullOrEmpty(document.HeaderUrl))
            {
                paramsBuilder.AppendFormat("--header-html {0} ", document.HeaderUrl);
                paramsBuilder.Append("--margin-top 25 ");
                paramsBuilder.Append("--header-spacing 5 ");
            }

            if (!string.IsNullOrEmpty(document.FooterUrl))
            {
                var footerUrl = document.FooterUrl + "?";
                if (!string.IsNullOrEmpty(document.FooterCenter))
                    footerUrl += string.Format("footer-center=\"{0}\"", document.FooterCenter);

                paramsBuilder.AppendFormat("--footer-html {0} ", footerUrl);
                paramsBuilder.Append("--margin-bottom 25 ");
                paramsBuilder.Append("--footer-spacing 5 ");
            }

            //if (!string.IsNullOrEmpty(document.FooterUrl))
            //{
            //    paramsBuilder.AppendFormat("--footer-html {0} ", document.FooterUrl);
            //    paramsBuilder.Append("--margin-bottom 25 ");
            //    paramsBuilder.Append("--footer-spacing 5 ");
            //}

            if (document.FooterLine.HasValue)
                if (document.FooterLine.Value)
                    paramsBuilder.Append("--footer-line ");

            if (!string.IsNullOrEmpty(document.HeaderLeft))
                paramsBuilder.AppendFormat("--header-left \"{0}\" ", document.HeaderLeft);

            if (!string.IsNullOrEmpty(document.HeaderCenter))
                paramsBuilder.AppendFormat("--header-center \"{0}\" ", document.HeaderCenter);

            if (!string.IsNullOrEmpty(document.HeaderRight))
                paramsBuilder.AppendFormat("--header-right \"{0}\" ", document.HeaderRight);

            if (!string.IsNullOrEmpty(document.FooterLeft))
                paramsBuilder.AppendFormat("--footer-left \"{0}\" ", document.FooterLeft);

            //if (!string.IsNullOrEmpty(document.FooterCenter))
            //{
            //    paramsBuilder.Append("--footer-spacing 2 ");
            //    paramsBuilder.Append("--footer-font-size 8 ");
            //    paramsBuilder.AppendFormat("--footer-center \"{0}\" ", document.FooterCenter);
            //}

            if (!string.IsNullOrEmpty(document.FooterRight))
                paramsBuilder.AppendFormat("--footer-right \"{0}\" ", document.FooterRight);

            if (document.ExtraParams != null)
                foreach (var extraParam in document.ExtraParams)
                    paramsBuilder.AppendFormat("--{0} {1} ", extraParam.Key, extraParam.Value);

            if (document.Cookies != null)
                foreach (var cookie in document.Cookies)
                    paramsBuilder.AppendFormat("--cookie {0} {1} ", cookie.Key, cookie.Value);

            if (!string.IsNullOrEmpty(document.Html))
                paramsBuilder.AppendFormat("- {0}", outputPdfFilePath);
            else
                paramsBuilder.AppendFormat("\"{0}\" \"{1}\"", document.Url, outputPdfFilePath);

            try
            {
                StringBuilder output = new StringBuilder();
                StringBuilder error = new StringBuilder();

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = environment.WkHtmlToPdfPath;
                    process.StartInfo.Arguments = paramsBuilder.ToString();
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.RedirectStandardInput = true;

                    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                    {
                        DataReceivedEventHandler outputHandler = (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                outputWaitHandle.Set();
                            }
                            else
                            {
                                output.AppendLine(e.Data);
                            }
                        };

                        DataReceivedEventHandler errorHandler = (sender, e) =>
                        {
                            if (e.Data == null)
                            {
                                errorWaitHandle.Set();
                            }
                            else
                            {
                                error.AppendLine(e.Data);
                            }
                        };

                        process.OutputDataReceived += outputHandler;
                        process.ErrorDataReceived += errorHandler;

                        try
                        {
                            process.Start();

                            process.BeginOutputReadLine();
                            process.BeginErrorReadLine();

                            if (document.Html != null)
                            {
                                using (var stream = process.StandardInput)
                                {
                                    byte[] buffer = Encoding.UTF8.GetBytes(document.Html);
                                    stream.BaseStream.Write(buffer, 0, buffer.Length);
                                    stream.WriteLine();


                                }
                            }


                            if (process.WaitForExit(environment.Timeout) && outputWaitHandle.WaitOne(environment.Timeout) && errorWaitHandle.WaitOne(environment.Timeout))
                            {
                                if (process.ExitCode != 0 && !File.Exists(outputPdfFilePath))
                                {
                                    throw new PdfConvertException(String.Format("Html to PDF conversion of '{0}' failed. Wkhtmltopdf output: \r\n{1}", document.Url, error));
                                }
                            }
                            else
                            {
                                if (!process.HasExited)
                                    process.Kill();

                                throw new PdfConvertTimeoutException();
                            }
                        }
                        finally
                        {
                            process.OutputDataReceived -= outputHandler;
                            process.ErrorDataReceived -= errorHandler;
                        }
                    }
                }


                if (woutput.OutputStream != null)
                {
                    using (Stream fs = new FileStream(outputPdfFilePath, FileMode.Open))
                    {
                        byte[] buffer = new byte[32 * 1024];
                        int read;

                        while ((read = fs.Read(buffer, 0, buffer.Length)) > 0)
                            woutput.OutputStream.Write(buffer, 0, read);
                    }
                }

                if (woutput.OutputCallback != null)
                {
                    byte[] pdfFileBytes = File.ReadAllBytes(outputPdfFilePath);
                    woutput.OutputCallback(document, pdfFileBytes);
                }

            }
            finally
            {
                if (delete && File.Exists(outputPdfFilePath))
                    File.Delete(outputPdfFilePath);

            }
        }
    }

    public class PdfConvertException : Exception
    {
        public PdfConvertException(String msg) : base(msg) { }
    }

    public class PdfConvertTimeoutException : PdfConvertException
    {
        public PdfConvertTimeoutException() : base("HTML to PDF conversion process has not finished in the given period.") { }
    }

    public class PdfOutput
    {
        public String OutputFilePath { get; set; }
        public Stream OutputStream { get; set; }
        public Action<PdfDocument, byte[]> OutputCallback { get; set; }
    }

    public class PdfDocument
    {
        public String Url { get; set; }
        public String Html { get; set; }
        public String HeaderUrl { get; set; }
        public String FooterUrl { get; set; }
        public String HeaderLeft { get; set; }
        public String HeaderCenter { get; set; }
        public String HeaderRight { get; set; }
        public String FooterLeft { get; set; }
        public bool? FooterLine { get; set; }
        public String FooterCenter { get; set; }
        public String FooterRight { get; set; }
        public bool IsLandScape { get; set; }
        public object State { get; set; }
        public Dictionary<String, String> Cookies { get; set; }
        public Dictionary<String, String> ExtraParams { get; set; }

        public String MarginTop { get; set; }
        public String MarginBottom { get; set; }
        public String MarginLeft { get; set; }
        public String MarginRight { get; set; }
        public String PageSize { get; set; }
    }

    public class PdfConvertEnvironment
    {
        public String TempFolderPath { get; set; }
        public String WkHtmlToPdfPath { get; set; }
        public int Timeout { get; set; }
        public bool Debug { get; set; }
    }

}


