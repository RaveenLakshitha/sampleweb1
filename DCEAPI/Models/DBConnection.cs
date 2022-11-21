using System.Runtime.Serialization;

namespace DCE_API.Models
{
    public class DBConnection
    {
        [DataMember(Name = "IsSuccessful")]
        public bool IsSuccessful { get; set; }

        [DataMember(Name = "ReturnMessage")]
        public string ReturnMessage { get; set; }

       /* [DataMember(Name = "Data")]
        public T Data { get; set; }*/
    }
}
