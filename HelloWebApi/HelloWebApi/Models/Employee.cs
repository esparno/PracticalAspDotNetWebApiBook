using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace HelloWebApi.Models
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        public decimal Compensation { get; set; }

        [DataMember(Name="Compensation")]
        private string CompensationSerialized { get; set; }

        [OnSerializing]
        void OnSerializing(StreamingContext context)
        {
            this.CompensationSerialized = this.Compensation.ToString();
        }

        [DataMember]
        public int Department { get; set; }
    }
}