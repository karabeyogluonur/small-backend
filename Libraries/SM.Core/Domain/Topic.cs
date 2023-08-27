﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Domain
{
    public class Topic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
