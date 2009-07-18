using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace July09v31.Core.Domain.Model
{
    public class RssFeed : PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string Url { get; set; }
    }
}
