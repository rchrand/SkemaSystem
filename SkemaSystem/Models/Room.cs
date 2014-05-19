﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SkemaSystem.Models
{
    public class Room
    {
        [Required]
        public int ID { get; set; }
        
        [Required]
        public string RoomName { get; set; }
    }
}