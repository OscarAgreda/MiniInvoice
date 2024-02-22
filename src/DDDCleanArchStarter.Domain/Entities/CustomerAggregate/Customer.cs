using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using DDDInvoicingCleanL.SharedKernel;
using DDDInvoicingCleanL.SharedKernel.Interfaces;

namespace DDDInvoicingClean.Domain.Entities
{
    public class Customer : BaseEntityEv<Guid>, IAggregateRoot
    {
        private readonly List<CustomerAccount> _customerAccounts = new();

        private readonly List<CustomerAddress> _customerAddresses = new();

        private readonly List<CustomerPhoneNumber> _customerPhoneNumbers = new();

        public Customer(Guid customerId, string customerFirstName, string customerLastName, DateTime? customerBirthDate, string? customerWebsite, decimal? creditLimit, string? notes, string? customerMiddleName, string? customerTitle, string? customerGender, string? customerCompanyName, string? customerJobTitle, string? industry, string? preferredContactMethod, string? customerStatus, int? loyaltyPoints, string? source, DateTime? lastPurchaseDate, decimal? totalPurchases, bool? isDeleted, System.Guid tenantId, decimal? customerDecimal, int customerInt, DateTime customerDateTime2, DateTime customerDateTime, bool customerBit, double? customerFloat, float? customerReal, long? customerBigInt, short? customerSmallInt, sbyte? customerTinyInt, string? customerChar, string? customerVarChar, string? customerText, string? customerNchar, string? customerNvarChar, string? customerNtext, byte[]? customerBinary, byte[]? customerVarBinary, byte[]? customerImage, decimal? customerMoney, decimal? customerSmallMoney, byte[]? customerTimestamp, System.Guid? customerUniqueIdentifier, string? customerXml)
        {
            CustomerId = Guard.Against.NullOrEmpty(customerId, nameof(customerId));
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

        private Customer()
        { }

        public decimal? CreditLimit { get; private set; }

        public IEnumerable<CustomerAccount> CustomerAccounts => _customerAccounts.AsReadOnly();

        public IEnumerable<CustomerAddress> CustomerAddresses => _customerAddresses.AsReadOnly();

        public long? CustomerBigInt { get; private set; }

        public byte[]? CustomerBinary { get; private set; }

        public DateTime? CustomerBirthDate { get; private set; }

        public bool CustomerBit { get; private set; }

        public string? CustomerChar { get; private set; }

        public string? CustomerCompanyName { get; private set; }

        public DateTime CustomerDateTime { get; private set; }

        public DateTime CustomerDateTime2 { get; private set; }

        public decimal? CustomerDecimal { get; private set; }

        public string CustomerFirstName { get; private set; }

        public double? CustomerFloat { get; private set; }

        public string? CustomerGender { get; private set; }

        [Key]
        public Guid CustomerId { get; private set; }

        public byte[]? CustomerImage { get; private set; }
        public int CustomerInt { get; private set; }
        public string? CustomerJobTitle { get; private set; }
        public string CustomerLastName { get; private set; }
        public string? CustomerMiddleName { get; private set; }
        public decimal? CustomerMoney { get; private set; }
        public string? CustomerNChar { get; private set; }
        public string? CustomerNText { get; private set; }
        public string? CustomerNVarChar { get; private set; }
        public IEnumerable<CustomerPhoneNumber> CustomerPhoneNumbers => _customerPhoneNumbers.AsReadOnly();
        public float? CustomerReal { get; private set; }
        public short? CustomerSmallInt { get; private set; }
        public decimal? CustomerSmallMoney { get; private set; }
        public string? CustomerStatus { get; private set; }
        public string? CustomerText { get; private set; }
        public byte[]? CustomerTimestamp { get; private set; }
        public sbyte? CustomerTinyInt { get; private set; }
        public string? CustomerTitle { get; private set; }
        public Guid? CustomerUniqueIdentifier { get; private set; }
        public byte[]? CustomerVarBinary { get; private set; }
        public string? CustomerVarChar { get; private set; }
        public string? CustomerWebsite { get; private set; }
        public string? CustomerXml { get; private set; }
        public string? Industry { get; private set; }
        public bool? IsDeleted { get; private set; }
        public DateTime? LastPurchaseDate { get; private set; }
        public int? LoyaltyPoints { get; private set; }
        public string? Notes { get; private set; }
        public string? PreferredContactMethod { get; private set; }
        public string? Source { get; private set; }
        public Guid TenantId { get; private set; }
        public decimal? TotalPurchases { get; private set; }

        public void SetCustomerChar(string customerChar)
        {
            CustomerChar = customerChar;
        }

        public void SetCustomerCompanyName(string customerCompanyName)
        {
            CustomerCompanyName = customerCompanyName;
        }

        public void SetCustomerFirstName(string customerFirstName)
        {
            CustomerFirstName = Guard.Against.NullOrEmpty(customerFirstName, nameof(customerFirstName));
        }

        public void SetCustomerGender(string customerGender)
        {
            CustomerGender = customerGender;
        }

        public void SetCustomerJobTitle(string customerJobTitle)
        {
            CustomerJobTitle = customerJobTitle;
        }

        public void SetCustomerLastName(string customerLastName)
        {
            CustomerLastName = Guard.Against.NullOrEmpty(customerLastName, nameof(customerLastName));
        }

        public void SetCustomerMiddleName(string customerMiddleName)
        {
            CustomerMiddleName = customerMiddleName;
        }

        public void SetCustomerNChar(string customerNchar)
        {
            CustomerNChar = customerNchar;
        }

        public void SetCustomerNText(string customerNtext)
        {
            CustomerNText = customerNtext;
        }

        public void SetCustomerNVarChar(string customerNvarChar)
        {
            CustomerNVarChar = customerNvarChar;
        }

        public void SetCustomerStatus(string customerStatus)
        {
            CustomerStatus = customerStatus;
        }

        public void SetCustomerText(string customerText)
        {
            CustomerText = customerText;
        }

        public void SetCustomerTitle(string customerTitle)
        {
            CustomerTitle = customerTitle;
        }

        public void SetCustomerVarChar(string customerVarChar)
        {
            CustomerVarChar = customerVarChar;
        }

        public void SetCustomerWebsite(string customerWebsite)
        {
            CustomerWebsite = customerWebsite;
        }

        public void SetCustomerXml(string customerXml)
        {
            CustomerXml = customerXml;
        }

        public void SetIndustry(string industry)
        {
            Industry = industry;
        }

        public void SetNotes(string notes)
        {
            Notes = notes;
        }

        public void SetPreferredContactMethod(string preferredContactMethod)
        {
            PreferredContactMethod = preferredContactMethod;
        }

        public void SetSource(string source)
        {
            Source = source;
        }
    }
}