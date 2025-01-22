using System.ComponentModel.DataAnnotations.Schema;

namespace PVSolrSystemPrivate.Models
{
    public class ConsumptionCustomer
    {
        public string Name { get; set; } // اسم المشترك
        public bool IsSupported { get; set; } // هل العداد مدعوم أم لا
        public string CalculationType { get; set; } // نوع الحساب (معدل الاستهلاك، شهر معين، كامل السنة)
        public double Consumption { get; set; } // الاستهلاك المدخل
        public string SelectedMonth { get; set; } // الشهر المحدد
        public List<double> MonthlyConsumptions { get; set; } // الاستهلاك الشهري لكل شهر
        public List<double> ExportedEnergyValues { get; set; } // الطاقة المصدرة
        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}
