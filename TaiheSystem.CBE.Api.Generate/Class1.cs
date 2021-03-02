using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DW = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using PIC = DocumentFormat.OpenXml.Drawing.Pictures;
using A = DocumentFormat.OpenXml.Drawing;
using System.Drawing;


namespace TaiheSystem.CBE.Api.Generate
{
    public static class Class1
    {

        public static void test()
        {
            GenerateText();
            InsertAPicture(@"D:\Work\CBE\CNAS图片.png", @"D:\Work\CBE\openxmltext.docx", "tp_image1");
        }

        /// <summary>
        /// 替换文本信息
        /// </summary>
        public static void GenerateText()
        {
            string path = @"D:\Work\CBE\openxmltext.docx";
            using (WordprocessingDocument doc = WordprocessingDocument.Open(path, true))
            {
                List<Text> textList = new List<Text>();
                Body body = doc.MainDocumentPart.Document.Body;
                foreach (Paragraph paragraph in body.Elements<Paragraph>())
                {
                    if (paragraph.InnerText.Contains("上海启真"))
                    {
                        foreach (Run run in paragraph.Elements<Run>())
                        {
                            textList.AddRange(run.Elements<Text>());
                        }
                        paragraph.Elements<Run>().FirstOrDefault().Append(new Text("欢迎你，钛和认证！"));
                    }
                }
                foreach (Table table in body.Elements<Table>())
                {
                    foreach (TableRow tableRow in table.Elements<TableRow>())
                    {
                        foreach (TableCell tableCell in tableRow.Elements<TableCell>())
                        {
                            if (tableCell.InnerText.Contains("启真上海"))
                            {
                                foreach (Paragraph paragraph in tableCell.Elements<Paragraph>())
                                {
                                    foreach (Run run in paragraph.Elements<Run>())
                                    {
                                        textList.AddRange(run.Elements<Text>());
                                    }
                                }
                                tableCell.Elements<Paragraph>().FirstOrDefault().Elements<Run>().FirstOrDefault().Append(new Text("欢迎你，钛和认证！"));
                            }
                        }
                    }
                }
                foreach (var removeText in textList)
                {
                    removeText.Remove();
                }
            }
        }

        public static void InsertAPicture(string fileName, string document, string bookmark)
        {
            OpenSettings openSettings = new OpenSettings();

            // Add the MarkupCompatibilityProcessSettings
            openSettings.MarkupCompatibilityProcessSettings =
                new MarkupCompatibilityProcessSettings(
                    MarkupCompatibilityProcessMode.ProcessAllParts, 
                    FileFormatVersions.Office2007);

            //   string document = @"E:\**项目\**文书\108.docx";
            //   document = @"F:\Project_Code\SVNProject\SDHS_SZCG_ZCCG\SZZF\SZZFWord\xcjckyyszj.docx";
            using (WordprocessingDocument wordprocessingDocument =
                WordprocessingDocument.Open(document, true))
            {
                MainDocumentPart mainPart = wordprocessingDocument.MainDocumentPart;

                ImagePart imagePart = mainPart.AddImagePart(ImagePartType.Jpeg);

                using (FileStream stream = new FileStream(fileName, FileMode.Open))
                {
                    imagePart.FeedData(stream);
                }



                AddImageToBody(wordprocessingDocument, mainPart.GetIdOfPart(imagePart),bookmark);
            }
        }

        //帮助文档中的源码，直接拷贝出来修改，根据
        private static void AddImageToBody(WordprocessingDocument wordDoc, string relationshipId,string bookmarkname)
        {
            var img = new Bitmap(@"D:\Work\CBE\CNAS图片.png");

            //var img = new BitmapImage(new Uri(@"D:\Work\CBE\CNAS图片.png", UriKind.RelativeOrAbsolute));
            var widthPx = img.Width;
            var heightPx = img.Height;
            var horzRezDpi = img.HorizontalResolution;
            var vertRezDpi = img.HorizontalResolution;
            const int emusPerInch = 914400;
            const int emusPerCm = 360000;
            var maxWidthCm = 16.51;
            var widthEmus = (long)(widthPx / horzRezDpi * emusPerInch);
            var heightEmus = (long)(heightPx / vertRezDpi * emusPerInch);
            var maxWidthEmus = (long)(maxWidthCm * emusPerCm);
            if (widthEmus > maxWidthEmus)
            {
                var ratio = (heightEmus * 1.0m) / widthEmus;
                widthEmus = maxWidthEmus;
                heightEmus = (long)(widthEmus * ratio);
            }

            //var widthEmus = (long)(16 * 360000);
            //var heightEmus = (long)(4 * 360000);

            var element =
         new Drawing(
             new DW.Inline(
                 new DW.Extent() { Cx = widthEmus, Cy = heightEmus },
                 new DW.EffectExtent()
                 {
                     LeftEdge = 0L,
                     TopEdge = 0L,
                     RightEdge = 0L,
                     BottomEdge = 0L
                 },
                 new DW.DocProperties()
                 {
                     Id = (UInt32Value)1U,
                     Name = "Picture 1"
                 },
                 new DW.NonVisualGraphicFrameDrawingProperties(
                     new A.GraphicFrameLocks() { NoChangeAspect = true }),
                 new A.Graphic(
                     new A.GraphicData(
                         new PIC.Picture(
                             new PIC.NonVisualPictureProperties(
                                 new PIC.NonVisualDrawingProperties()
                                 {
                                     Id = (UInt32Value)0U,
                                     Name = "New Bitmap Image.jpg"
                                 },
                                 new PIC.NonVisualPictureDrawingProperties()),
                             new PIC.BlipFill(
                                 new A.Blip(
                                     new A.BlipExtensionList(
                                         new A.BlipExtension()
                                         {
                                             Uri =
                                               "{28A0092B-C50C-407E-A947-70E740481C1C}"
                                         })
                                 )
                                 {
                                     Embed = relationshipId,
                                     CompressionState = A.BlipCompressionValues.Print
                                 },
                                 new A.Stretch(
                                     new A.FillRectangle())),
                             new PIC.ShapeProperties(
                                 new A.Transform2D(
                                     new A.Offset() { X = 0L, Y = 0L },
                                     new A.Extents() { Cx = widthEmus, Cy = heightEmus }),
                                 new A.PresetGeometry(
                                     new A.AdjustValueList()
                                 )
                                 { Preset = A.ShapeTypeValues.Rectangle }))
                     )
                     { Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture" })
             )
             {
                 DistanceFromTop = (UInt32Value)0U,
                 DistanceFromBottom = (UInt32Value)0U,
                 DistanceFromLeft = (UInt32Value)0U,
                 DistanceFromRight = (UInt32Value)0U,
                 EditId = "50D07946"
             });

            // Append the reference to body, the element should be in a Run.
            wordDoc.MainDocumentPart.Document.Body.AppendChild(new Paragraph(new Run(element)));

            ////书签处写入---这是关键 tp为传递过来的 书签名称，替换成变量即可
            //MainDocumentPart mainPart = wordDoc.MainDocumentPart;
            //var bookmarks = from bm in mainPart.Document.Body.Descendants<BookmarkStart>()
            //                where bm.Name == bookmarkname
            //                select bm;
            //var bookmark = bookmarks.SingleOrDefault();
            //if (bookmark != null)
            //{
            //    OpenXmlElement elem = bookmark.NextSibling();
            //    while (elem != null && !(elem is BookmarkEnd))
            //    {
            //        OpenXmlElement nextElem = elem.NextSibling();
            //        elem.Remove();
            //        elem = nextElem;
            //    }
            //    var parent = bookmark.Parent;   // bookmark's parent element
            //    Run run = new Run(new RunProperties());
            //    //该处插入element，注意区分和字符插入的方法Append(dd)
            //    parent.InsertAfter<Run>(new Run(new Run(element)), bookmark);

            //}


        }


    }
}
