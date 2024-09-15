using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATN.Core.ViewModel.GHNVM
{
    public class CreateGHNResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
    }
    public class Fee2
    {
        public int coupon { get; set; }
        public int insurance { get; set; }
        public int main_service { get; set; }
        public int r2s { get; set; }
        public int Return { get; set; }
        public int station_do { get; set; }
        public int station_pu { get; set; }
    }

    public class Data
    {
        public string district_encode { get; set; }
        public string expected_delivery_time { get; set; }
        public Fee2 Fee { get; set; }
        public string order_code { get; set; }
        public string sort_code { get; set; }
        public string total_fee { get; set; }
        public string trans_type { get; set; }
        public string ward_encode { get; set; }
    }

}
