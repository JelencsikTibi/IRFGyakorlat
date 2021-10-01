using System;
using System.Collections.Generic;
using System.Text;

namespace UserMaintenance.Entities
{
    public class User
    {
        public Guid ID { get; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }


        private int myVar;
        public string FullName 
        {
            get 
            {
                return string.Format(
                  "{0} {1}",
                  LastName,
                  FirstName);  
            } 
        }
    }
}
