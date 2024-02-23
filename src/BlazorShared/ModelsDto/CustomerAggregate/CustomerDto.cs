using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.ModelsDto
{
    public class CustomerDto : AuditBase
    {
        public CustomerDto()
        { }

        public CustomerDto(Guid customerId, string customerFirstName, string customerLastName, DateTime? customerBirthDate, string? customerWebsite, decimal? creditLimit, string? notes, string? customerMiddleName, string? customerTitle, string? customerGender, string? customerCompanyName, string? customerJobTitle, string? industry, string? preferredContactMethod, string? customerStatus, int? loyaltyPoints, string? source, DateTime? lastPurchaseDate, decimal? totalPurchases, bool? isDeleted, System.Guid tenantId, decimal? customerDecimal, int customerInt, DateTime customerDateTime2, DateTime customerDateTime, bool customerBit, double? customerFloat, float? customerReal, long? customerBigInt, short? customerSmallInt, sbyte? customerTinyInt, string? customerChar, string? customerVarChar, string? customerText, string? customerNchar, string? customerNvarChar, string? customerNtext, byte[]? customerBinary, byte[]? customerVarBinary, byte[]? customerImage, decimal? customerMoney, decimal? customerSmallMoney, byte[]? customerTimestamp, System.Guid? customerUniqueIdentifier, string? customerXml)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
            AuditEntityId = customerId.ToString();
            AuditId = Guid.NewGuid().ToString();
            AuditEntityType = GetType().Name;
            CustomerFirstName = Guard.Against.NullOrWhiteSpace(customerFirstName, nameof(customerFirstName));
            CustomerLastName = Guard.Against.NullOrWhiteSpace(customerLastName, nameof(customerLastName));
            CustomerBirthDate = customerBirthDate;
            CustomerWebsite = customerWebsite;
            CreditLimit = creditLimit;
            Notes = notes;
            CustomerMiddleName = customerMiddleName;
            CustomerTitle = customerTitle;
            CustomerGender = customerGender;
            CustomerCompanyName = customerCompanyName;
            CustomerJobTitle = customerJobTitle;
            Industry = industry;
            PreferredContactMethod = preferredContactMethod;
            CustomerStatus = customerStatus;
            LoyaltyPoints = loyaltyPoints;
            Source = source;
            LastPurchaseDate = lastPurchaseDate;
            TotalPurchases = totalPurchases;
            IsDeleted = isDeleted;
            TenantId = Guard.Against.NullOrEmpty(tenantId, nameof(tenantId));
            CustomerDecimal = customerDecimal;
            CustomerInt = Guard.Against.NegativeOrZero(customerInt, nameof(customerInt));
            CustomerDateTime2 = Guard.Against.OutOfSQLDateRange(customerDateTime2, nameof(customerDateTime2));
            CustomerDateTime = Guard.Against.OutOfSQLDateRange(customerDateTime, nameof(customerDateTime));
            CustomerBit = Guard.Against.Null(customerBit, nameof(customerBit));
            CustomerFloat = customerFloat;
            CustomerReal = customerReal;
            CustomerBigInt = customerBigInt;
            CustomerSmallInt = customerSmallInt;
            CustomerTinyInt = customerTinyInt;
            CustomerChar = customerChar;
            CustomerVarChar = customerVarChar;
            CustomerText = customerText;
            CustomerNChar = customerNchar;
            CustomerNVarChar = customerNvarChar;
            CustomerNText = customerNtext;
            CustomerBinary = customerBinary;
            CustomerVarBinary = customerVarBinary;
            CustomerImage = customerImage;
            CustomerMoney = customerMoney;
            CustomerSmallMoney = customerSmallMoney;
            CustomerTimestamp = customerTimestamp;
            CustomerUniqueIdentifier = customerUniqueIdentifier;
            CustomerXml = customerXml;
        }

        public decimal? CreditLimit { get; set; }
        public List<CustomerAccountDto> CustomerAccounts { get; set; } = new();
        public List<CustomerAddressDto> CustomerAddresses { get; set; } = new();
        public long? CustomerBigInt { get; set; }
        public byte[]? CustomerBinary { get; set; }
        public DateTime? CustomerBirthDate { get; set; }

        [Required(ErrorMessage = "Customer Bit is required")]
        public bool CustomerBit { get; set; }

        [MaxLength(10)]
        public string? CustomerChar { get; set; }

        [MaxLength(255)]
        public string? CustomerCompanyName { get; set; }

        [Required(ErrorMessage = "Customer Date Time is required")]
        public DateTime CustomerDateTime { get; set; }

        [Required(ErrorMessage = "Customer Date Time 2 is required")]
        public DateTime CustomerDateTime2 { get; set; }

        public decimal? CustomerDecimal { get; set; }

        [Required(ErrorMessage = "Customer First Name is required")]
        [MaxLength(255)]
        public string CustomerFirstName { get; set; }

        public double? CustomerFloat { get; set; }

        [MaxLength(10)]
        public string? CustomerGender { get; set; }

        public Guid CustomerId { get; set; }
        public byte[]? CustomerImage { get; set; }

        [Required(ErrorMessage = "Customer Int is required")]
        public int CustomerInt { get; set; }

        [MaxLength(255)]
        public string? CustomerJobTitle { get; set; }

        [Required(ErrorMessage = "Customer Last Name is required")]
        [MaxLength(255)]
        public string CustomerLastName { get; set; }

        [MaxLength(255)]
        public string? CustomerMiddleName { get; set; }

        public decimal? CustomerMoney { get; set; }

        [MaxLength(10)]
        public string? CustomerNChar { get; set; }

        [MaxLength(1073741823)]
        public string? CustomerNText { get; set; }

        [MaxLength(255)]
        public string? CustomerNVarChar { get; set; }

        public List<CustomerPhoneNumberDto> CustomerPhoneNumbers { get; set; } = new();

        public float? CustomerReal { get; set; }

        public short? CustomerSmallInt { get; set; }

        public decimal? CustomerSmallMoney { get; set; }

        [MaxLength(50)]
        public string? CustomerStatus { get; set; }

        [MaxLength(2147483647)]
        public string? CustomerText { get; set; }

        public byte[]? CustomerTimestamp { get; set; }

        public sbyte? CustomerTinyInt { get; set; }

        [MaxLength(50)]
        public string? CustomerTitle { get; set; }

        public Guid? CustomerUniqueIdentifier { get; set; }

        public byte[]? CustomerVarBinary { get; set; }

        [MaxLength(255)]
        public string? CustomerVarChar { get; set; }

        [MaxLength(255)]
        public string? CustomerWebsite { get; set; }

        [MaxLength(100)]
        public string? CustomerXml { get; set; }

        [MaxLength(255)]
        public string? Industry { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? LastPurchaseDate { get; set; }

        public int? LoyaltyPoints { get; set; }

        [MaxLength(999)]
        public string? Notes { get; set; }

        [MaxLength(50)]
        public string? PreferredContactMethod { get; set; }

        [MaxLength(255)]
        public string? Source { get; set; }

        [Required(ErrorMessage = "Tenant Id is required")]
        public Guid TenantId { get; set; }

        public decimal? TotalPurchases { get; set; }
    }
}