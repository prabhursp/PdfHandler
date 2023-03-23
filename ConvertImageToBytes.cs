public static byte[] ConvertImageToBytes(System.Drawing.Image image, long compressionLevel)
        {
            if (compressionLevel < 0)
            {
                compressionLevel = 0;
            }
            else if (compressionLevel > 100)
            {
                compressionLevel = 100;
            }
            ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, compressionLevel);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, jgpEncoder, myEncoderParameters);
                return ms.ToArray();
            }

        }