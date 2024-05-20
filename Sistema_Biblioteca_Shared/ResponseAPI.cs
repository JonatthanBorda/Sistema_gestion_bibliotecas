using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_Biblioteca_Shared
{
    public class ResponseAPI<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Object { get; set; }
    }
}
