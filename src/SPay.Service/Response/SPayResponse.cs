using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPay.Service.Response
{
    /// <summary>
    /// Generic wrapper for web api response.       
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SPayResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public string Error { get; set; } = null;
        public List<string> ErrorMessages { get; set; } = null;
    }
}
