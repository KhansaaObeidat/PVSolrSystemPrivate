namespace PVSolrSystemPrivate.Models.ViewModels
{
    public class SearchViewModel
    {
        public string SearchString { get; set; }
        public IEnumerable<Customer> Customers { get; set; } // Add this property to hold the list of customers


    }
}
