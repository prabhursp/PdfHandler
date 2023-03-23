public byte[] PdfFileHandler(byte[] pdf)
        {
            // Read the file
            PdfReader pdfReader = new PdfReader(pdf);
			
			//Get the totla no of elements in file
            int count = pdfReader.XrefSize;
            PdfObject pdfObject;
            PRStream prStream;
			
            // Check for image and handler image PRStream
            for (int i = 0; i < count; i++)
            {
                try
                {
					//Get Pdf elements
                    pdfObject = pdfReader.GetPdfObject(i);
                    if (pdfObject == null || !pdfObject.IsStream()) continue;

                    prStream = (PRStream)pdfObject;
					
					//Get Images in bytes[] format
                    byte[] imageBytes;
                    
                    
                        PdfImageObject image = new PdfImageObject(prStream);
                        using (System.Drawing.Image original = image.GetDrawingImage())
                        {
                            if (original == null) continue;                            
                            int width = (int)(original.Width * 1);                            
                            int height = (int)(original.Height * 1);

                            using (System.Drawing.Image thumb = new Bitmap(width, height))
                            {
                                using (Graphics graphic = Graphics.FromImage(thumb))
                                {
                                    graphic.DrawImage(original, 0, 0, width, height);
                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        thumb.Save(ms, ImageFormat.Png);
                                        imageBytes = ms.ToArray();
                                    }
                                }
                            }

                            var Bytes = imageBytes;
                            MemoryStream sourceMS = new MemoryStream(Bytes);
                            System.Drawing.Image oldImage = Bitmap.FromStream(sourceMS);
							//Set Compression Level
                            imageBytes = Form2.ConvertImageToBytes(oldImage, 85);
                            prStream.SetData(imageBytes, false, PRStream.NO_COMPRESSION);
                            prStream.Put(PdfName.TYPE, PdfName.XOBJECT);
                            prStream.Put(PdfName.SUBTYPE, PdfName.IMAGE);
                            prStream.Put(PdfName.FILTER, PdfName.DCTDECODE);
                            prStream.Put(PdfName.WIDTH, new PdfNumber(width));
                            prStream.Put(PdfName.HEIGHT, new PdfNumber(height));
                            prStream.Put(PdfName.BITSPERCOMPONENT, new PdfNumber(8));
                            prStream.Put(PdfName.COLORSPACE, PdfName.DEVICERGB);
                        }
                    
                }
                catch (Exception ex)
                {
                    //Handle Exceptions
                }
                
            }
            // Save altered PDF
            using (MemoryStream ms = new MemoryStream())
            {
                File.WriteAllBytes(@"C:\Users\RANGATHP\Downloads\Test5.pdf", ms.ToArray());
                return ms.ToArray();
            }
        }
			
            // Save  PDF
            using (MemoryStream ms = new MemoryStream())
            {
                File.WriteAllBytes(@"C:\Users\RANGATHP\Downloads\Test5.pdf", ms.ToArray());
                return ms.ToArray();
            }
        }