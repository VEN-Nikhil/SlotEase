using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotEase.Application.DTO.User
{
    public  class UserRequestDto
    {
            public int pageNumber { get; set; }
            public int pageSize { get; set; }
        public string email { get; set; } = string.Empty;
     
    }
}
