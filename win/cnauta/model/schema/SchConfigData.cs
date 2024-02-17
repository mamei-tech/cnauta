
namespace cnauta.model.schema
{
    /// <summary>CNauta app configuration schema</summary>
    public class SchConfigData
    {
        public string DefaultUser { get; set; }
        public string DefaultUserPass { get; set; }

        /// <summary>Alternative user A</summary>
        public string AltAUSer { get; set; }
        public string AltAUSerPass { get; set; }

        /// <summary>Alternative user B</summary>
        public string AltBUSer { get; set; }
        public string AltBUSerPass { get; set; }
    }
}

