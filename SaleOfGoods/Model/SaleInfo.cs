//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class SaleInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SaleInfo()
        {
            this.FileInfo = new HashSet<FileInfo>();
        }
    
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
    
        public virtual Client Clients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FileInfo> FileInfo { get; set; }
        public virtual Product Products { get; set; }
    }
}
