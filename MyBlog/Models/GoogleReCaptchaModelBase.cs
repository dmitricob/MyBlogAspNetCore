﻿using Microsoft.AspNetCore.Mvc;
using MyBlog.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public abstract class GoogleReCaptchaModelBase
    {
        // model to add reCaptcha field to model with spesified validation method
        //[Required]
        [GoogleReCaptchaValidation]
        [BindProperty(Name = "g-recaptcha-response")]
        public String GoogleReCaptchaResponse { get; set; }
    }
}
