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
    
    public partial class User
    {
        public User()
        {
            this.AuthHistories = new HashSet<AuthHistory>();
            this.Orders = new HashSet<Order>();
            this.Sales = new HashSet<Sale>();
            this.Supplies = new HashSet<Supply>();
        }
    
        public int ID { get; set; }
        public int RoleID { get; set; }
        public Nullable<int> PartnerID { get; set; }
        public bool Health { get; set; }
        public string FIO { get; set; }
        public System.DateTime Birthday { get; set; }
        public string Passport { get; set; }
        public bool HaveFamily { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string CardRecvisits { get; set; }
    
        public virtual ICollection<AuthHistory> AuthHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
        public virtual ICollection<Supply> Supplies { get; set; }
    }
}
