//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class vacation_requests
    {
        public int request_id { get; set; }
        public int employee_id { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public string status { get; set; }
        public Nullable<int> approved_by { get; set; }
        public System.DateTime created_at { get; set; }
    
        public virtual employees employees { get; set; }
        public virtual employees employees1 { get; set; }
    }
}
