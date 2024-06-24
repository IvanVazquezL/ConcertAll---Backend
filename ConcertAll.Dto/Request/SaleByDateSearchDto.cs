using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertAll.Dto.Request
{
    public class SaleByDateSearchDto
    {
        public string DateStart { get; set; } = default!;
        public string DateEnd { get; set; } = default!;
    }
}
