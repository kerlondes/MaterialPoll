//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Учебаня_практика
{
    using System;
    using System.Collections.Generic;
    
    public partial class AuthHistory
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual User User { get; set; }
    }
}
