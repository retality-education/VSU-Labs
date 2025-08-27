using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeFinanceApp.Services
{
    internal static class ConvertHelper
    {
        public static Image ByteArrayToImage(byte[] byteArray)
        {
            using var ms = new MemoryStream(byteArray);
            return new Bitmap(ms);
        }

    }
}
