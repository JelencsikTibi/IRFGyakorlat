using System;
using System.Collections.Generic;
using System.Text;

namespace UserMaintenance.Entities
{
    public class User
    {
        public Guid ID { get; } = Guid.NewGuid();
        
        private int myVar;
        public string FullName { get; set; }

    }
}
