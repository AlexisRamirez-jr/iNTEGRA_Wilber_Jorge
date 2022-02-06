using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iNTEGRA_Wilber_Jorge.DTO
{
    public class APIcryptoDTO
    {
        public class Time
        {
            public string Updated { get; set; }

            public DateTimeOffset UpdatedIso { get; set; }

            public string Updateduk { get; set; }
        }

        public class Bitcoin
        {
            public Time Time { get; set; }

            public string Disclaimer { get; set; }

            public string ChartName { get; set; }

            public Bpi Bpi { get; set; }
        }

        public class Bpi
        {
            public EUR USD { get; set; }

            public EUR GBP { get; set; }

            public EUR EUR { get; set; }
        }

        public class EUR
        {
            public string Code { get; set; }

            public string Symbol { get; set; }

            public string Rate { get; set; }

            public string Description { get; set; }

            public double RateFloat { get; set; }
        }

        
    }
}
