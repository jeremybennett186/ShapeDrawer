using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeDrawer.Models
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public ErrorResponse(string message)
        {
            this.Message = message;        }
    }
}
