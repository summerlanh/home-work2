﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelloWorld.Models
{
    public class GuestResponse
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
    }
}