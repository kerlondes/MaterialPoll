//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Учебаня_практика
{
    using System;
    using System.Collections.Generic;
    
    public partial class Partner
    {
        public Partner()
        {
            this.Users = new HashSet<User>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int TypeID { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public double INN { get; set; }
        public double Rating { get; set; }
    
        public virtual PartnerType PartnerType { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
