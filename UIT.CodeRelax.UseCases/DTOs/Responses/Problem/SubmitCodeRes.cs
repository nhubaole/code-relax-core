﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIT.CodeRelax.UseCases.DTOs.Responses.Problem
{
    public class SubmitCodeRes
    {
        public bool Success { get; set; }
        public object Output { get; set; }
        public string Errors { get; set; }
    }
}
