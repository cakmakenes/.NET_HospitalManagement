﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models
{
    public class FavoritesModel
    {
        public int  DoctorId { get; set; }
        public int UserId { get; set; }
        public string DoctorName { get; set; }
    }
}
