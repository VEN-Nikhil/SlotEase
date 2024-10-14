using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotEase.Application.DTO.PickpPoint
{
    public class PickpPointRequestDto
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public  bool IsActive {  get; set; }
    }
}
