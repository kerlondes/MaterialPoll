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
    
    public partial class ProductsInOrder
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int ProductArticle { get; set; }
        public int Amount { get; set; }
        public int Cost { get; set; }
        public int Skidka { get; set; }
        public int CostAfter { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
