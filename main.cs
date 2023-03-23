Main()
{
	try
      {
			PdfFileHandler(File.ReadAllBytes(@"PdfFilePath.pdf"));
	  }
	catch (Exception ex)
		{
			//Print/Handle Exceptions
		}  
}