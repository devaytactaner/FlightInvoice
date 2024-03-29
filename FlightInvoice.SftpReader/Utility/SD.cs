using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightInvoice.SftpReader.Utility
{
    public class SD
    {
        public static string SftpFileApiBase {  get; set; }
        public enum ApiType
        {
            GET,
            POST, 
            PUT,
            DELETE
        }
    }
}
