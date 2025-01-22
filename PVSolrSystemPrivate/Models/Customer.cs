using PVSolrSystemPrivate.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace PVSolrSystemPrivate.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage ="Customer Name Is Required")]
        [Display(Name =" اسم العميل")]
        public string? CustomerName { get; set; }

        [Required(ErrorMessage = "Customer Phone Number Is Required")]
        [Display(Name = "رقم هاتف العميل ")]
        public string? PhoneNumber { get; set; }
        [Display(Name = "رقم الاشتراك")]
        public string? SubscriptionNumber { get; set; }
        [Display(Name = "الطور")]
        public PhaseType? Phase { get; set; } 

        [Display(Name = "الموقع")]
        public string? Location { get; set; }

        [Display(Name = "ملاحظات")]
        public string? Notes { get; set; }
        public decimal? Consumption { get; set; } // استهلاك الطاقة
        public decimal? SolarSystemSize { get; set; } // حجم النظام الشمسي
        public decimal? BillBefore { get; set; } // فاتورة قبل النظام الشمسي
        public decimal ? BillAfter { get; set; } // فاتورة بعد النظام الشمسي
        public string? DocumentPath { get; set; } // مسار الأوراق

    }
}
