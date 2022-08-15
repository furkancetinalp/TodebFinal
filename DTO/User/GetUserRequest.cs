﻿using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class GetUserRequest
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string IdentityNo { get; set; }
        public int HouseNo { get; set; }
        public string Phone { get; set; }
        public string CarInfo { get; set; }
        public string UserRole { get; set; }
    }
}
