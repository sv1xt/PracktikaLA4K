//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace practika
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerOrderItem
    {
        public int CustomerOrderItemID { get; set; }
        public int CustomerOrderID { get; set; }
        public int Product_ID { get; set; }
        public int Quantity { get; set; }
    
        public virtual CustomerOrder CustomerOrder { get; set; }
        public virtual Товары Товары { get; set; }
    }
}
