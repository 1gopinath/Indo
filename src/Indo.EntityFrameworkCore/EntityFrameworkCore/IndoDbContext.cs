using Microsoft.EntityFrameworkCore;
using Indo.Users;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using Indo.Customers;
using Indo.Currencies;
using Indo.Products;
using Indo.Companies;
using Indo.Uoms;
using Indo.ServiceOrders;
using Indo.ServiceOrderDetails;
using Indo.ProjectOrders;
using Indo.ProjectOrderDetails;
using Indo.NumberSequences;
using Indo.Vendors;
using Indo.PurchaseOrders;
using Indo.PurchaseReceipts;
using Indo.SalesOrders;
using Indo.SalesDeliveries;
using Indo.Warehouses;
using Indo.TransferOrders;
using Indo.DeliveryOrders;
using Indo.GoodsReceipts;
using Indo.StockAdjustments;
using Indo.Movements;
using Indo.Stocks;
using Indo.Services;
using Indo.PurchaseOrderDetails;
using Indo.SalesOrderDetails;
using Indo.PurchaseReceiptDetails;
using Indo.SalesDeliveryDetails;
using Indo.TransferOrderDetails;
using Indo.DeliveryOrderDetails;
using Indo.GoodsReceiptDetails;
using Indo.StockAdjustmentDetails;
using Indo.Departments;
using Indo.Employees;
using Indo.ExpenseTypes;
using Indo.Resources;
using Indo.Todos;
using Indo.Bookings;
using Indo.Expenses;
using Indo.Calendars;
using Indo.ExpenseDetails;
using Indo.Notes;
using Indo.ImportantDates;
using Indo.Contacts;
using Indo.Activities;
using Indo.LeadRatings;
using Indo.LeadSources;
using Indo.Tasks;
using Indo.ServiceQuotations;
using Indo.ServiceQuotationDetails;
using Indo.SalesQuotations;
using Indo.SalesQuotationDetails;
using Indo.CashAndBanks;
using Indo.CustomerInvoices;
using Indo.CustomerInvoiceDetails;
using Indo.CustomerCreditNotes;
using Indo.CustomerCreditNoteDetails;
using Indo.CustomerPayments;
using Indo.VendorBills;
using Indo.VendorBillDetails;
using Indo.VendorDebitNotes;
using Indo.VendorDebitNoteDetails;
using Indo.VendorPayments;

namespace Indo.EntityFrameworkCore
{
    /* This is your actual DbContext used on runtime.
     * It includes only your entities.
     * It does not include entities of the used modules, because each module has already
     * its own DbContext class. If you want to share some database tables with the used modules,
     * just create a structure like done for AppUser.
     *
     * Don't use this DbContext for database migrations since it does not contain tables of the
     * used modules (as explained above). See IndoMigrationsDbContext for migrations.
     */
    [ConnectionStringName("Default")]
    public class IndoDbContext : AbpDbContext<IndoDbContext>
    {
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Uom> Uoms { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceOrderDetail> ServiceOrderDetails { get; set; }
        public DbSet<ProjectOrder> ProjectOrders { get; set; }
        public DbSet<ProjectOrderDetail> ProjectOrderDetails { get; set; }
        public DbSet<NumberSequence> NumberSequences { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseReceipt> PurchaseReceipts { get; set; }
        public DbSet<SalesOrder> SalesOrders { get; set; }
        public DbSet<SalesDelivery> SalesDeliveries { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<TransferOrder> TransferOrders { get; set; }
        public DbSet<DeliveryOrder> DeliveryOrders { get; set; }
        public DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public DbSet<StockAdjustment> StockAdjustments { get; set; }
        public DbSet<Movement> Movements { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
        public DbSet<PurchaseReceiptDetail> PurchaseReceiptDetails { get; set; }
        public DbSet<SalesDeliveryDetail> SalesDeliveryDetails { get; set; }
        public DbSet<TransferOrderDetail> TransferOrderDetails { get; set; }
        public DbSet<DeliveryOrderDetail> DeliveryOrderDetails { get; set; }
        public DbSet<GoodsReceiptDetail> GoodsReceiptDetails { get; set; }
        public DbSet<StockAdjustmentDetail> StockAdjustmentDetails { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<ExpenseDetail> ExpenseDetails { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ImportantDate> ImportantDates { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<LeadRating> LeadRatings { get; set; }
        public DbSet<LeadSource> LeadSources { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ServiceQuotation> ServiceQuotations { get; set; }
        public DbSet<ServiceQuotationDetail> ServiceQuotationDetails { get; set; }
        public DbSet<SalesQuotation> SalesQuotations { get; set; }
        public DbSet<SalesQuotationDetail> SalesQuotationDetails { get; set; }
        public DbSet<CashAndBank> CashAndBanks { get; set; }
        public DbSet<CustomerInvoice> CustomerInvoices { get; set; }
        public DbSet<CustomerInvoiceDetail> CustomerInvoiceDetails { get; set; }
        public DbSet<CustomerCreditNote> CustomerCreditNotes { get; set; }
        public DbSet<CustomerCreditNoteDetail> CustomerCreditNoteDetails { get; set; }
        public DbSet<CustomerPayment> CustomerPayments { get; set; }
        public DbSet<VendorBill> VendorBills { get; set; }
        public DbSet<VendorBillDetail> VendorBillDetails { get; set; }
        public DbSet<VendorDebitNote> VendorDebitNotes { get; set; }
        public DbSet<VendorDebitNoteDetail> VendorDebitNoteDetails { get; set; }
        public DbSet<VendorPayment> VendorPayments { get; set; }

        /* Add DbSet properties for your Aggregate Roots / Entities here.
         * Also map them inside IndoDbContextModelCreatingExtensions.ConfigureIndo
         */

        public IndoDbContext(DbContextOptions<IndoDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Configure the shared tables (with included modules) here */

            builder.Entity<AppUser>(b =>
            {
                b.ToTable(AbpIdentityDbProperties.DbTablePrefix + "Users"); //Sharing the same table "AbpUsers" with the IdentityUser
                
                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                /* Configure mappings for your additional properties
                 * Also see the IndoEfCoreEntityExtensionMappings class
                 */
            });

            /* Configure your own tables/entities inside the ConfigureIndo method */

            builder.ConfigureIndo();
        }
    }
}
