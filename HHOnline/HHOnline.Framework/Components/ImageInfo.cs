using System;
using System.Collections;
using System.IO;
using System.Text;

namespace HHOnline.Framework
{
    /// <summary>
    /// ÕºœÒ–≈œ¢
    /// </summary>
    public class ImageInfo
    {

        #region Public Static Members

        public static readonly int FORMAT_JPEG = 0;
        public static readonly int FORMAT_GIF = 1;
        public static readonly int FORMAT_PNG = 2;
        public static readonly int FORMAT_BMP = 3;

        public static readonly int COLOR_TYPE_UNKNOWN = -1;
        public static readonly int COLOR_TYPE_TRUECOLOR_RGB = 0;
        public static readonly int COLOR_TYPE_PALETTED = 1;
        public static readonly int COLOR_TYPE_GRAYSCALE = 2;
        public static readonly int COLOR_TYPE_BLACK_AND_WHITE = 3;

        #endregion

        #region Private Static Members

        private static readonly String[] FORMAT_NAMES = { "JPEG", "GIF", "PNG", "BMP" };

        private static readonly String[] MIME_TYPE_STRINGS = { "image/jpeg", "image/gif", "image/png", "image/bmp" };

        #endregion

        #region Private Members

        private int width;
        private int height;
        private int bitsPerPixel;
        private int colorType = COLOR_TYPE_UNKNOWN;
        private bool progressive;
        private int format;
        private bool collectComments = true;
        private ArrayList comments;
        private bool determineNumberOfImages;
        private int numberOfImages;
        private int physicalHeightDpi;
        private int physicalWidthDpi;

        private Stream stream = null;

        #endregion

        #region Public Properties

        public Stream Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public int BitsPerPixel
        {
            get { return bitsPerPixel; }
        }

        public bool Progressive
        {
            get { return progressive; }
        }

        public int Format
        {
            get { return format; }
        }

        public int ColorType
        {
            get { return colorType; }
        }

        public bool CollectComments
        {
            get { return collectComments; }
            set { collectComments = value; }
        }

        public ArrayList Comments
        {
            get { return comments; }
        }

        public bool DetermineNumberOfImages
        {
            get { return determineNumberOfImages; }
            set { determineNumberOfImages = value; }
        }

        public int NumberOfImages
        {
            get { return numberOfImages; }
        }

        public int PhysicalHeightDpi
        {
            get { return physicalHeightDpi; }
        }

        public int PhysicalWidthDpi
        {
            get { return physicalWidthDpi; }
        }

        public string FormatName
        {
            get
            {
                if (format >= 0 && format < FORMAT_NAMES.Length)
                    return FORMAT_NAMES[format];
                else
                    return "?";
            }
        }

        public string MimeType
        {
            get
            {
                if (format >= 0 && format < MIME_TYPE_STRINGS.Length)
                {
                    if (format == FORMAT_JPEG && progressive)
                        return "image/pjpeg";
                    return MIME_TYPE_STRINGS[format];
                }
                else
                    return null;
            }
        }

        #endregion

        #region Constructor

        public ImageInfo() { }

        public ImageInfo(Stream stream)
        {
            this.stream = stream;
        }

        #endregion

        #region Public Methods

        public bool Check()
        {
            if (stream == null)
                return false;

            format = -1;
            width = -1;
            height = -1;
            bitsPerPixel = -1;
            numberOfImages = 1;
            physicalHeightDpi = -1;
            physicalWidthDpi = -1;
            comments = null;
            stream.Position = 0;

            try
            {
                int b1 = stream.ReadByte() & 0xff;
                int b2 = stream.ReadByte() & 0xff;

                if (b1 == 0x47 && b2 == 0x49)
                    return CheckGif();
                else if (b1 == 0x89 && b2 == 0x50)
                    return CheckPng();
                else if (b1 == 0xff && b2 == 0xd8)
                    return CheckJpeg();
                else if (b1 == 0x42 && b2 == 0x4d)
                    return CheckBmp();
                else
                    return false;
            }
            catch (IOException)
            {
                return false;
            }
        }

        #endregion

        #region Private Checking Methods

        private bool CheckBmp()
        {
            byte[] a = new byte[44];
            if (stream.Read(a, 0, 44) != 44)
                return false;

            width = getIntLittleEndian(a, 16);
            height = getIntLittleEndian(a, 20);
            if (width < 1 || height < 1)
                return false;

            bitsPerPixel = getShortLittleEndian(a, 26);
            if (bitsPerPixel != 1 && bitsPerPixel != 4 &&
                bitsPerPixel != 8 && bitsPerPixel != 16 &&
                bitsPerPixel != 24 && bitsPerPixel != 32)
                return false;

            int x = (int)(getIntLittleEndian(a, 36) * 0.0254);
            if (x > 0)
                physicalWidthDpi = x;
            int y = (int)(getIntLittleEndian(a, 40) * 0.0254);
            if (y > 0)
                physicalHeightDpi = y;

            format = FORMAT_BMP;
            return true;
        }

        private bool CheckGif()
        {
            byte[] GIF_MAGIC_87A = new byte[] { 0x46, 0x38, 0x37, 0x61 };
            byte[] GIF_MAGIC_89A = { 0x46, 0x38, 0x39, 0x61 };
            byte[] a = new byte[11]; // 4 from the GIF signature + 7 from the global header
            if (stream.Read(a, 0, 11) != 11)
                return false;

            if ((!equals(a, 0, GIF_MAGIC_89A, 0, 4)) &&
                (!equals(a, 0, GIF_MAGIC_87A, 0, 4)))
                return false;

            format = FORMAT_GIF;
            width = getShortLittleEndian(a, 4);
            height = getShortLittleEndian(a, 6);

            int flags = a[8] & 0xff;
            bitsPerPixel = ((flags >> 4) & 0x07) + 1;
            progressive = (flags & 0x02) != 0;

            // Check for number of images
            if (!determineNumberOfImages)
                return true;

            // skip global color palette
            if ((flags & 0x80) != 0)
            {
                int tableSize = (1 << ((flags & 7) + 1)) * 3;
                stream.Position += tableSize;
            }
            numberOfImages = 0;
            int blockType;
            do
            {
                blockType = stream.ReadByte();
                switch (blockType)
                {
                    case 0x2c: // image separator
                        if (stream.Read(a, 0, 9) != 9)
                            return false;

                        flags = a[8] & 0xff;
                        int localBitsPerPixel = (flags & 0x07) + 1;
                        if (localBitsPerPixel > bitsPerPixel)
                            bitsPerPixel = localBitsPerPixel;

                        if ((flags & 0x80) != 0)
                            stream.Position += (1 << localBitsPerPixel) * 3;

                        stream.Position++; // initial code length
                        int n1;
                        do
                        {
                            n1 = stream.ReadByte();
                            if (n1 > 0)
                                stream.Position += n1;
                            else if (n1 == -1)
                                return false;
                        }
                        while (n1 > 0);
                        numberOfImages++;
                        break;

                    case 0x21: // extension
                        int extensionType = stream.ReadByte();
                        if (collectComments && extensionType == 0xfe)
                        {
                            StringBuilder sb = new StringBuilder();
                            int n2;
                            do
                            {
                                n2 = stream.ReadByte();
                                if (n2 == -1)
                                    return false;

                                if (n2 > 0)
                                {
                                    for (int i = 0; i < n2; i++)
                                    {
                                        int ch = stream.ReadByte();
                                        if (ch == -1)
                                            return false;
                                        sb.Append((char)ch);
                                    }
                                }
                            }
                            while (n2 > 0);
                        }
                        else
                        {
                            int n3;
                            do
                            {
                                n3 = stream.ReadByte();
                                if (n3 > 0)
                                    stream.Position += n3;
                                else if (n3 == -1)
                                    return false;
                            }
                            while (n3 > 0);
                        }
                        break;

                    case 0x3b: // end of file
                        break;
                    default:
                        return false;
                }
            }
            while (blockType != 0x3b);

            return true;
        }

        private bool CheckJpeg()
        {
            byte[] data = new byte[12];
            while (true)
            {
                if (stream.Read(data, 0, 4) != 4)
                    return false;

                int marker = getShortBigEndian(data, 0);
                int size = getShortBigEndian(data, 2);
                if ((marker & 0xff00) != 0xff00)
                    return false; // not a valid marker

                if (marker == 0xffe0)
                { // APPx 
                    if ((size < 14) && (size != 8))
                        return false; // APPx header is normally >= 14 bytes, but some that don't have DPIs are 8

                    if (stream.Read(data, 0, 12) != 12)
                        return false;

                    byte[] APP0_ID = { 0x4a, 0x46, 0x49, 0x46, 0x00 };
                    if (equals(APP0_ID, 0, data, 0, 5))
                    {
                        if (data[7] == 1)
                        {
                            physicalWidthDpi = getShortBigEndian(data, 8);
                            physicalHeightDpi = getShortBigEndian(data, 10);
                        }
                        else if (data[7] == 2)
                        {
                            int x = getShortBigEndian(data, 8);
                            int y = getShortBigEndian(data, 10);
                            physicalWidthDpi = (int)(x * 2.54f);
                            physicalHeightDpi = (int)(y * 2.54f);
                        }
                    }

                    stream.Position += size - 14;
                }
                else if (collectComments && size > 2 && marker == 0xfffe)
                {	// comment
                    size -= 2;
                    byte[] chars = new byte[size];
                    if (stream.Read(chars, 0, size) != size)
                        return false;

                    Encoding enc = Encoding.GetEncoding("iso-8859-1");
                    string comment = enc.GetString(chars);
                    comment = comment.Trim();
                    AddComment(comment);
                }
                else if (marker >= 0xffc0 && marker <= 0xffcf && marker != 0xffc4 && marker != 0xffc8)
                {
                    if (stream.Read(data, 0, 6) != 6)
                        return false;

                    format = FORMAT_JPEG;
                    bitsPerPixel = (data[0] & 0xff) * (data[5] & 0xff);
                    progressive = marker == 0xffc2 || marker == 0xffc6 ||
                        marker == 0xffca || marker == 0xffce;

                    width = getShortBigEndian(data, 3);
                    height = getShortBigEndian(data, 1);

                    return true;
                }
                else
                    stream.Position += size - 2;
            }
        }

        private bool CheckPng()
        {
            byte[] PNG_MAGIC = new byte[] { 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a };
            byte[] a = new byte[27];

            if (stream.Read(a, 0, 27) != 27)
                return false;

            if (!equals(a, 0, PNG_MAGIC, 0, 6))
                return false;

            format = FORMAT_PNG;
            width = getIntBigEndian(a, 14);
            height = getIntBigEndian(a, 18);
            bitsPerPixel = a[22] & 0xff;

            int colorType = a[23] & 0xff;
            if (colorType == 2 || colorType == 6)
                bitsPerPixel *= 3;

            progressive = (a[26] & 0xff) != 0;
            return true;
        }

        #endregion

        #region Private Helper Methods

        private void AddComment(string s)
        {
            if (comments == null)
                comments = new ArrayList();
            comments.Add(s);
        }

        private bool equals(byte[] a1, int offs1, byte[] a2, int offs2, int num)
        {
            while (num-- > 0)
                if (a1[offs1++] != a2[offs2++])
                    return false;
            return true;
        }


        private int getIntBigEndian(byte[] a, int offs)
        {
            return
                (a[offs] & 0xff) << 24 |
                (a[offs + 1] & 0xff) << 16 |
                (a[offs + 2] & 0xff) << 8 |
                a[offs + 3] & 0xff;
        }

        private int getIntLittleEndian(byte[] a, int offs)
        {
            return
                (a[offs + 3] & 0xff) << 24 |
                (a[offs + 2] & 0xff) << 16 |
                (a[offs + 1] & 0xff) << 8 |
                a[offs] & 0xff;
        }

        private int getShortBigEndian(byte[] a, int offs)
        {
            return
                (a[offs] & 0xff) << 8 |
                (a[offs + 1] & 0xff);
        }

        private int getShortLittleEndian(byte[] a, int offs)
        {
            return (a[offs] & 0xff) | (a[offs + 1] & 0xff) << 8;
        }

        #endregion

    }
}