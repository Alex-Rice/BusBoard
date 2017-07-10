using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    public class ApiReturn
    {
        public List<StopAndBus> StopAndBusList { get; set; }
        public bool postCodeValid { get; set; }
        public Exception unknownError { get; set; }

        public ApiReturn(List<StopAndBus> list)
        {
            StopAndBusList = list;
            postCodeValid = true;
            unknownError = null;
        }

        public ApiReturn(Exception e)
        {
            unknownError = e;
            StopAndBusList = null;
            postCodeValid = true;
        }

        public ApiReturn()
        {
            unknownError = null;
            StopAndBusList = null;
            postCodeValid = false;
        }
    }
}

