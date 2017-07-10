using System.Collections.Generic;
using BusBoard.Api;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public BusInfo(string postCode, ApiReturn apiOutput)
        {
            PostCode = postCode;
            ApiOutput = apiOutput;
        }

        public string PostCode { get; set; }
        public ApiReturn ApiOutput { get; set; }
    }
}