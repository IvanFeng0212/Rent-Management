﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModules.Models
{
    public class DataBaseModel<T>
    {
        public List<T> Data { get; set; } = new List<T>();
    }
}
