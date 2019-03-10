using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Domain
{
    public class Page : Entity
    {
        public string PageName { get; set; }

        public string PageText { get; set; }

        public string PageDesc { get; set; }

    }
}
